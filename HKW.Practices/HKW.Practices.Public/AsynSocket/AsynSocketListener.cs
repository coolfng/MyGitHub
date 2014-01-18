using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace HKW.Practices.Public.AsynSocket
{
    public sealed class AsynSocketListener
    {
        public delegate void AcceptSocketEventHandler(object sender, SocketEventArgs e);
        public event AcceptSocketEventHandler AcceptSocketEvent;

        private readonly Socket _socketListener;
        private readonly string _ipEndPoint;

        /// <summary>
        /// 异步Socket监听器
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="blocklog"></param>
        public AsynSocketListener(string ip, int port, int blocklog)
        {
            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); //   Dns.Resolve(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPAddress ipAddress = IPAddress.Parse(ip);
            var localEndPoint = new IPEndPoint(ipAddress, port);
            _ipEndPoint = string.Format("{0}:{1}", localEndPoint.Address, localEndPoint.Port);
            Logger.Write(string.Format("开始初始化 - {0}, BlockLog:{1}", _ipEndPoint, blocklog));
            _socketListener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            try
            {
                _socketListener.Bind(localEndPoint);
                _socketListener.Listen(blocklog);
                Logger.Write(string.Format("初始化成功 - {0}", _ipEndPoint));
            }
            catch (SocketException e)
            {
                Logger.Write(string.Format("!!!初始化错误：{0}  {1}", e.Message, _ipEndPoint));
            }
            catch (Exception e)
            {
                LogError("AsynSocketListener", e.Message);
            }
        }

        public void StartListening()
        {
            try
            {
                _socketListener.BeginAccept(AcceptCallback, _socketListener);
            }
            catch (ObjectDisposedException e)
            {
                Logger.Write(string.Format("!!!开始监听错误1：{0}  {1}", e.Message, _ipEndPoint));
            }
            catch (InvalidOperationException e)
            {
                Logger.Write(string.Format("!!!开始监听错误2：{0}  {1}", e.Message, _ipEndPoint));
            }
            catch (SocketException e)
            {
                Logger.Write(string.Format("!!!开始监听错误3：{0}  {1}", e.Message, _ipEndPoint));
            }
            catch (Exception e)
            {
                LogError("StartListening", e.Message);
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                StartListening();
                var listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);
                var state = new StateObject {WorkSocket = handler};
                handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
                    ReadCallback, state);
            }
            catch (Exception e)
            {
                LogError("AcceptCallback", e.Message);
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            try
            {
              //  String content = String.Empty;
                var state = (StateObject)ar.AsyncState;
                Socket handler = state.WorkSocket;

                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.Sb.Append(Encoding.Default.GetString(
                        state.Buffer, 0, bytesRead));
                    String content = state.Sb.ToString();
                    if (content.IndexOf("<EOF>", StringComparison.Ordinal) > -1)
                    {
                        content = content.Replace("<EOF>", "");
                        RaiseAcceptEvent(content, handler);
                        state.Sb.Clear();
                    }
                    else
                    {
                        handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
                        ReadCallback, state);
                    }
                }
            }
            catch (Exception e)
            {
                LogError("ReadCallback", e.Message);
            }
        }

        private void OnAcceptEvent(SocketEventArgs e)
        {
            if (AcceptSocketEvent != null)
                AcceptSocketEvent(this, e);
        }

        public void RaiseAcceptEvent(string stringFromSocketEvent, Socket handler)
        {
            var e = new SocketEventArgs(stringFromSocketEvent, handler);
            OnAcceptEvent(e);
        }

        public void Send(Socket handler, String data)
        {
            try
            {
                data += "<EOF>";
                byte[] byteData = Encoding.Default.GetBytes(data);
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                    SendCallback, handler);
            }
            catch (Exception e)
            {
                LogError("Send", e.Message);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                var handler = (Socket)ar.AsyncState;
                handler.EndSend(ar);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
                //  StartListening();
            }
            catch (Exception e)
            {
                LogError("SendCallback", e.Message);
            }
        }

        private void LogError(string errPoint, string errMessage)
        {
            Logger.Write(string.Format("ERROR - AsynSocketListener.{0}:{1}   {2}", errPoint, errMessage, _ipEndPoint));
        }
    }
}
