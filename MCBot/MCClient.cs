using System;
using System.Threading;

namespace Org.Jonyleeson.MCBot
{
    sealed class MCClient : MCRawClient
    {
        #region Members
        private string m_Username;
        private string m_Password;
        private string m_ServerPassword;
        private bool m_UseAuth;
        private MCAccountInfo m_AccountInfo;
        private Thread m_Heartbeat;
        #endregion

        #region Properties
        public long Time
        { get; private set; }
        #endregion

        #region Constructors
        public MCClient(string server, int port)
            : this(server, port, "Anonymous", "", "Password", false)
        { }

        public MCClient(string server, int port, string username)
            : this(server, port, username, "", "Password", false)
        { }

        public MCClient(string server, int port, string username, string password)
            : this(server, port, username, password, "Password", true)
        { }

        public MCClient(string server, int port, string username, string password, string serverpassword)
            : this(server, port, username, password, serverpassword, false)
        { }

        public MCClient(string server, int port, string username, string password, string serverpassword, bool useauth)
            : base(server, port)
        {
            m_Username = username;
            m_Password = password;
            m_ServerPassword = serverpassword;
            m_UseAuth = useauth;

            if (m_UseAuth)
            {
                m_AccountInfo = MCLogin.GetInfo(m_Username, m_Password);

                if (m_AccountInfo == null)
                    throw new Exception("Invalid credentials or bot is outdated.");
            }

            base.OnDisconnect += new DisconnectEventHandler(MCClient_OnDisconnect);
            base.OnHandshake += new HandshakeEventHandler(MCClient_OnHandshake);
            base.OnLogin += new LoginEventHandler(MCClient_OnLogin);
            base.OnUpdateTime += new UpdateTimeEventHandler(MCClient_OnUpdateTime);
            base.OnPlayerMoveLook += new PlayerMoveLookEventHandler(MCClient_OnPlayerMoveLook);
        }
        #endregion

        #region Methods
        void MCClient_OnDisconnect(object sender, DisconnectEventArgs e)
        {
            m_Heartbeat.Abort();
        }

        void MCClient_OnUpdateTime(object sender, UpdateTimeEventArgs e)
        {
            this.Time = e.Time;
        }

        void MCClient_OnLogin(object sender, LoginEventArgs e)
        {
            m_Heartbeat = new Thread(new ThreadStart(delegate { HeartbeatThread(); }));
            m_Heartbeat.Start();
        }

        void MCClient_OnHandshake(object sender, HandshakeEventArgs e)
        {
            if (e.Hash != "-") // some form of auth is required
            {
                if (e.Hash == "+")
                {
                    Login(m_Username, m_ServerPassword);
                    return;
                }
                else
                {
                    if (m_UseAuth)
                    {
                        string message;

                        if ((message = MCLogin.JoinServer(m_Username, m_AccountInfo.Session, e.Hash)) != "OK")
                            throw new Exception(string.Format("Unable to join server, minecraft.net says : {0}", message));
                    }
                }
            }

            Login(m_Username, "Password");
        }

        void MCClient_OnPlayerMoveLook(object sender, PlayerMoveLookEventArgs e)
        {
            // Server wants us to echo this position, so we do just that
            MoveLook(e.Position, e.Stance, e.Yaw, e.Pitch, e.Ground);
        }

        private void HeartbeatThread()
        {
            while (this.Connected)
            {
                Heartbeat();
                Thread.Sleep(1000);
            }
        }

        public void Join()
        {
            Handshake(m_Username);
        }
        #endregion
    }
}
