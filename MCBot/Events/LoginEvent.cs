using System;

namespace Org.Jonyleeson.MCBot
{
    public delegate void LoginEventHandler(object sender, LoginEventArgs e);

    public class LoginEventArgs : EventArgs
    {
        public int PlayerID
        { get; private set; }

        public string Username
        { get; private set; }

        public string Password
        { get; private set; }

        public LoginEventArgs(int id, string username, string password)
            : base()
        {
            PlayerID = id;
            Username = username;
            Password = password;
        }
    }
}
