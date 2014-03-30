using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalPhysics.Primitives {
    public struct Vec2 {        
        public Vec2(double x, double y) : this() {
            this.X = x;
            this.Y = y;
        }

        public double X { get; private set; }
        public double Y { get; private set; }

        public double Mag {
            get {
                return Math.Sqrt(this.X * this.X + this.Y * this.Y);
            }
        }

        public double Theta {
            get {
                return Math.Atan2(this.Y, this.X);
            }
        }
    }
}
