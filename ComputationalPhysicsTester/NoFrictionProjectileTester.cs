using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComputationalPhysics;
using ComputationalPhysics.Primitives;
using System.Diagnostics;

namespace ComputationalPhysicsTester {
    [TestClass]
    public class NoFrictionProjectileTester {
        [TestMethod]
        public void TestGetAngles() {
            Vec2 target = new Vec2(10, 20);
            double angle1, angle2;
            NoFrictionProjectile.Angles(target, 25, out angle1, out angle2);
            Debug.Print(string.Format("Angle1: {0}, angle2: {1}", angle1 * 180 / Math.PI, angle2 * 180 / Math.PI));

            NoFrictionProjectile.Angles(target, 20, out angle1, out angle2);
            Debug.Print(string.Format("Angle1: {0}, angle2: {1}", angle1 * 180 / Math.PI, angle2 * 180 / Math.PI));

            NoFrictionProjectile.Angles(target, 35, out angle1, out angle2);
            Debug.Print(string.Format("Angle1: {0}, angle2: {1}", angle1 * 180 / Math.PI, angle2 * 180 / Math.PI));
        }

        [TestMethod]
        public void TestMaxDistance() {
            var range = NoFrictionProjectile.Range(20, Math.PI / 4);
            Debug.Print(string.Format("Range: {0}", range));
            range = NoFrictionProjectile.Range(20, Math.PI / 5);
            Debug.Print(string.Format("Range: {0}", range));
            range = NoFrictionProjectile.Range(20, Math.PI / 6);
            Debug.Print(string.Format("Range: {0}", range));
            
        }

        [TestMethod]
        public void InitialSpeedTest() {
            var target = new Vec2(8, 2);
            //var target = new Vec2(2, 8);
            var v0 = NoFrictionProjectile.InitialSpeed(target, Math.PI / 4, 100);
            Debug.Print(string.Format("v0: {0}", v0));
        }
    }
}
