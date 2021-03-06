﻿using System;

namespace Org.Jonyleeson.MCBot
{
    // Oh my lord this was such a bitch to type out
    public enum MCBlockType : short
    {
        None = -1,

        // Blocks
        Air = 0,
        Stone = 1,
        Grass = 2,
        Dirt = 3,
        Cobblestone = 4,
        Wood = 5,
        Sapling = 6,
        Bedrock = 7,
        Water = 8,
        StationaryWater = 9,
        Lava = 10,
        StationaryLava = 11,
        Sand = 12,
        Gravel = 13,
        GoldOre = 14,
        IronOre = 15,
        CoalOre = 16,
        Log = 17,
        Leaves = 18,
        Sponge = 19,
        Glass = 20,
        WhiteCloth = 35,
        YellowFlower = 37,
        RedRose = 38,
        BrownMushroom = 39,
        RedMushroom = 40,
        GoldBlock = 41,
        IronBlock = 42,
        DoubleStep = 43,
        Step = 44,
        Brick = 45,
        TNT = 46,
        Bookcase = 47,
        MossyCobblestone = 48,
        Obsidian = 49,
        Torch = 50,
        Fire = 51,
        MobSpawner = 52,
        WoodenStairs = 53,
        Chest = 54,
        RedstoneWire = 55,
        DiamondOre = 56,
        DiamondBlock = 57,
        WorkBench = 58,
        Crops = 59,
        Soil = 60,
        Furnace = 61,
        BurningFurnace = 62,
        SignPost = 63,
        WoodenDoor = 64,
        Ladder = 65,
        MinecartTracks = 66,
        CobblestoneStairs = 67,
        WallSign = 68,
        Level = 69,
        StonePressurePlate = 70,
        IronDoor = 71,
        WoodenPressurePlate = 72,
        RedstoneOre = 73,
        GlowingRedstoneOre = 74,
        RedstoneTorchOff = 75,
        RedstoneTorchOn = 76,
        StoneButton = 77,
        Snow = 78,
        Ice = 79,
        SnowBlock = 80,
        Cactus = 81,
        Clay = 82,
        Reed = 83,
        Jukebox = 84,
        Fence = 85,

        // Items
        IronSpade = 256,
        IronPickaxe = 257,
        IronAxe = 258,
        FlintAndSteel = 259,
        Apple = 260,
        Bow = 261,
        Arrow = 262,
        Coal = 263,
        Diamond = 264,
        IronLingot = 265,
        GoldLingot = 266,
        IronSword = 267,
        WoodenSword = 268,
        WoodenSpade = 269,
        WoodenPickaxe = 270,
        WoodenAxe = 271,
        StoneSword = 272,
        StoneSpade = 273,
        StonePickaxe = 274,
        StoneAxe = 275,
        DiamondSword = 276,
        DiamondSpade = 277,
        DiamondPickaxe = 278,
        DiamondAxe = 279,
        Stick = 280,
        Bowl = 281,
        MushroomSoup = 282,
        GoldSword = 283,
        GoldSpade = 284,
        GoldPickaxe = 285,
        GoldAxe = 286,
        String = 287,
        Feather = 288,
        Gunpowder = 289,
        WoodenHoe = 290,
        StoneHoe = 291,
        IronHoe = 292,
        DiamondHoe = 293,
        GoldHoe = 294,
        Seeds = 295,
        Wheat = 296,
        Bread = 297,
        LeatherHelmet = 298,
        LeatherChestplate = 299,
        LeatherPants = 300,
        LeatherBoots = 301,
        ChainmailHelmet = 302,
        ChainmailChestplate = 303,
        ChainmailPants = 304,
        ChainmailBoots = 305,
        IronHelmet = 306,
        IronChestplate = 307,
        IronPants = 308,
        IronBoots = 309,
        DiamondHelmet = 310,
        DiamondChestplate = 311,
        DiamondPants = 312,
        DiamondBoots = 313,
        GoldHelmet = 314,
        GoldChestplate = 315,
        GoldPants = 316,
        GoldBoots = 317,
        Flint = 318,
        Pork = 319,
        GrilledPork = 320,
        Paintings = 321,
        GoldenApple = 322,
        Sign = 323,
        WoodenDoorItem = 324,
        Bucket = 325,
        WaterBucket = 326,
        LavaBucket = 327,
        Minecart = 328,
        Saddle = 329,
        IronDoorItem = 330,
        Redstone = 331,
        Snowball = 332,
        Boat = 333,
        Leather = 334,
        MilkBucket = 335,
        ClayBrick = 336,
        ClayBalls = 337,
        ReedItem = 338,
        Paper = 339,
        Book = 340,
        SlimeBall = 341,
        StorageMinecart = 342,
        PoweredMinecart = 343,
        Egg = 344,
        Compass = 345,
        FishingRod = 346,

        // Records
        GoldRecord = 2256,
        GreenRecord = 2257
    }
}
