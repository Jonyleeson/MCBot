using System;
using System.Collections.Generic;
using System.IO;
using Org.Jonyleeson.Generic;
using Org.Jonyleeson.IO;
using Org.Jonyleeson.IO.Compression;
using Org.Jonyleeson.MCBot.NBT;

namespace Org.Jonyleeson.MCBot
{
    public class MCRawClient
    {
        private MCSocket m_Socket;
        private string m_Server;
        private int m_Port;

        public event HandshakeEventHandler OnHandshake;
        public event DisconnectEventHandler OnDisconnect;
        public event LoginEventHandler OnLogin;
        public event PreChunkEventHandler OnPreChunk;
        public event SpawnPositionEventHandler OnSpawnPosition;
        public event ChatEventHandler OnChat;
        public event MobSpawnEventHandler OnMobSpawn;
        public event ItemSpawnEventHandler OnItemSpawn;
        public event NamedEntitySpawnEventHandler OnNamedEntitySpawn;
        public event UpdateTimeEventHandler OnUpdateTime;
        public event PlayerMoveLookEventHandler OnPlayerMoveLook;
        public event PlayerInventoryEventHandler OnPlayerInventory;
        public event HeartbeatEventHandler OnHeartbeat;
        public event EntityEventHandler OnEntity;
        public event RelativeEntityMoveEventHandler OnRelativeEntityMove;
        public event RelativeEntityMoveLookEventHandler OnRelativeEntityMoveLook;
        public event EntityLookEventHandler OnEntityLook;
        public event DestroyEntityEventHandler OnDestroyEntity;
        public event AddVehicleEventHandler OnAddVehicle;
        public event ArmAnimationEventHandler OnArmAnimation;
        public event BlockChangeEventHandler OnBlockChange;
        public event HoldSwitchEventHandler OnHoldSwitch;
        public event EntityTeleportEventHandler OnEntityTeleport;
        public event MultiBlockChangeEventHandler OnMultiBlockChange;
        public event MapChunkEventHandler OnMapChunk;
        public event UnknownPacketEventHandler OnUnknownPacket;
        public event ComplexEntitiesEventHandler OnComplexEntity;
        public event CollectItemEventHandler OnCollectItem;
        public event AddInventoryEventHandler OnAddInventory;

        public bool Connected
        {
            get
            { return m_Socket.Connected; }
        }

        public MCRawClient(string server, int port)
        {
            m_Server = server;
            m_Port = port;
        }

        #region Methods
        private void SendPacket(byte[] packet)
        {
            if (m_Socket != null)
                m_Socket.SendPacket(packet);
            else
                throw new Exception("Client not connected. Call .Connect() first.");
        }

        public void Disconnect()
        {
            this.Disconnect("MCBot Disconnecting...");
        }

        public void Disconnect(string message)
        {
            m_Socket.Disconnect(message);
        }

        public void Connect()
        {
            m_Socket = new MCSocket(m_Server, m_Port);
            m_Socket.OnRecieve += new PacketEventHandler(m_Socket_OnRecieve);
        }

        #region Parsing
        protected void ParsePlayerInventory(BinaryReader reader)
        {
            int type = reader.ReadNetworkInt32();
            short count = reader.ReadNetworkInt16();

            List<MCItem> items = new List<MCItem>();

            for (int i = 0; i < count; i++)
            {
                short itemid = reader.ReadNetworkInt16();

                if (itemid == -1)
                    items.Add(new MCItem(-1, 0, 0));
                else
                    items.Add(new MCItem(itemid, reader.ReadByte(), reader.ReadNetworkInt16()));
            }

            HandlePlayerInventory(type, count, items);
        }

        protected void ParseMultiBlockChange(BinaryReader reader)
        {
            int x = reader.ReadNetworkInt32();
            int z = reader.ReadNetworkInt32();
            short size = reader.ReadNetworkInt16();

            List<MCBlockTransform> changes = new List<MCBlockTransform>();

            Vector3D[] relativepos = new Vector3D[size];
            byte[] blocks = new byte[size];
            byte[] metadatas = new byte[size];

            for (short i = 0; i < size; i++)
            {
                short xyz = reader.ReadNetworkInt16();

                int xpos = (xyz >> 12) & 0xF;
                int zpos = (xyz >> 8) & 0xF;
                int ypos = xyz & 0xFF;

                relativepos[i] = new Vector3D(xpos, ypos, zpos);
            }

            for (short i = 0; i < size; i++)
                blocks[i] = reader.ReadByte();

            for (short i = 0; i < size; i++)
                metadatas[i] = reader.ReadByte();

            for (short i = 0; i < size; i++)
                changes.Add(new MCBlockTransform(relativepos[i], blocks[i], metadatas[i]));

            HandleMultiBlockChange(x, z, size, changes);
        }

        private void ParseMapChunk(BinaryReader reader)
        {
            int x = reader.ReadNetworkInt32();
            short y = reader.ReadNetworkInt16();
            int z = reader.ReadNetworkInt32();
            byte size_x = reader.ReadByte();
            byte size_y = reader.ReadByte();
            byte size_z = reader.ReadByte();
            int size = reader.ReadNetworkInt32();

            if (reader.BaseStream.Position + size > reader.BaseStream.Length)        // Like omg BinaryReader won't throw exceptions
                throw new EndOfStreamException("Reader has reached end of stream."); // if you read past the end of the stream :@

            byte[] data = Compression.DecompressZLib(reader.ReadBytes(size));

            // parse data block

            size_x++;
            size_y++;
            size_z++;

            // wat to do here... hmm

            MCBlock[, ,] blockdata = new MCBlock[size_x, size_y, size_z];

            BinaryReader parser = new BinaryReader(new MemoryStream(data));

            byte[] blocks = parser.ReadBytes(size_x * size_y * size_z);
            byte[] metadatas = new byte[blocks.Length];
            byte[] lights = new byte[blocks.Length];
            byte[] skylights = new byte[blocks.Length];

            for (int i = 0; i < blocks.Length; i += 2)
            {
                byte metadata = parser.ReadByte();

                metadatas[i] = (byte)((metadata >> 4) & 0xF);
                metadatas[i + 1] = (byte)(metadata & 0xF);
            }

            for (int i = 0; i < blocks.Length; i += 2)
            {
                byte light = parser.ReadByte();

                lights[i] = (byte)((light >> 4) & 0xF);
                lights[i + 1] = (byte)(light & 0xF);
            }

            for (int i = 0; i < blocks.Length; i += 2)
            {
                byte skylight = parser.ReadByte();

                skylights[i] = (byte)((skylight >> 4) & 0xF);
                skylights[i + 1] = (byte)(skylight & 0xF);
            }

            for (int iX = 0; iX < size_x; iX++)
            {
                for (int iY = 0; iY < size_y; iY++)
                {
                    for (int iZ = 0; iZ < size_z; iZ++)
                    {
                        int index = iY + (iZ * size_y) + (iX * size_y * size_z);

                        blockdata[iX, iY, iZ] = new MCBlock();

                        blockdata[iX, iY, iZ].Type = (MCBlockType)blocks[index];
                        blockdata[iX, iY, iZ].MetaData = metadatas[index];
                        blockdata[iX, iY, iZ].Light = lights[index];
                        blockdata[iX, iY, iZ].SkyLight = skylights[index];
                    }
                }
            }

            HandleMapChunk(x, y, z, blockdata);
        }

        private void ParseComplexEntities(BinaryReader reader)
        {
            int x = reader.ReadNetworkInt32();
            short y = reader.ReadNetworkInt16();
            int z = reader.ReadNetworkInt32();
            short size = reader.ReadNetworkInt16();

            if (reader.BaseStream.Position + size > reader.BaseStream.Length)
                throw new EndOfStreamException("Reader has reached end of stream.");

            byte[] data = Compression.DecompressGZip(reader.ReadBytes(size));

            INamedBinaryTag nbt = NBTStructure.ParseNBT(data);

            HandleComplexEntities(x, y, z, nbt);
        }

        private void ParseHandshake(BinaryReader reader)
        {
            HandleHandshake(reader.ReadNetworkString());
        }

        private void ParseDisconnect(BinaryReader reader)
        {
            HandleDisconnect(reader.ReadNetworkString());
        }

        private void ParseLogin(BinaryReader reader)
        {
            HandleLogin(reader.ReadNetworkInt32(), reader.ReadNetworkString(), reader.ReadNetworkString());
        }

        private void ParsePreChunk(BinaryReader reader)
        {
            HandlePreChunk(reader.ReadNetworkInt32(), reader.ReadNetworkInt32(), reader.ReadBoolean());
        }

        private void ParseSpawnPosition(BinaryReader reader)
        {
            HandleSpawnPosition(reader.ReadNetworkInt32(), reader.ReadNetworkInt32(), reader.ReadNetworkInt32());
        }

        private void ParseChat(BinaryReader reader)
        {
            HandleChat(reader.ReadNetworkString());
        }

        private void ParseMobSpawn(BinaryReader reader)
        {
            HandleMobSpawn(reader.ReadNetworkInt32(), reader.ReadByte(), reader.ReadNetworkInt32(), reader.ReadNetworkInt32(), reader.ReadNetworkInt32(), reader.ReadByte(), reader.ReadByte());
        }

        private void ParseItemSpawn(BinaryReader reader)
        {
            HandleItemSpawn(reader.ReadNetworkInt32(), reader.ReadNetworkInt16(), reader.ReadByte(), reader.ReadNetworkInt32(), reader.ReadNetworkInt32(), reader.ReadNetworkInt32(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        }

        private void ParseNamedEntitySpawn(BinaryReader reader)
        {
            HandleNamedEntitySpawn(reader.ReadNetworkInt32(), reader.ReadNetworkString(), reader.ReadNetworkInt32(), reader.ReadNetworkInt32(), reader.ReadNetworkInt32(), reader.ReadByte(), reader.ReadByte(), reader.ReadNetworkInt16());
        }

        private void ParseUpdateTime(BinaryReader reader)
        {
            HandleUpdateTime(reader.ReadNetworkInt64());
        }

        private void ParsePlayerMoveLook(BinaryReader reader)
        {
            HandlePlayerMoveLook(reader.ReadNetworkDouble(), reader.ReadNetworkDouble(), reader.ReadNetworkDouble(), reader.ReadNetworkDouble(), reader.ReadNetworkSingle(), reader.ReadNetworkSingle(), reader.ReadBoolean());
        }

        private void ParseEntity(BinaryReader reader)
        {
            HandleEntity(reader.ReadNetworkInt32());
        }

        private void ParseRelativeEntityMove(BinaryReader reader)
        {
            HandleRelativeEntityMove(reader.ReadNetworkInt32(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        }

        private void ParseRelativeEntityMoveLook(BinaryReader reader)
        {
            HandleRelativeEntityMoveLook(reader.ReadNetworkInt32(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        }

        private void ParseEntityLook(BinaryReader reader)
        {
            HandleEntityLook(reader.ReadNetworkInt32(), reader.ReadByte(), reader.ReadByte());
        }

        private void ParseDestroyEntity(BinaryReader reader)
        {
            HandleDestroyEntity(reader.ReadNetworkInt32());
        }

        private void ParseAddVehicle(BinaryReader reader)
        {
            HandleAddVehicle(reader.ReadNetworkInt32(), reader.ReadByte(), reader.ReadNetworkInt32(), reader.ReadNetworkInt32(), reader.ReadNetworkInt32());
        }

        private void ParseArmAnimation(BinaryReader reader)
        {
            HandleArmAnimation(reader.ReadNetworkInt32(), reader.ReadBoolean());
        }

        private void ParseBlockChange(BinaryReader reader)
        {
            HandleBlockChange(reader.ReadNetworkInt32(), reader.ReadByte(), reader.ReadNetworkInt32(), reader.ReadByte(), reader.ReadByte());
        }

        private void ParseHoldSwitch(BinaryReader reader)
        {
            HandleHoldSwitch(reader.ReadNetworkInt32(), reader.ReadNetworkInt16());
        }

        private void ParseEntityTeleport(BinaryReader reader)
        {
            HandleEntityTeleport(reader.ReadNetworkInt32(), reader.ReadNetworkInt32(), reader.ReadNetworkInt32(), reader.ReadNetworkInt32(), reader.ReadByte(), reader.ReadByte());
        }

        private void ParseCollectItem(BinaryReader reader)
        {
            HandleCollectItem(reader.ReadNetworkInt32(), reader.ReadNetworkInt32());
        }

        private void ParseAddInventory(BinaryReader reader)
        {
            HandleAddInventory(reader.ReadNetworkInt16(), reader.ReadByte(), reader.ReadNetworkInt16());
        }
        #endregion

        #region Handle
        private void HandleHandshake(string hash)
        {
            if (OnHandshake != null)
                OnHandshake(this, new HandshakeEventArgs(hash));
        }
        
        private void HandleDisconnect(string reason)
        {
            if (OnDisconnect != null)
                OnDisconnect(this, new DisconnectEventArgs(reason));

            m_Socket.Disconnect("MCBot Disconnecting..."); // no matter what we're disconnecting
        }

        private void HandleLogin(int id, string username, string password)
        {
            if (OnLogin != null)
                OnLogin(this, new LoginEventArgs(id, username, password));
        }

        private void HandlePreChunk(int x, int z, bool mode)
        {
            if (OnPreChunk != null)
                OnPreChunk(this, new PreChunkEventArgs(x, z, mode));
        }

        private void HandleSpawnPosition(int x, int y, int z)
        {
            if (OnSpawnPosition != null)
                OnSpawnPosition(this, new SpawnPositionEventArgs(x, y, z));
        }

        private void HandleChat(string message)
        {
            if (OnChat != null)
                OnChat(this, new ChatEventArgs(message));
        }

        private void HandleMobSpawn(int id, byte type, int x, int y, int z, byte yaw, byte pitch)
        {
            if (OnMobSpawn != null)
                OnMobSpawn(this, new MobSpawnEventArgs(id, type, x, y, z, yaw, pitch));
        }

        private void HandleItemSpawn(int id, short item, byte count, int x, int y, int z, byte yaw, byte pitch, byte roll)
        {
            if (OnItemSpawn != null)
                OnItemSpawn(this, new ItemSpawnEventArgs(id, item, count, x, y, z, yaw, pitch, roll));
        }

        private void HandleNamedEntitySpawn(int id, string name, int x, int y, int z, byte yaw, byte pitch, short item)
        {
            if (OnNamedEntitySpawn != null)
                OnNamedEntitySpawn(this, new NamedEntitySpawnEventArgs(id, name, x, y, z, yaw, pitch, item));
        }

        private void HandleUpdateTime(long time)
        {
            if (OnUpdateTime != null)
                OnUpdateTime(this, new UpdateTimeEventArgs(time));
        }

        private void HandlePlayerMoveLook(double x, double stance, double y, double z, float yaw, float pitch, bool ground)
        {
            if (OnPlayerMoveLook != null)
                OnPlayerMoveLook(this, new PlayerMoveLookEventArgs(x, y, stance, z, yaw, pitch, ground));
        }

        private void HandlePlayerInventory(int type, short count, List<MCItem> items)
        {
            if (OnPlayerInventory != null)
                OnPlayerInventory(this, new PlayerInventoryEventArgs((MCInventoryType)type, items));
        }

        private void HandleHeartbeat()
        {
            if (OnHeartbeat != null)
                OnHeartbeat(this, new EventArgs());
        }

        private void HandleEntity(int id)
        {
            if (OnEntity != null)
                OnEntity(this, new EntityEventArgs(id));
        }

        private void HandleRelativeEntityMove(int id, byte x, byte y, byte z)
        {
            if (OnRelativeEntityMove != null)
                OnRelativeEntityMove(this, new RelativeEntityMoveEventArgs(id, x, y, z));
        }

        private void HandleRelativeEntityMoveLook(int id, byte x, byte y, byte z, byte yaw, byte pitch)
        {
            if (OnRelativeEntityMoveLook != null)
                OnRelativeEntityMoveLook(this, new RelativeEntityMoveLookEventArgs(id, x, y, z, yaw, pitch));
        }

        private void HandleEntityLook(int id, byte yaw, byte pitch)
        {
            if (OnEntityLook != null)
                OnEntityLook(this, new EntityLookEventArgs(id, yaw, pitch));
        }

        private void HandleDestroyEntity(int id)
        {
            if (OnDestroyEntity != null)
                OnDestroyEntity(this, new DestoryEntityEventArgs(id));
        }

        private void HandleAddVehicle(int id, byte type, int x, int y, int z)
        {
            if (OnAddVehicle != null)
                OnAddVehicle(this, new AddVehicleEventArgs(id, (MCVehicleType)type, x, y, z));
        }

        private void HandleArmAnimation(int entityid, bool forward)
        {
            if (OnArmAnimation != null)
                OnArmAnimation(this, new ArmAnimationEventArgs(entityid, forward));
        }

        private void HandleBlockChange(int x, byte y, int z, byte type, byte metadata)
        {
            if (OnBlockChange != null)
                OnBlockChange(this, new BlockChangeEventArgs(x, y, z, type, metadata));
        }

        private void HandleHoldSwitch(int entity, short item)
        {
            if (OnHoldSwitch != null)
                OnHoldSwitch(this, new HoldSwitchEventArgs(entity, item));
        }

        private void HandleEntityTeleport(int id, int x, int y, int z, byte yaw, byte pitch)
        {
            if (OnEntityTeleport != null)
                OnEntityTeleport(this, new EntityTeleportEventArgs(id, x, y, z, yaw, pitch));
        }

        private void HandleMapChunk(int x, short y, int z, MCBlock[, ,] data)
        {
            if (OnMapChunk != null)
                OnMapChunk(this, new MapChunkEventArgs(x, y, z, data));
        }

        private void HandleMultiBlockChange(int x, int z, short size, List<MCBlockTransform> changes)
        {
            if (OnMultiBlockChange != null)
                OnMultiBlockChange(this, new MultiBlockChangeEventArgs(x, z, changes.ToArray()));
        }

        private void HandleCollectItem(int objectid, int playerid)
        {
            if (OnCollectItem != null)
                OnCollectItem(this, new CollectItemEventArgs(objectid, playerid));
        }

        private void HandleComplexEntities(int x, short y, int z, INamedBinaryTag nbt)
        {
            if (OnComplexEntity != null)
                OnComplexEntity(this, new ComplexEntitiesEventArgs(x, y, z, nbt));
        }

        private void HandleAddInventory(short item, byte count, short life)
        {
            if (OnAddInventory != null)
                OnAddInventory(this, new AddInventoryEventArgs(item, count, life));
        }
        #endregion

        void m_Socket_OnRecieve(object sender, PacketRecieveEventArgs e)
        {
            BinaryReader reader = e.Reader;

            // packet opcode being the first byte
            byte opcode = reader.ReadByte();

            switch ((MCPacketOpcodes)opcode)
            {
                case MCPacketOpcodes.Disconnect:
                    ParseDisconnect(reader);
                    break;
                case MCPacketOpcodes.Handshake:
                    ParseHandshake(reader);
                    break;
                case MCPacketOpcodes.Login:
                    ParseLogin(reader);
                    break;
                case MCPacketOpcodes.PreChunk:
                    ParsePreChunk(reader);
                    break;
                case MCPacketOpcodes.SpawnPosition:
                    ParseSpawnPosition(reader);
                    break;
                case MCPacketOpcodes.Chat:
                    ParseChat(reader);
                    break;
                case MCPacketOpcodes.MobSpawn:
                    ParseMobSpawn(reader);
                    break;
                case MCPacketOpcodes.ItemSpawn:
                    ParseItemSpawn(reader);
                    break;
                case MCPacketOpcodes.NamedEntitySpawn:
                    ParseNamedEntitySpawn(reader);
                    break;
                case MCPacketOpcodes.UpdateTime:
                    ParseUpdateTime(reader);
                    break;
                case MCPacketOpcodes.PlayerMoveLook:
                    ParsePlayerMoveLook(reader);
                    break;
                case MCPacketOpcodes.PlayerInventory:
                    ParsePlayerInventory(reader);
                    break;
                case MCPacketOpcodes.KeepAlive:
                    HandleHeartbeat();
                    break;
                case MCPacketOpcodes.Entity:
                    ParseEntity(reader);
                    break;
                case MCPacketOpcodes.RelativeEntityMove:
                    ParseRelativeEntityMove(reader);
                    break;
                case MCPacketOpcodes.RelativeEntityMoveLook:
                    ParseRelativeEntityMoveLook(reader);
                    break;
                case MCPacketOpcodes.EntityLook:
                    ParseEntityLook(reader);
                    break;
                case MCPacketOpcodes.DestroyEntity:
                    ParseDestroyEntity(reader);
                    break;
                case MCPacketOpcodes.AddVehicle:
                    ParseAddVehicle(reader);
                    break;
                case MCPacketOpcodes.ArmAnimation:
                    ParseArmAnimation(reader);
                    break;
                case MCPacketOpcodes.BlockChange:
                    ParseBlockChange(reader);
                    break;
                case MCPacketOpcodes.HoldSwitch:
                    ParseHoldSwitch(reader);
                    break;
                case MCPacketOpcodes.EntityTeleport:
                    ParseEntityTeleport(reader);
                    break;
                case MCPacketOpcodes.MultiBlockChange:
                    ParseMultiBlockChange(reader);
                    break;
                case MCPacketOpcodes.MapChunk:
                    ParseMapChunk(reader);
                    break;
                case MCPacketOpcodes.CollectItem:
                    ParseCollectItem(reader);
                    break;
                case MCPacketOpcodes.ComplexEntities:
                    ParseComplexEntities(reader);
                    break;
                case MCPacketOpcodes.AddInventory:
                    break;
                default:
                    if (OnUnknownPacket != null)
                        OnUnknownPacket(this, new UnknownPacketEventArgs(opcode, reader));

                    m_Socket.Disconnect("MCBot recieved unknown packet. Aborting connection.");
                    break;
            }
        }

        #region Sending
        public void Handshake(string username)
        {
            SendPacket(MCPackets.CreateHandshakePacket(username));
        }

        public void Login(string username, string password)
        {
            SendPacket(MCPackets.CreateLoginPacket(username, password));
        }

        public void Chat(string message)
        {
            SendPacket(MCPackets.CreateChatPacket(message));
        }

        public void Heartbeat()
        {
            SendPacket(MCPackets.CreateHeartbeatPacket());
        }

        public void MoveLook(Point3D newposition, double stance, float yaw, float pitch, bool ground)
        {
            if (stance - newposition.Y < 0.1 || stance - newposition.Y > 1.65) // Illegal stance
                throw new Exception("Attempted to send an illegal stance");

            SendPacket(MCPackets.CreatePlayerMoveLookPacket(newposition.X, newposition.Y, stance, newposition.Z, yaw, pitch, ground));
        }

        public void UpdateInventory(MCInventoryType type, MCItem[] items)
        {
            SendPacket(MCPackets.CreatePlayerInventoryPacket(type, items));
        }

        public void Tick(bool ground)
        {
            SendPacket(MCPackets.CreatePlayerTickPacket(ground));
        }

        public void Move(Point3D newposition, double stance, bool ground)
        {
            if (stance - newposition.Y < 0.1 || stance - newposition.Y > 1.65) // Illegal stance
                throw new Exception("Attempted to send an illegal stance");

            SendPacket(MCPackets.CreatePlayerPositionPacket(newposition.X, newposition.Y, stance, newposition.Z, ground));
        }

        public void Look(float yaw, float pitch, bool ground)
        {
            SendPacket(MCPackets.CreatePlayerLookPacket(yaw, pitch, ground));
        }

        public void Dig(MCDigState status, Point3D position, MCBlockFace face)
        {
            SendPacket(MCPackets.CreatePlayerDiggingPacket((byte)status, (int)position.X, (byte)position.Y, (int)position.Z, (byte)face));
        }

        public void PlaceBlock(MCBlockType block, Point3D position, MCBlockFace direction)
        {
            SendPacket(MCPackets.CreatePlayerBlockPlacePacket((short)block, (int)position.X, (byte)position.Y, (int)position.Z, (byte)direction));
        }

        public void ChangeHolding(MCBlockType item)
        {
            SendPacket(MCPackets.CreatePlayerHoldingPacket(0, (short)item));
        }

        public void ArmAnimation(bool animate)
        {
            SendPacket(MCPackets.CreateArmAnimationPacket(0, animate));
        }
        #endregion
        #endregion
    }
}
