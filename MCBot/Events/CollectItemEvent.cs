using System;

namespace Org.Jonyleeson.MCBot
{
    public delegate void CollectItemEventHandler(object sender, CollectItemEventArgs e);

    public class CollectItemEventArgs : EventArgs
    {
        public int ObjectID
        { get; private set; }

        public int PlayerID
        { get; private set; }

        public CollectItemEventArgs(int objectid, int playerid)
            : base()
        {
            ObjectID = objectid;
            PlayerID = playerid;
        }
    }
}
