using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace HKW.Practices.Public.AsynSocket
{
    public class AsynSocketClient
    {
        public delegate void AcceptSocketEventHandler(object sender, SocketEventArgs e);
        public event AcceptSocketEventHandler AcceptSocketEvent;

        public delegate void ErrorSocketEventHandler(object sender, SocketEventArgs e);
        public event ErrorSocketEventHandler ErrorSocketEvent;

        
        private int _port = 11000;
        private Socket _client;
        private string _sendData;
        private string _remoteIPAddress;

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        private void Connect(String remoteIPAddress)
        {
            IPAddress ipAddress = IPAddress.Parse(remoteIPAddress);
            var remoteEp = new IPEndPoint(ipAddress, _port);

            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _client.BeginConnect(remoteEp, ConnectCallback, _client);
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                var handler = (Socket)ar.AsyncState;
                handler.EndConnect(ar);
                SendSocket(_sendData);
                Receive();
            }
            catch (Exception e)
            {
                Console.WriteLine("===连接超时===================================================");
                RaiseErrorEvent(string.Format("Socket连接超时：{0}", _remoteIPAddress));
                Console.WriteLine(e.ToString());

            }
        }

        public void Send(String remoteIPAddress, String data)
        {
            _sendData = data;
            _remoteIPAddress = remoteIPAddress;
            Connect(remoteIPAddress);
        }

        private void SendSocket(String data)
        {
            Console.Write("Send:{0} , ", data);
            data = data + "<EOF>";
            byte[] byteData = Encoding.Default.GetBytes(data);
            _client.BeginSend(byteData, 0, byteData.Length, 0,
                SendCallback, _client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                var client = (Socket)ar.AsyncState;
                if (client != null) client.EndSend(ar);
                Console.WriteLine("SendCallback.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Receive()
        {
            try
            {
                var state = new StateObject {WorkSocket = _client};
                _client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
                    ReceiveCallback, state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var state = (StateObject)ar.AsyncState;
                Socket client = state.WorkSocket;
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.Sb.Append(Encoding.Default.GetString(state.Buffer, 0, bytesRead));
                    client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
                        ReceiveCallback, state);
                }
                else
                {
                    if (state.Sb.Length > 1)
                    {
                        RaiseAcceptEvent(state.Sb.ToString());
                        state.Sb.Clear();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        protected virtual void OnAcceptEvent(SocketEventArgs e)
        {
            if (AcceptSocketEvent != null)
                AcceptSocketEvent(this, e);
        }

        public void RaiseAcceptEvent(string stringFromSocketEvent)
        {
            var e = new SocketEventArgs(stringFromSocketEvent);
            OnAcceptEvent(e);
        }

        protected virtual void OnErrorEvent(SocketEventArgs e)
        {
            if (ErrorSocketEvent != null)
                ErrorSocketEvent(this, e);
        }

        public void RaiseErrorEvent(string errorSocketEvent)
        {
            var e = new SocketEventArgs(errorSocketEvent);
            OnErrorEvent(e);
        }
    }
}
