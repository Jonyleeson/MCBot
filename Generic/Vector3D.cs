using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.Jonyleeson.Generic
{
    public class Vector3D
    {
        public double X
        { get; set; }
        public double Y
        { get; set; }
        public double Z
        { get; set; }

        public Vector3D()
        {
            X = Y = Z = 0;
        }

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3D(int x, int y, int z)
        {
            X = (double)x;
            Y = (double)y;
            Z = (double)z;
        }

        public Vector3D(float x, float y, float z)
        {
            X = (double)x;
            Y = (double)y;
            Z = (double)z;
        }

        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
    }
}
