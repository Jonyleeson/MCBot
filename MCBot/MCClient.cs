using System;
using System.Collections.Generic;
using System.Threading;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public sealed class MCClient : MCRawClient
    {
        #region Members
        private string m_Username;
        private string m_Password;
        private string m_ServerPassword;
        private bool m_UseAuth;
        private MCAccountInfo m_AccountInfo;
        private Thread m_Heartbeat;
        private MCBlockType m_SelectedItem;
        private MCItem[] m_EquipInv;
        private MCItem[] m_CraftInv;
        private MCItem[] m_ItemInv;
        private Point3D m_Position;
        private List<MCPlayer> m_Players;
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return m_Username;
            }
        }

        public long Time
        { get; private set; }

        public MCBlockType SelectedItem
        {
            get
            {
                return m_SelectedItem;
            }
            set
            {
                m_SelectedItem = value;
                ChangeHolding(m_SelectedItem);
            }
        }
        
        public MCItem[] EquipInventory
        {
            get
            {
                return m_EquipInv;
            }
            set
            {
                if (value.Length != 4)
                    throw new Exception("Equipment Inventory must contain 4 items. Use MCBlockType.None for empty slots.");

                m_EquipInv = value;
                UpdateInventory(MCInventoryType.Equipment, m_EquipInv);
            }
        }
        
        public MCItem[] CraftInventory
        {
            get
            {
                return m_CraftInv;
            }
            set
            {
                if (value.Length != 4)
                    throw new Exception("Crafting Inventory must contain 4 items. Use MCBlockType.None for empty slots.");

                m_CraftInv = value;
                UpdateInventory(MCInventoryType.Crafting, m_CraftInv);
            }
        }
        
        public MCItem[] ItemInventory
        {
            get
            {
                return m_ItemInv;
            }
            set
            {
                if (value.Length != 4)
                    throw new Exception("Item Inventory must contain 36 items. Use MCBlockType.None for empty slots.");

                m_ItemInv = value;
                UpdateInventory(MCInventoryType.Items, m_ItemInv);
            }
        }
        
        public Point3D Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
                Move(m_Position, m_Position.Y + 0.5, true);
            }
        }
        
        public Point3D SpawnPoint
        { get; private set; }

        public MCPlayer[] Players
        {
            get 
            { 
                return m_Players.ToArray(); 
            }
        }
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

            m_SelectedItem = MCBlockType.Air;
            m_Position = new Point3D();
            m_ItemInv = null;
            m_CraftInv = null;
            m_EquipInv = null;
            m_Players = new List<MCPlayer>();
            Time = 0;
            SpawnPoint = new Point3D();

            base.OnDisconnect += new DisconnectEventHandler(MCClient_OnDisconnect);
            base.OnHandshake += new HandshakeEventHandler(MCClient_OnHandshake);
            base.OnLogin += new LoginEventHandler(MCClient_OnLogin);
            base.OnUpdateTime += new UpdateTimeEventHandler(MCClient_OnUpdateTime);
            base.OnPlayerMoveLook += new PlayerMoveLookEventHandler(MCClient_OnPlayerMoveLook);
            base.OnAddInventory += new AddInventoryEventHandler(MCClient_OnAddInventory);
            base.OnPlayerInventory += new PlayerInventoryEventHandler(MCClient_OnPlayerInventory);
            base.OnSpawnPosition += new SpawnPositionEventHandler(MCClient_OnSpawnPosition);
            base.OnNamedEntitySpawn += new NamedEntitySpawnEventHandler(MCClient_OnNamedEntitySpawn);
        }

        void MCClient_OnNamedEntitySpawn(object sender, NamedEntitySpawnEventArgs e)
        {
            m_Players.Add(new MCPlayer(e.ID, e.Position, e.Name, e.Yaw, e.Pitch, e.Item));
        }

        void MCClient_OnSpawnPosition(object sender, SpawnPositionEventArgs e)
        {
            SpawnPoint = e.Position;
        }

        void MCClient_OnPlayerInventory(object sender, PlayerInventoryEventArgs e)
        {
            switch (e.Inventory)
            {
                case MCInventoryType.Items:
                    m_ItemInv = e.Items;
                    break;
                case MCInventoryType.Crafting:
                    m_CraftInv = e.Items;
                    break;
                case MCInventoryType.Equipment:
                    m_EquipInv = e.Items;
                    break;
            }
        }

        void MCClient_OnAddInventory(object sender, AddInventoryEventArgs e)
        {
            foreach (MCItem item in m_ItemInv)
            {
                if (item.Type == MCBlockType.None)
                {
                    item.Type = e.Item;
                    item.Health = e.Health;
                    item.Count = e.Count;
                }
            }
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
            m_Position = e.Position;

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
