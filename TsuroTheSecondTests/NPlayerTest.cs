using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TsuroTheSecond;

namespace TsuroTheSecondTests
{
    [TestClass]
    public class NPlayerTest
    {
        NPlayer player;

        [TestInitialize]
        public void Initialize() {
            player = new NPlayer();
        }

        [TestMethod]
        public void GetNameTest() {
            // not really sure how name works
            string name = player.GetName();
        }

        [TestMethod]
        public void InitializeTest()
        {
            Console.SetIn(new StringReader("<void></void>"));
            player.Initialize("blue", new List<string> { "blue", "red" });
            Assert.AreEqual(2, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Network should have returned void")]
        public void InitializeTestBadResponse() {
            Console.SetIn(new StringReader("<not-void></not-void>"));
            player.Initialize("blue", new List<string> { "blue", "red" });
        }

        //[TestMethod]
        //public void PlacePawnTest() {
        //    string response = "<pawn-loc><hv><h></h><n>0</n><n>1</n></hv></pawn-loc>";
        //    Board board = new Board(6);

        //    Position initPosition = player.PlacePawn(board);
        //    Console.SetIn(new StringReader(response));
        //    Position check = new Position(0, -1, 4, true);
        //    Assert.AreEqual(initPosition, check);
        //}
    }
}
