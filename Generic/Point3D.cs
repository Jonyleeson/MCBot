using System;

namespace Org.Jonyleeson.Generic
{
    public class Point3D
    {
        public double X
        { get; set; }
        
        public double Y
        { get; set; }
        
        public double Z
        { get; set; }

        public Point3D()
        {
            X = Y = Z = 0;
        }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D(int x, int y, int z)
        {
            X = (double)x;
            Y = (double)y;
            Z = (double)z;
        }

        public Point3D(float x, float y, float z)
        {
            X = (double)x;
            Y = (double)y;
            Z = (double)z;
        }

        public static Point3D operator +(Point3D p1, Vector3D v1)
        {
            return new Point3D(p1.X + v1.X, p1.Y + v1.Y, p1.Z + v1.Z);
        }

        public static Point3D operator -(Point3D p1, Vector3D v1)
        {
            return new Point3D(p1.X - v1.X, p1.Y - v1.Y, p1.Z - v1.Z);
        }
    }
}
