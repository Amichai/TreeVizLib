using ComputationalPhysics.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalPhysics {
    /// <summary>
    /// We take g to 9.81 m/s^2
    /// </summary>
    public static class NoFrictionProjectile {
        private static double g = 9.81;

        public static void Angles(Vec2 target, double initialSpeed, out double angle1, out double angle2){
            double v0 = initialSpeed;
            double x = target.X;
            double y = target.Y;

            var discriminant = v0.Pow(4) - g * (g * x.Sqrd() + 2 * y * v0.Sqrd());
            if (discriminant > 0) {
                angle1 = Math.Atan2(v0.Sqrd() + Math.Sqrt(discriminant), g * x);
                angle2 = Math.Atan2(v0.Sqrd() - Math.Sqrt(discriminant), g * x);
            } else {
                angle1 = double.NaN;
                angle2 = double.NaN;
            }
        }

        public static double InitialSpeed(Vec2 target, double angle, double vMax) {
            Func<double, double> toZero1 = v0 => {
                double a1, a2;
                Angles(target, v0, out a1, out a2);
                return a1 - angle;
            };
            Func<double, double> toZero2 = v0 => {
                double a1, a2;
                Angles(target, v0, out a1, out a2);
                return a2 - angle;
            };

            int counter;
            try {
                var start = toZero1.DomainStart(0, vMax, out counter, 1e-6);
                var r1 = toZero1.Zero(start + .001, vMax, out counter);
                if (!double.IsNaN(r1)) {
                    return r1;
                } else {
                    start = toZero2.DomainStart(0, vMax, out counter, 1e-6);
                    return toZero2.Zero(start + .001, vMax, out counter);
                }
            } catch {
                return double.NaN;
            }
        }

        /// <summary>Distance in the x direction</summary>
        public static double Range(double initialSpeed, double angle) {
            double v0 = initialSpeed;
            return (v0.Sqrd() / g) * Math.Sin(2 * angle);
        }

        /// <summary>Distance in the x direction</summary>
        public static double Range(Vec2 initialVelocity) {
            return Range(initialVelocity.Mag, initialVelocity.Theta);
        }

        public static double MaxHeight(double initialSpeed, double angle) {
            double v0 = initialSpeed;
            return (v0.Sqrd() * Math.Sin(angle).Sqrd()) / (2 * g);
        }

        public static double MaxHeight(Vec2 initialVelocity) {
            return MaxHeight(initialVelocity.Mag, initialVelocity.Theta);
        }

        public static double AngleForTimeOfFlight(double initialSpeed, double timeOfFlight) {
            Func<double, double> toZero = (a) => TimeOfFlight(initialSpeed, a) - timeOfFlight;
            int counter;
            try {
                return toZero.Zero(0, Math.PI / 2, out counter);
            } catch {
                return double.NaN;
            }
        }

        public static double TimeOfFlight(double initialSpeed, double angle) {
            double vy0 = initialSpeed * Math.Sin(angle);
            var result = 2 * vy0 / g;
            return result;
        }

        public static double TimeOfFlight(Vec2 initialVelocity) {
            return TimeOfFlight(initialVelocity.Mag, initialVelocity.Theta);
        }
    }
}
