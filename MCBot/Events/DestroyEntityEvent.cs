using System;

namespace Org.Jonyleeson.MCBot
{
    public delegate void DestroyEntityEventHandler(object sender, DestoryEntityEventArgs e);

    public class DestoryEntityEventArgs : EventArgs
    {
        public int ID
        { get; private set; }

        public DestoryEntityEventArgs(int id)
            : base()
        {
            ID = id;
        }
    }
}
