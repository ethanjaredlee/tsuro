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
            board.AddPlayerToken("blue", new List<int> { 0, -1, 5 });
            board.AddPlayerToken("green", new List<int> { 0, -1, 4 });
            CollectionAssert.Equals(new List<int> { 0, -1, 5 }, board.tokenPositions["blue"]);
            CollectionAssert.Equals(new List<int> { 0, -1, 4 }, board.tokenPositions["green"]);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddDuplicateColorPlayerToken()
        {
            Board board = new Board(6);
            board.AddPlayerToken("blue", new List<int> { 0, -1, 5 });
            board.AddPlayerToken("blue", new List<int> { 0, -1, 4 });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddDuplicatePositionPlayerToken()
        {
            Board board = new Board(6);
            board.AddPlayerToken("blue", new List<int> { 0, -1, 5 });
            board.AddPlayerToken("green", new List<int> { 0, -1, 5 });
        }
    }
}
