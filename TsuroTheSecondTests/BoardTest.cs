using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TsuroTheSecond;

namespace TsuroTheSecondTests
{
    [TestClass]
    public class BoardTest
    {
        Server server;

        [TestInitialize]
        public void Initialize()
        {
            server = new Server();
        }
        void AddTwoPlayers()
        {
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();

            server.AddPlayer(p1, "blue");
            server.AddPlayer(p2, "green");
        }

        void AddFourPlayers()
        {
            MPlayer p1 = new MPlayer();
            MPlayer p2 = new MPlayer();
            MPlayer p3 = new MPlayer();
            MPlayer p4 = new MPlayer();

            server.AddPlayer(p1, "blue");
            server.AddPlayer(p2, "green");
            server.AddPlayer(p3, "red");
            server.AddPlayer(p4, "hotpink");
        }

        [TestMethod]
        public void TestConstructor() {
            Board board = new Board(6);
            List<List<Tile>> initBoard = new List<List<Tile>> {
                new List<Tile>{null, null, null, null, null, null},
                new List<Tile>{null, null, null, null, null, null},
                new List<Tile>{null, null, null, null, null, null},
                new List<Tile>{null, null, null, null, null, null},
                new List<Tile>{null, null, null, null, null, null},
                new List<Tile>{null, null, null, null, null, null},
            };
            CollectionAssert.Equals(initBoard, board);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Board size must be > 0")]
        public void TestConstructorNegativeboard()
        {
            Board board = new Board(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Tile placement is out of board range")]
        public void TestPlaceTileOutOfRange1() {
            Board board = new Board(6);
            Tile tile = new Tile(1, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 }); 
            board.PlaceTile(tile, 1, 9);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Tile placement is out of board range")]
        public void TestPlaceTileOutOfRange2()
        {
            Board board = new Board(6);
            Tile tile = new Tile(1, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 });
            board.PlaceTile(tile, -1, 1);
        }

        [TestMethod]
        public void TestPlaceAndFreeTileSpace()
        {
            Board board = new Board(6);
            Tile tile = new Tile(1, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 });
            Assert.IsNull(board.tiles[1][1]);
            board.PlaceTile(tile, 1, 1);
            Assert.IsNotNull(board.tiles[1][1]);
        }

        [TestMethod]
        public void TestAddPlayerToken() {
            Board board = new Board(6);
            board.AddPlayerToken("blue", new Position(0, -1, 5));
            board.AddPlayerToken("green", new Position(0, -1, 4));

            Assert.AreEqual(0, board.tokenPositions["blue"].x);
            Assert.AreEqual(-1, board.tokenPositions["blue"].y);
            Assert.AreEqual(5, board.tokenPositions["blue"].port);

            Assert.AreEqual(0, board.tokenPositions["green"].x);
            Assert.AreEqual(-1, board.tokenPositions["green"].y);
            Assert.AreEqual(4, board.tokenPositions["green"].port);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddDuplicateColorPlayerToken()
        {
            Board board = new Board(6);
            board.AddPlayerToken("blue", new Position(0, -1, 5));
            board.AddPlayerToken("blue", new Position(0, -1, 4));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddDuplicatePositionPlayerToken()
        {
            Board board = new Board(6);
            board.AddPlayerToken("blue", new Position(0, -1, 5));
            board.AddPlayerToken("green", new Position(0, -1, 5));
        }

        //[TestMethod]
        //public void TestMovePlayer()
        //{
        //    // start at 5, 6 and at port 0
        //    Board board = new Board(6);
        //    //MPlayer p_1 = new MPlayer();
        //    //Player p1 = new Player(p_1, 4);
        //    board.AddPlayerToken("blue", new Position(5, 6, 0));
        //    Tile testTile_1 = new Tile(1, new List<int>(8) {
        //        0, 4, 1, 5, 2, 6, 3, 7
        //    });
        //    // places a tile that gives direct path up 
        //    board.PlaceTile(testTile_1, 5, 5);
        //    // move and update position of the player
        //    board.MovePlayer("blue");
        //    // check position
        //    Position p = board.tokenPositions["blue"];

        //    Assert.AreEqual(p.x, 5);
        //    Assert.AreEqual(p.y, 5);
        //    Assert.AreEqual(p.port, 1);
        //}

        [TestMethod]
        public void TestValidTilePlacement()
        {
            AddFourPlayers();

            server.board.AddPlayerToken(server.alive[0].Color, new Position(4, 6, 0));
            Tile testTile = new Tile(1, new List<int> { 1, 2, 3, 4, 5, 6, 7, 0 });
            Assert.IsTrue(server.board.ValidTilePlacement(server.alive[0], testTile));
        }

        [TestMethod]
        public void TestValidTilePlacementFalse()
        {
            AddFourPlayers();

            server.board.AddPlayerToken(server.alive[0].Color, new Position(4, 6, 0));
            Tile testTile = new Tile(1, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 });
            Assert.IsFalse(server.board.ValidTilePlacement(server.alive[0], testTile));
        }

        [TestMethod]
        public void TestAllPossibleTilesFullHand()
        {
            AddTwoPlayers();
            server.board.AddPlayerToken("blue", new Position(4, 6, 0));
            server.board.AddPlayerToken("green", new Position(4, 6, 1));


            Tile testTile1 = new Tile(1, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile2 = new Tile(2, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Tile testTile3 = new Tile(3, new List<int>(8) {
                0, 5, 1, 4, 2, 7, 3, 6,
            });
            Tile testTile4 = new Tile(4, new List<int>(8)
            {
                0, 4, 1, 5, 2, 6, 3, 7
            });

            server.alive[0].Hand = new List<Tile> { testTile1, testTile2, testTile3 };

            List<Tile> every_combi_blue = server.board.AllPossibleTiles(server.alive[0]);
            // only testTile3 is valid.
            Assert.AreEqual(4, every_combi_blue.Count);
            server.alive[1].Hand = new List<Tile> { testTile1, testTile3, testTile4 };
            List<Tile> every_combi_green = server.board.AllPossibleTiles(server.alive[1]);

            Assert.AreEqual(8, every_combi_green.Count);

        }
    }
}
