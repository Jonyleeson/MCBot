using System;

namespace Org.Jonyleeson.MCBot
{
    public class MCBlock
    {
        public MCBlockType Type
        { get; set; }

        public byte MetaData
        { get; set; }

        public byte Light
        { get; set; }

        public byte SkyLight
        { get; set; }

        public MCBlock()
            : this(MCBlockType.Air, 0, 0, 0)
        { }

        public MCBlock(MCBlockType type, byte metadata, byte light, byte skylight)
        {
            Type = type;
            MetaData = metadata;
            Light = light;
            SkyLight = skylight;
        }
    }
}
