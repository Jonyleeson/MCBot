using System;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public class MCBlockTransform
    {
        public Vector3D RelativePosition
        { get; private set; }

        public MCBlockType Block
        { get; private set; }

        public byte MetaData
        { get; private set; }

        public MCBlockTransform(Vector3D pos, byte type, byte metadata)
        {
            RelativePosition = pos;
            Block = (MCBlockType)type;
            MetaData = metadata;
        }
    }
}
