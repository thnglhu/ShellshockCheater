using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellshockCheater
{
    public struct Vector
    {
        public float x, y;
        
        public Vector(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"x: {this.x}, y: {this.y}";
        }

        public static Vector operator ^(Vector A, float angle)
        {
            float new_x = (float)(A.x * Math.Cos(angle) - A.y * Math.Sin(angle));
            float new_y = (float)(A.x * Math.Sin(angle) - A.y * Math.Cos(angle));
            return new Vector(new_x, new_y);
        }

        public static Vector operator +(Vector A, Vector B)
        {
            return new Vector(A.x + B.x, A.y + B.y);
        }

        public static Vector operator -(Vector A, Vector B)
        {
            return new Vector(A.x - B.x, A.y - B.y);
        }

        public static Vector operator *(float k, Vector A)
        {
            return new Vector(k * A.x, k * A.y);
        }

        public static Vector operator *(Vector A, float k)
        {
            return k * A;
        }
        
        public static float distance(Vector A, Vector B)
        {
            return (float)Math.Sqrt(Math.Pow(A.x - B.x, 2.0) + Math.Pow(A.y - B.y, 2.0));
        }
    }
}
