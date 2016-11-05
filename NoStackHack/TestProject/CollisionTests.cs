using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoStackHack.Utilities;
using Microsoft.Xna.Framework;

namespace TestProject
{
    [TestClass]
    public class CollisionTests
    {
        public Microsoft.Xna.Framework.Vector2 Vector2 { get; private set; }

        [TestMethod]
        public void Simple_No()
        {
            var a = new Box(0, 0, 1, 1);
            var b = new Box(10, 10, 1, 1);

            var info = CollisionHelper.CollisionInfo(a, b);

            Assert.IsFalse(info.IsColliding);
        }

        [TestMethod]
        public void Simple_AIsInB()
        {
            var a = new Box(50,50,25,25);
            var b = new Box(10,10,100,100);

            var info = CollisionHelper.CollisionInfo(a, b);

            Assert.IsTrue(info.IsColliding);
        }

        [TestMethod]
        public void Simple_BIsInA()
        {
            var a = new Box(10, 10, 100, 100);
            var b = new Box(50, 50, 25, 25);

            var info = CollisionHelper.CollisionInfo(a, b);

            Assert.IsTrue(info.IsColliding);
        }

        [TestMethod]
        public void Simple_AOverlapsB()
        {
            var a = new Box(10, 10, 100, 100);
            var b = new Box(50, 50, 100, 100);

            var info = CollisionHelper.CollisionInfo(a, b);

            Assert.IsTrue(info.IsColliding);
        }

        [TestMethod]
        public void Simple_BOverlapsA()
        {
            var b = new Box(10, 10, 100, 100);
            var a = new Box(50, 50, 100, 100);

            var info = CollisionHelper.CollisionInfo(a, b);

            Assert.IsTrue(info.IsColliding);
        }

        [TestMethod]
        public void Normal_ShouldBeUnitX()
        {
            var a = new Box(0, 0, 100, 100);
            var b = new Box(80, 20, 50, 60);

            var info = CollisionHelper.CollisionInfo(a, b);

            Assert.IsTrue(info.IsColliding);
            Assert.AreEqual(info.Normal, Vector2.UnitX);
            Assert.AreEqual(20, info.Overlap);
        }

        [TestMethod]
        public void Normal_ShouldBeNegativeUnitX()
        {
            var a = new Box(100, 100, 100, 100);
            var b = new Box(80, 120, 50, 60);

            var info = CollisionHelper.CollisionInfo(a, b);

            Assert.IsTrue(info.IsColliding);
            Assert.AreEqual(info.Normal, -Vector2.UnitX);
            Assert.AreEqual(30, info.Overlap);
        }

        [TestMethod]
        public void Normal_ShouldBeUnitY()
        {
            var a = new Box(100, 100, 100, 100);
            var b = new Box(120, 180, 60, 50);

            var info = CollisionHelper.CollisionInfo(a, b);

            Assert.IsTrue(info.IsColliding);
            Assert.AreEqual(info.Normal, Vector2.UnitY);
            Assert.AreEqual(20, info.Overlap);
        }

        [TestMethod]
        public void Normal_ShouldBeNegativeUnitY()
        {
            var a = new Box(100, 100, 100, 100);
            var b = new Box(120, 80, 60, 50);

            var info = CollisionHelper.CollisionInfo(a, b);

            Assert.IsTrue(info.IsColliding);
            Assert.AreEqual(info.Normal, -Vector2.UnitY);
            Assert.AreEqual(30, info.Overlap);
        }

        [TestMethod]
        public void UseCase_Ceiling_NO()
        {
            var a = new Box(0,0,1920,50);
            var b = new Box(500, 500, 50, 50);

            var info = CollisionHelper.CollisionInfo(a, b);

            Assert.IsFalse(info.IsColliding);
            //Assert.AreEqual(info.Normal, -Vector2.UnitY);
            //Assert.AreEqual(30, info.Overlap);
        }

        [TestMethod]
        public void UseCase_Ceiling_YES()
        {
            var a = new Box(0, 0, 1920, 50);
            var b = new Box(500, 40, 50, 50);

            var info = CollisionHelper.CollisionInfo(a, b);

            Assert.IsTrue(info.IsColliding);
            Assert.AreEqual(info.Normal, Vector2.UnitY);
            //Assert.AreEqual(30, info.Overlap);
        }
    }
}
