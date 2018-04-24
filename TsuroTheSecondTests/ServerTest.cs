using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TsuroTheSecond;

namespace TsuroTheSecondTests
{
    [TestClass]
    public class ServerTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Server server = new Server(4);
            Assert.AreEqual(4, server.alive.Count);
            Assert.AreEqual(0, server.dead.Count);

        }

        [TestMethod]
        public void TestDraw()
        {
        }

        [TestMethod]
        public void TestDrawTooManyCards()
        {
            
        }

        [TestMethod]
        public void TestSetInitialMarkers()
        {
        }

        [TestMethod]
        public void TestInitGame()
        {
        }

        [TestMethod]
        public void TestLegalPlay()
        {
        }

        [TestMethod]
        public void TestPlayTurn()
        {
        }
    }
}
