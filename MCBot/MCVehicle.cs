using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.Jonyleeson.Generic;

namespace Org.Jonyleeson.MCBot
{
    public class MCVehicle : MCEntity
    {
        public MCVehicleType Type
        { get; set; }

        public MCVehicle()
            : this(0, new Point3D(), MCVehicleType.Boat) // defaults to boat. fucktards should just specify a bloody vehicle type anyway
        { }

        public MCVehicle(int id)
            : this(id, new Point3D(), MCVehicleType.Boat)
        { }

        public MCVehicle(int id, Point3D position)
            : this(id, position, MCVehicleType.Boat)
        { }

        public MCVehicle(int id, Point3D position, MCVehicleType type)
        {
            ID = id;
            Position = position;
            Type = type;
        }
    }
}
