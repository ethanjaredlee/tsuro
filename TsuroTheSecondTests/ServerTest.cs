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
        Server server;

        [TestInitialize]
        public void Initialize() {
            server = new Server();
        }

        void AddTwoPlayers()
        {
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();

            server.AddPlayer(p1, "blue");
            server.AddPlayer(p2, "green");
        }

        void AddFourPlayers(){
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();
            MPlayer p3 = new MPlayer();
            MPlayer p4 = new MPlayer();

            server.AddPlayer(p1, "blue");
            server.AddPlayer(p2, "green");
            server.AddPlayer(p3, "red");
            server.AddPlayer(p4, "hotpink");
        }

        /* TODO ******************************
         * Test Tiles are unique
         * Dragon tile more
         */

        [TestMethod]
        public void TestTilesUnique()
        {
            // test that no tile is a rotated version of any other tile
            // there are only 35 such tiles possible
            HashSet<string> tileCombinations = new HashSet<string>();
            foreach (Tile t in server.deck) {
                string tilePathMap = t.PathMap();
                tileCombinations.Add(tilePathMap);
            }

            Assert.AreEqual(35, tileCombinations.Count);
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
            AddTwoPlayers();

            Assert.AreEqual(2, server.alive.Count);
            Assert.AreEqual(0, server.dead.Count);
        }

        [TestMethod]
        public void TestDraw()
        {
            AddTwoPlayers();

            Assert.AreEqual(0, server.alive[0].Hand.Count);
            Assert.AreEqual(35, server.deck.Count);
            server.DrawTile(server.alive[0], server.deck);
            Assert.AreEqual(1, server.alive[0].Hand.Count);
            Assert.AreEqual(34, server.deck.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid color")]
        public void TestDuplicateColor()
        {
            AddTwoPlayers();

            MPlayer mPlayer = new MPlayer();
            server.AddPlayer(mPlayer, "blue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid color")]
        public void TestInvalidColor()
        {
            AddTwoPlayers();

            MPlayer mPlayer = new MPlayer();
            server.AddPlayer(mPlayer, "not blue");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Player can't have more than 3 cards in hand")]
        public void TestDrawTooManyTiles()
        {
            AddTwoPlayers();

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
            AddTwoPlayers();

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
            AddFourPlayers();

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
            AddFourPlayers();

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
            AddFourPlayers(); 

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
        public void TestLegalPlayCheckLastPossibleMoveNotUndone()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            server.AddPlayer(p1, "blue");

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
            server.board.AddPlayerToken("blue", new Position(4, 6, 0));
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            Boolean legalPlay = server.LegalPlay(p_1, server.board, testTile1);
            Assert.IsFalse(legalPlay);
            Assert.AreEqual(3, p_1.Hand.Count);
            for (int i = 1; i < 4; i++){
                Assert.AreEqual(i, p_1.Hand[i-1].id);
            }
        }

        [TestMethod]
        public void TestLegalPlayBoardUndo()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            server.AddPlayer(p1, "blue");

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
            server.board.AddPlayerToken("blue", new Position(4, 6, 0));
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            Boolean legalPlay = server.LegalPlay(p_1, server.board, testTile1);
            Assert.IsFalse(legalPlay);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++) 
                {
                    Assert.IsNull(server.board.tiles[i][j]);
                }
            }
        }

        [TestMethod]
        public void TestLegalPlayFalse2()
        {
            AddTwoPlayers();

            Tile testTile1 = new Tile(1, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile3 = new Tile(3, new List<int>(8) {
                0, 5, 1, 4, 2, 7, 3, 6,
            });

            Player p_1 = server.alive[0];
            server.board.AddPlayerToken("blue", new Position(4, 6, 0));
            p_1.Hand = new List<Tile> { testTile1, testTile3 };
            Assert.IsFalse(server.LegalPlay(p_1, server.board, testTile1));
        }

        [TestMethod]
        public void TestLegalPlayTrue3()
        {
            AddTwoPlayers();

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
            server.board.AddPlayerToken("blue", new Position(4, 6, 0));
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            Assert.IsTrue(server.LegalPlay(p_1, server.board, testTile3));
        }

        [TestMethod]
        public void TestLegalPlayTrue2()
        {
            AddTwoPlayers();

            Tile testTile1 = new Tile(1, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile3 = new Tile(3, new List<int>(8) {
                0, 5, 1, 4, 2, 7, 3, 6,
            });

            Player p_1 = server.alive[0];
            server.board.AddPlayerToken("blue", new Position(4, 6, 0));
            p_1.Hand = new List<Tile> { testTile1, testTile3 };
            Assert.IsTrue(server.LegalPlay(p_1, server.board, testTile3));
        }

        [TestMethod]
        public void TestLegalPlayTrue1_t()
        {
            AddTwoPlayers();

            Tile testTile3 = new Tile(3, new List<int>(8) {
                0, 5, 1, 4, 2, 7, 3, 6,
            });
            Player p_1 = server.alive[0];
            server.board.AddPlayerToken("blue", new Position(4, 6, 0));
            p_1.Hand = new List<Tile> { testTile3 };
            Assert.IsTrue(server.LegalPlay(p_1, server.board, testTile3));
        }
        [TestMethod]
        public void TestLegalPlayFalse1()
        {
            AddTwoPlayers();

            Tile testTile1 = new Tile(1, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });

            Player p_1 = server.alive[0];
            server.board.AddPlayerToken("blue", new Position(4, 6, 0));
            p_1.Hand = new List<Tile> { testTile1 };
            Assert.IsTrue(server.LegalPlay(p_1, server.board, testTile1));
        }
        [TestMethod]
        public void TestLegalPlayLastResort()
        {
            AddTwoPlayers();

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
            server.board.AddPlayerToken("blue", new Position(4, 6, 0));
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            Assert.IsTrue(server.LegalPlay(p_1, server.board, testTile1));
        }

        [TestMethod]
        public void TestLegalPlayFalseWithRotation()
        {
            AddTwoPlayers();

            Tile testTile1 = new Tile(1, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile2 = new Tile(2, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile3 = new Tile(3, new List<int>(8) {
                0, 1, 2, 7, 3, 6, 4, 5,
            });

            Player p_1 = server.alive[0];
            server.board.AddPlayerToken("blue", new Position(4, 6, 0));
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            // testTile3 will kill without rotation, but with rotation won't kill.
            // Thus, testTile3 is a valid option, making legalPlay of testTile1 false.
            Assert.IsFalse(server.LegalPlay(p_1, server.board, testTile1));
        }
        [TestMethod]
        public void TestLegalPlayTrueWithRotation()
        {
            AddTwoPlayers();

            Tile testTile1 = new Tile(1, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile2 = new Tile(2, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile3 = new Tile(3, new List<int>(8) {
                0, 1, 2, 7, 3, 6, 4, 5,
            });
            Player p_1 = server.alive[0];
            server.board.AddPlayerToken("blue", new Position(4, 6, 0));
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            // testTile3 will kill without rotation, but with rotation won't kill.
            Assert.IsTrue(server.LegalPlay(p_1, server.board, testTile3));
        }

        [TestMethod]
        public void TestPlayTurn()
        {
            // alive[0] is blue, alive[1] is green
            AddTwoPlayers();

            server.board.AddPlayerToken("blue", new Position(0, -1, 5));
            server.board.AddPlayerToken("green", new Position(0, -1, 4));

            Tile playTile = new Tile(1, new List<int>{0, 7, 1, 2, 3, 4, 5, 6});
            server.alive[0].Hand.Add(playTile);


            (List<Tile>, List<Player>, List<Player>, Board, Boolean) playResult = server.PlayATurn(server.deck, 
                                                                                                   server.alive, 
                                                                                                   server.dead, 
                                                                                                   server.board, 
                                                                                                   playTile);
            Assert.AreEqual(1, server.alive.Count);
            Assert.AreEqual(1, server.dead.Count);
            // dead
            Assert.AreEqual(-1, server.board.tokenPositions["blue"].x);
            Assert.AreEqual(0, server.board.tokenPositions["blue"].y);
            Assert.AreEqual(2, server.board.tokenPositions["blue"].port);
            Assert.AreEqual(-1, server.board.tokenPositions[server.dead[0].Color].x);
            Assert.AreEqual(0, server.board.tokenPositions[server.dead[0].Color].y);
            Assert.AreEqual(2, server.board.tokenPositions[server.dead[0].Color].port);
            // alive
            Assert.AreEqual(0, server.board.tokenPositions["green"].x);
            Assert.AreEqual(0, server.board.tokenPositions["green"].y);
            Assert.AreEqual(2, server.board.tokenPositions["green"].port);
            Assert.AreEqual(0, server.board.tokenPositions[server.alive[0].Color].x);
            Assert.AreEqual(0, server.board.tokenPositions[server.alive[0].Color].y);
            Assert.AreEqual(2, server.board.tokenPositions[server.alive[0].Color].port);



        }

        [TestMethod]
        public void TestPlayTurn2TilePath()
        {
            AddTwoPlayers();


            server.board.AddPlayerToken("blue", new Position(0, -1, 5));
            server.board.AddPlayerToken("green", new Position(0, -1, 4));


            Tile playTile = new Tile(1, new List<int> { 0, 4, 1, 2, 3, 5, 6, 7 });
            server.alive[0].Hand.Add(playTile);
            Assert.AreEqual("blue", server.alive[0].Color);

            Tile secondTile = new Tile(2, new List<int> { 0, 7, 2, 6, 1, 3, 5, 4 });
            server.board.PlaceTile(secondTile, 0, 1);

            (List<Tile>, List<Player>, List<Player>, Board, Boolean) playResult = server.PlayATurn(server.deck,
                                                                                                   server.alive,
                                                                                                   server.dead,
                                                                                                   server.board,
                                                                                                   playTile);
            Assert.AreEqual(2, server.alive.Count);
            Assert.AreEqual("green", server.alive[0].Color);
            Assert.AreEqual("blue", server.alive[1].Color);
            // 0th player should move to the end
            Assert.AreEqual(0, server.board.tokenPositions["green"].x);
            Assert.AreEqual(0, server.board.tokenPositions["green"].y);
            Assert.AreEqual(2, server.board.tokenPositions["green"].port);
            Assert.AreEqual(0, server.board.tokenPositions[server.alive[0].Color].x);
            Assert.AreEqual(0, server.board.tokenPositions[server.alive[0].Color].y);
            Assert.AreEqual(2, server.board.tokenPositions[server.alive[0].Color].port);

            Assert.AreEqual(0, server.board.tokenPositions["blue"].x);
            Assert.AreEqual(1, server.board.tokenPositions["blue"].y);
            Assert.AreEqual(3, server.board.tokenPositions["blue"].port);
            Assert.AreEqual(0, server.board.tokenPositions[server.alive[1].Color].x);
            Assert.AreEqual(1, server.board.tokenPositions[server.alive[1].Color].y);
            Assert.AreEqual(3, server.board.tokenPositions[server.alive[1].Color].port);

        }

        [TestMethod]
        public void TestPlayTurnOneWin()
        {
        }
    }
}
