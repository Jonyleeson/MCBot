using System;

namespace Org.Jonyleeson.MCBot
{
    public delegate void UpdateTimeEventHandler(object sender, UpdateTimeEventArgs e);

    public class UpdateTimeEventArgs : EventArgs
    {
        public long Time
        { get; private set; }

        public UpdateTimeEventArgs(long time)
            : base()
        {
            Time = time;
        }
    }
}
