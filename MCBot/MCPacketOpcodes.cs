using System;

namespace Org.Jonyleeson.MCBot
{
    public enum MCPacketOpcodes : byte
    {
        KeepAlive = 0,
        Login = 1,
        Handshake = 2,
        Chat = 3,
        UpdateTime = 4,
        PlayerInventory = 5,
        SpawnPosition = 6,
        PlayerTick = 0xA,
        PlayerPosition = 0xB,
        PlayerLook = 0xC,
        PlayerMoveLook = 0xD,
        BlockDig = 0xE,
        BlockPlace = 0xF,
        HoldSwitch = 0x10,
        AddInventory = 0x11,
        ArmAnimation = 0x12,
        NamedEntitySpawn = 0x14,
        ItemSpawn = 0x15,
        CollectItem = 0x16,
        AddVehicle = 0x17,
        MobSpawn = 0x18,
        DestroyEntity = 0x1D,
        Entity = 0x1E,
        RelativeEntityMove = 0x1F,
        EntityLook = 0x20,
        RelativeEntityMoveLook = 0x21,
        EntityTeleport = 0x22,
        PreChunk = 0x32,
        MapChunk = 0x33,
        MultiBlockChange = 0x34,
        BlockChange = 0x35,
        ComplexEntities = 0x3B,
        Disconnect = 0xFF
    }
}
