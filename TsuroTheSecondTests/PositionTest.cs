using System;
using TsuroTheSecond;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TsuroTheSecondTests
{
    [TestClass]
    public class PositionTest
    {
        [TestMethod]
        public void TestConstructor() {
            // top
            Position position = new Position(0, -1, 5);
            Assert.AreEqual(0, position.x);
            Assert.AreEqual(-1, position.y);
            Assert.AreEqual(5, position.port);
        }

        [TestMethod]
        public void TestConstructor2()
        {
            // right
            Position position = new Position(6, 0, 6);
            Assert.AreEqual(6, position.x);
            Assert.AreEqual(0, position.y);
            Assert.AreEqual(6, position.port);
        }

        [TestMethod]
        public void TestConstructor3()
        {
            // bottom 
            Position position = new Position(0, 6, 1);
            Assert.AreEqual(0, position.x);
            Assert.AreEqual(6, position.y);
            Assert.AreEqual(1, position.port);
        }

        [TestMethod]
        public void TestConstructor4()
        {
            // left 
            Position position = new Position(-1, 0, 3);
            Assert.AreEqual(-1, position.x);
            Assert.AreEqual(0, position.y);
            Assert.AreEqual(3, position.port);
        }

        // we can write more tests for illegal positions
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Illegal position to initialize player")]
        public void TestIllegalPosition()
        {
            // inside board
            Position position = new Position(0, 2, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Illegal position to initialize player")]
        public void TestIllegalPosition2()
        {
            Position position = new Position(-1, -1, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Illegal position to initialize player")]
        public void TestIllegalPosition3()
        {
            Position position = new Position(-1, 0, 7);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Illegal position to initialize player")]
        public void TestIllegalPosition4()
        {
            // illegal edge position
            Position position = new Position(0, 0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Illegal position to initialize player")]
        public void TestIllegalPosition5()
        {
            // illegal edge position
            Position position = new Position(0, 0, 4);
        }
    }
}
