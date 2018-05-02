﻿using System;
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

            server.AddPlayer(p1, 12);
            server.AddPlayer(p2, 10);
        }

        void AddFourPlayers(){
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();
            MPlayer p3 = new MPlayer();
            MPlayer p4 = new MPlayer();

            server.AddPlayer(p1, 12);
            server.AddPlayer(p2, 10);
            server.AddPlayer(p3, 20);
            server.AddPlayer(p4, 30);
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
        public void TestSortedByAge()
        {
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();
            MPlayer p3 = new MPlayer();

            server.AddPlayer(p1, 9);
            server.AddPlayer(p2, 10);
            server.AddPlayer(p3, 2);

            Console.WriteLine(server.alive[0].age);
            Console.WriteLine(server.alive[1].age);
            Console.WriteLine(server.alive[2].age);

            Assert.IsTrue(server.alive[0].age >= server.alive[1].age);
            Assert.IsTrue(server.alive[1].age >= server.alive[2].age);
            Assert.AreEqual(10, server.alive[0].age);
            Assert.AreEqual(9, server.alive[1].age);
            Assert.AreEqual(2, server.alive[2].age);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Only 8 players allowed in a game")]
        public void Test9PlayerGame()
        {
            for (int i = 0; i < 8; i++) {
                server.AddPlayer(new MPlayer(), 10);
            }

            // case that breaks
            server.AddPlayer(new MPlayer(), 10);
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
        public void TestValidTilePlacement() {
            AddFourPlayers();

            server.alive[0].InitPlayerPosition(new List<int> { 4, 6, 0 });
            Tile testTile = new Tile(1, new List<int> { 1, 2, 3, 4, 5, 6, 7, 0 });
            Assert.IsTrue(server.ValidTilePlacement(server.board, server.alive[0], testTile));
        }

        [TestMethod]
        public void TestValidTilePlacementFalse()
        {
            AddFourPlayers();

            server.alive[0].InitPlayerPosition(new List<int> { 4, 6, 0 });
            Tile testTile = new Tile(1, new List<int> {0, 1, 2, 3, 4, 5, 6, 7});
            Assert.IsFalse(server.ValidTilePlacement(server.board, server.alive[0], testTile));
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
            server.AddPlayer(p1, 12);

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
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            Boolean legalPlay = server.LegalPlay(p_1, server.board, testTile1);
            Tile checkTile1 = new Tile(1, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });

            for (int i = 0; i < 4; i++){
                CollectionAssert.AreEqual(testTile1.paths[i], checkTile1.paths[i]);
            }
        }

        [TestMethod]
        public void TestLegalPlayBoardUndo()
        {
            Server server = new Server();
            MPlayer p1 = new MPlayer();
            server.AddPlayer(p1, 12);

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
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            Boolean legalPlay = server.LegalPlay(p_1, server.board, testTile1);

            Assert.IsNull(server.board.tiles[4][5]);
        }

        [TestMethod]
        public void TestLegalPlayFalse3()
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
            Board board = new Board(6);
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            Assert.IsFalse(server.LegalPlay(p_1, board, testTile1));
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
            Board board = new Board(6);
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile1, testTile3 };
            Assert.IsFalse(server.LegalPlay(p_1, board, testTile1));
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
            Board board = new Board(6);
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            Assert.IsTrue(server.LegalPlay(p_1, board, testTile3));
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
            Board board = new Board(6);
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile1, testTile3 };
            Assert.IsTrue(server.LegalPlay(p_1, board, testTile3));
        }
        [TestMethod]
        public void TestLegalPlayTrue1_t()
        {
            AddTwoPlayers();

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
            AddTwoPlayers();

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
            Board board = new Board(6);
            p_1.InitPlayerPosition(new List<int> { 4, 6, 0 });
            p_1.Hand = new List<Tile> { testTile1, testTile2, testTile3 };
            Assert.IsTrue(server.LegalPlay(p_1, board, testTile1));
        }

        [TestMethod]
        public void TestPlayTurn()
        {
            AddTwoPlayers();

            Tile playTile = new Tile(1, new List<int>{0, 7, 1, 2, 3, 4, 5, 6});
            server.alive[0].Hand.Remove(playTile);

            server.alive[0].InitPlayerPosition(new List<int>{0, -1, 5});
            server.alive[1].InitPlayerPosition(new List<int>{ 0, -1, 4 });
             
            (List<Tile>, List<Player>, List<Player>, Board, Boolean) playResult = server.PlayATurn(server.deck, 
                                                                                                   server.alive, 
                                                                                                   server.dead, 
                                                                                                   server.board, 
                                                                                                   playTile);
            Assert.AreEqual(1, server.alive.Count);
            Assert.AreEqual(1, server.dead.Count);
            CollectionAssert.AreEqual(new List<int> { 0, 0, 2 }, server.alive[0].position);
        }

        [TestMethod]
        public void TestPlayTurn2TilePath()
        {
            AddTwoPlayers();

            server.alive[0].InitPlayerPosition(new List<int> { 0, -1, 5 });
            server.alive[1].InitPlayerPosition(new List<int> { 0, -1, 4 });

            Tile playTile = new Tile(1, new List<int> { 0, 4, 1, 2, 3, 5, 6, 7 });
            server.alive[0].Hand.Remove(playTile);

            Tile secondTile = new Tile(2, new List<int> { 0, 7, 2, 6, 1, 3, 5, 4 });
            server.board.PlaceTile(secondTile, 0, 1);

            (List<Tile>, List<Player>, List<Player>, Board, Boolean) playResult = server.PlayATurn(server.deck,
                                                                                                   server.alive,
                                                                                                   server.dead,
                                                                                                   server.board,
                                                                                                   playTile);
            Assert.AreEqual(2, server.alive.Count);
            // 0th player should move to the end
            CollectionAssert.AreEqual(new List<int>{ 0, 0, 2 }, server.alive[1].position);
        }

        [TestMethod]
        public void TestPlayTurnOneWin()
        {
        }
    }
}
