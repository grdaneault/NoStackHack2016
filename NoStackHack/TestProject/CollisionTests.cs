using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoStackHack.Utilities;

namespace TestProject
{
    [TestClass]
    public class CollisionTests
    {
        [TestMethod]
        public void SimpleNo()
        {
            var a = new Box(0, 0, 1, 1);
            var b = new Box(10, 10, 1, 1);

            var info = CollisionHelper.CollisionInfo(a, b);

            Assert.IsFalse(info.IsColliding);
        }
    }
}
