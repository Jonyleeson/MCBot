using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public class MCPlayer : MCEntity
    {
        public string Name
        { get; set; }

        public float Yaw
        { get; set; }

        public float Pitch
        { get; set; }

        public MCBlockType Item
        { get; set; }

        public MCPlayer()
            : this(0, new Point3D(), "", 0f, 0f, MCBlockType.Air)
        { }

        public MCPlayer(int id)
            : this(id, new Point3D(), "", 0f, 0f, MCBlockType.Air)
        { }

        public MCPlayer(int id, Point3D position)
            : this(id, position, "", 0f, 0f, MCBlockType.Air)
        { }

        public MCPlayer(int id, Point3D position, string name)
            : this(id, position, name, 0f, 0f, MCBlockType.Air)
        { }

        public MCPlayer(int id, Point3D position, string name, float yaw, float pitch)
            : this(id, position, name, yaw, pitch, MCBlockType.Air)
        { }

        public MCPlayer(int id, Point3D position, string name, float yaw, float pitch, MCBlockType item)
            : base(id, position)
        {
            Name = name;
            Yaw = yaw;
            Pitch = pitch;
            Item = item;
        }
    }
}
