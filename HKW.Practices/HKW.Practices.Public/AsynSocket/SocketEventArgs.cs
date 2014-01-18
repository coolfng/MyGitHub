using System;
using System.Net.Sockets;

namespace HKW.Practices.Public.AsynSocket
{
    public class SocketEventArgs : EventArgs
    {
        public readonly string StringFromSocketEvent;
        public readonly Socket Handler;

        public SocketEventArgs(string stringFromSocketEvent)
        {
            StringFromSocketEvent = stringFromSocketEvent;
        }
        public SocketEventArgs(string stringFromSocketEvent, Socket handler)
        {
            StringFromSocketEvent = stringFromSocketEvent;
            Handler = handler;
        }
    }
}
