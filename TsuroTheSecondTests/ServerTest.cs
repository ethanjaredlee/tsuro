using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TsuroTheSecond;

namespace TsuroTheSecondTests
{
    [TestClass]
    public class ServerTest
    {

        Server server = new Server();

        public void Add2MPlayersToServer() {
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();

            server.AddPlayer(p1, 12, "blue");
            server.AddPlayer(p2, 10, "green");
        }

        public void Add4MPlayersToServer()
        {
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();
            MPlayer p3 = new MPlayer();
            MPlayer p4 = new MPlayer();

            server.AddPlayer(p1, 12, "blue");
            server.AddPlayer(p2, 10, "green");
            server.AddPlayer(p3, 20, "pink");
            server.AddPlayer(p4, 30, "red");
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
            Add2MPlayersToServer();

            Assert.AreEqual(2, server.alive.Count);
            Assert.AreEqual(0, server.dead.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Only 8 players allowed in a game")]
        public void Test9PlayerGame()
        {
            List<string> colors = new List<string>{
                "red",
                "blue",
                "green",
                "yellow",
                "purple",
                "pink",
                "black",
                "white",
                "orange",
            };

            for (int i = 0; i < 8; i++) {
                server.AddPlayer(new MPlayer(), 10, colors[i]);
            }

            // case that breaks
            server.AddPlayer(new MPlayer(), 10, colors[8]);
        }

        [TestMethod]
        public void TestDraw()
        {
            Add2MPlayersToServer();

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
            Add2MPlayersToServer();

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
            Add2MPlayersToServer();

            server.alive[0].Hand = new List<Tile>{
                new Tile(1, new List<int>{1, 2, 3, 4, 5, 6, 7, 0}),
                new Tile(1, new List<int>{1, 2, 3, 4, 5, 6, 7, 0}),
                new Tile(1, new List<int>{1, 2, 3, 4, 5, 6, 7, 0}),
            };

            server.DrawTile(server.alive[0], server.deck);
        }

        [TestMethod]
        public void TestDragonTile()
        {
            Add4MPlayersToServer();

            // manually shorten deck to 5 cards
            server.deck = server.deck.GetRange(0, 5);
            server.DrawTile(server.alive[0], server.deck);
            server.DrawTile(server.alive[1], server.deck);
            server.DrawTile(server.alive[2], server.deck);
            server.DrawTile(server.alive[3], server.deck);
            server.DrawTile(server.alive[0], server.deck);
            Assert.AreEqual(0, server.deck.Count);
            Assert.AreEqual(0, server.dragonQueue.Count);

            server.DrawTile(server.alive[1], server.deck);
            Assert.AreEqual(1, server.dragonQueue.Count);
            //Assert.AreEqual(server.alive[1], server.dragonQueue[0]);
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
