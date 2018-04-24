using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TsuroTheSecond;

namespace TsuroTheSecondTests
{
    [TestClass]
    public class ServerTest
    {

        Server server = new Server();

        public void AddPlayersToServer() {
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();

            server.AddPlayer(p1, 12, "blue");
            server.AddPlayer(p2, 10, "green");
        }

        [TestMethod]
        public void TestConstructor()
        {
            Assert.AreEqual(0, server.alive.Count);
            Assert.AreEqual(0, server.dead.Count);
        }

        [TestMethod]
        public void TestConstructor2Player()
        {
            AddPlayersToServer();

            Assert.AreEqual(2, server.alive.Count);
            Assert.AreEqual(0, server.dead.Count);
        }

        [TestMethod]
        public void TestDraw()
        {
            AddPlayersToServer();

            Assert.AreEqual(0, server.alive[0].Hand.Count);
            Assert.AreEqual(35, server.deck.Count);
            server.DrawTile(server.alive[0], server.deck);
            Assert.AreEqual(1, server.alive[0].Hand.Count);
            Assert.AreEqual(34, server.deck.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Player can't have more than 3 cards in hand")]
        public void TestDrawTooManyTiles()
        {
            AddPlayersToServer();

            server.DrawTile(server.alive[0], server.deck);
            server.DrawTile(server.alive[0], server.deck);
            server.DrawTile(server.alive[0], server.deck);
            server.DrawTile(server.alive[0], server.deck);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Player can't have more than 3 cards in hand")]
        public void TestDrawTooManyTilesTilesSet()
        {
            // tiles were externally set
            AddPlayersToServer();

            server.alive[0].Hand = new List<Tile>{
                new Tile(1, new List<int>{1, 2, 3, 4, 5, 6, 7, 0}),
                new Tile(1, new List<int>{1, 2, 3, 4, 5, 6, 7, 0}),
                new Tile(1, new List<int>{1, 2, 3, 4, 5, 6, 7, 0}),
            };

            server.DrawTile(server.alive[0], server.deck);
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
