using System;

namespace Org.Jonyleeson.MCBot
{
    public delegate void EntityEventHandler(object sender, EntityEventArgs e);

    public class EntityEventArgs : EventArgs
    {
        public int ID
        { get; private set; }

        public EntityEventArgs(int id)
            : base()
        {
            ID = id;
        }
    }
}
