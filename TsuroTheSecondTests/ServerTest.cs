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
        [TestMethod]
        public void TestConstructor()
        {
            Server server = new Server();
            Assert.AreEqual(0, server.alive.Count);
            Assert.AreEqual(0, server.dead.Count);
        }

        [TestMethod]
        public void TestConstructor2Player()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();

            server.AddPlayer(p1, 12, "blue");
            server.AddPlayer(p2, 10, "green");

            Assert.AreEqual(2, server.alive.Count);
            Assert.AreEqual(0, server.dead.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Only 8 players allowed in a game")]
        public void Test9PlayerGame()
        {
            Server server = new Server();
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
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();
            server.AddPlayer(p1, 12, "blue");
            server.AddPlayer(p2, 10, "green");

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
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();
            server.AddPlayer(p1, 12, "blue");
            server.AddPlayer(p2, 10, "green");

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
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();
            server.AddPlayer(p1, 12, "blue");
            server.AddPlayer(p2, 10, "green");

            server.alive[0].Hand = new List<Tile>{
                new Tile(1, new List<int>{1, 2, 3, 4, 5, 6, 7, 0}),
                new Tile(1, new List<int>{1, 2, 3, 4, 5, 6, 7, 0}),
                new Tile(1, new List<int>{1, 2, 3, 4, 5, 6, 7, 0}),
            };

            server.DrawTile(server.alive[0], server.deck);
        }

        [TestMethod]
        public void TestKillPlayer()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();
            MPlayer p3 = new MPlayer();
            MPlayer p4 = new MPlayer();

            server.AddPlayer(p1, 12, "blue");
            server.AddPlayer(p2, 10, "green");
            server.AddPlayer(p3, 20, "pink");
            server.AddPlayer(p4, 30, "red");

            Assert.AreEqual(0, server.alive[0].Hand.Count);
            Assert.AreEqual(35, server.deck.Count);
            server.DrawTile(server.alive[0], server.deck);
            Assert.AreEqual(0, server.dead.Count);
            Assert.AreEqual(4, server.alive.Count);
            Assert.AreEqual(34, server.deck.Count);

            server.KillPlayer(server.alive[0]);
            Assert.AreEqual(1, server.dead.Count);
            Assert.AreEqual(3, server.alive.Count);
            Assert.AreEqual(35, server.deck.Count);
        }

        [TestMethod]
        public void TestDragonTile()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();
            MPlayer p3 = new MPlayer();
            MPlayer p4 = new MPlayer();

            server.AddPlayer(p1, 12, "blue");
            server.AddPlayer(p2, 10, "green");
            server.AddPlayer(p3, 20, "pink");
            server.AddPlayer(p4, 30, "red");

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
            Assert.AreEqual(server.alive[1], server.dragonQueue[0]);

            // kill off player 2, it's cards should go to player 1 because
            // p1 has the dragon tile
            Assert.AreEqual(1, server.alive[2].Hand.Count);
            server.KillPlayer(server.alive[2]);
            Assert.AreEqual(1, server.dead.Count);
            Assert.AreEqual(0, server.dragonQueue.Count);
            Assert.AreEqual(2, server.alive[1].Hand.Count);
        }

        [TestMethod]
        public void TestKillYourselfWithDragonTile()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();
            MPlayer p3 = new MPlayer();
            MPlayer p4 = new MPlayer();

            server.AddPlayer(p1, 12, "blue");
            server.AddPlayer(p2, 10, "green");
            server.AddPlayer(p3, 20, "pink");
            server.AddPlayer(p4, 30, "red");

            server.deck = server.deck.GetRange(0, 5);
            server.DrawTile(server.alive[0], server.deck);
            server.DrawTile(server.alive[1], server.deck);
            server.DrawTile(server.alive[2], server.deck);
            server.DrawTile(server.alive[3], server.deck);
            server.DrawTile(server.alive[0], server.deck);
            // player 1 has the dragon tile
            server.DrawTile(server.alive[1], server.deck);
            server.KillPlayer(server.alive[1]);
            Assert.AreEqual(1, server.deck.Count);
            Assert.AreEqual(0, server.dragonQueue.Count);
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
        public void TestLegalPlayFalse3()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            server.AddPlayer(p1, 12, "blue");

            Tile testTile1 = new Tile(1, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile2 = new Tile(2, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile3 = new Tile(3, new List<int>(8) {
                0, 5, 1, 4, 2, 7, 3, 6,
            });

            Player p_1 = server.alive[0];
            Board board = new Board(6);
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            Assert.IsTrue(server.LegalPlay(p_1, board, testTile1));
        }
        [TestMethod]
        public void TestLegalPlayFalse2()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            server.AddPlayer(p1, 12, "blue");
            Tile testTile1 = new Tile(1, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile3 = new Tile(3, new List<int>(8) {
                0, 5, 1, 4, 2, 7, 3, 6,
            });

            Player p_1 = server.alive[0];
            Board board = new Board(6);
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile1, testTile3 };
            Assert.IsTrue(server.LegalPlay(p_1, board, testTile1));
        }

        [TestMethod]
        public void TestLegalPlayTrue3()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();

            server.AddPlayer(p1, 12, "blue");

            Tile testTile1 = new Tile(1, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile2 = new Tile(2, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile3 = new Tile(3, new List<int>(8) {
                0, 5, 1, 4, 2, 7, 3, 6,
            });

            Player p_1 = server.alive[0];
            Board board = new Board(6);
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            Assert.IsTrue(server.LegalPlay(p_1, board, testTile3));
        }


        [TestMethod]
        public void TestLegalPlayTrue2()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            server.AddPlayer(p1, 12, "blue");
            Tile testTile1 = new Tile(1, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile3 = new Tile(3, new List<int>(8) {
                0, 5, 1, 4, 2, 7, 3, 6,
            });

            Player p_1 = server.alive[0];
            Board board = new Board(6);
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile1, testTile3 };
            Assert.IsTrue(server.LegalPlay(p_1, board, testTile3));
        }
        [TestMethod]
        public void TestLegalPlayTrue1_t()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();

            server.AddPlayer(p1, 12, "blue");
            Tile testTile3 = new Tile(3, new List<int>(8) {
                0, 5, 1, 4, 2, 7, 3, 6,
            });
            Player p_1 = server.alive[0];
            Board board = new Board(6);
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile3 };
            Assert.IsTrue(server.LegalPlay(p_1, board, testTile3));
        }
        [TestMethod]
        public void TestLegalPlayTrue1_f()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();

            server.AddPlayer(p1, 12, "blue");
            Tile testTile1 = new Tile(1, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });

            Player p_1 = server.alive[0];
            Board board = new Board(6);
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile1 };
            Assert.IsTrue(server.LegalPlay(p_1, board, testTile1));
        }
        [TestMethod]
        public void TestLegalPlayLastResort()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            server.AddPlayer(p1, 12, "blue");
            Tile testTile1 = new Tile(1, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile2 = new Tile(2, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile3 = new Tile(3, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });

            Player p_1 = server.alive[0];
            Board board = new Board(6);
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            Assert.IsTrue(server.LegalPlay(p_1, board, testTile1));
        }

        [TestMethod]
        public void TestPlayTurn()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();
            server.AddPlayer(p1, 12, "blue");
            server.AddPlayer(p2, 10, "green");

            Tile playTile = new Tile(1, new List<int>{0, 7, 1, 2, 3, 4, 5, 6});
            server.alive[0].Hand.Remove(playTile);

            server.alive[0].InitPlayerPosition(new List<int>{0, -1, 5});
             
            (List<Tile>, List<Player>, List<Player>, Board, Boolean) playResult = server.PlayATurn(server.deck, 
                                                                                                   server.alive, 
                                                                                                   server.dead, 
                                                                                                   server.board, 
                                                                                                   playTile);
            Assert.AreEqual(1, server.alive.Count);
            Assert.AreEqual(1, server.dead.Count);
        }

        [TestMethod]
        public void TestPlayTurn2TilePath()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();
            server.AddPlayer(p1, 12, "blue");
            server.AddPlayer(p2, 10, "green");

            Tile playTile = new Tile(1, new List<int> { 0, 4, 1, 2, 3, 5, 6, 7 });
            server.alive[0].Hand.Remove(playTile);

            Tile secondTile = new Tile(2, new List<int> { 0, 4, 2, 5, 1, 3, 6, 7 });
            server.board.PlaceTile(secondTile, 1, 0);

            (List<Tile>, List<Player>, List<Player>, Board, Boolean) playResult = server.PlayATurn(server.deck,
                                                                                                   server.alive,
                                                                                                   server.dead,
                                                                                                   server.board,
                                                                                                   playTile);
            Assert.AreEqual(2, server.alive.Count);
            Assert.AreEqual(0, server.dead.Count);
            // 0th player should move to the end
            CollectionAssert.AreEqual(new List<int>() { 1, 0, 5 }, server.alive[1].position);
        }

        [TestMethod]
        public void TestPlayTurnOneWin()
        {
        }
    }
}
