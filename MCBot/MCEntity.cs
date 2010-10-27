using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public class MCEntity
    {
        public int ID
        { get; set; }

        public Point3D Position
        { get; set; }

        public MCEntity()
            : this(0, new Point3D())
        { }

        public MCEntity(int id)
            : this(id, new Point3D())
        { }

        public MCEntity(int id, Point3D pos)
        {
            ID = id;
            Position = pos;
        }
    }
}
