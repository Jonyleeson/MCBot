using System;

namespace Org.Jonyleeson.MCBot
{
    public delegate void ChatEventHandler(object sender, ChatEventArgs e);

    public class ChatEventArgs : EventArgs
    {
        public string Message
        { get; private set; }

        public ChatEventArgs(string message)
            : base()
        {
            Message = message;
        }
    }
}
