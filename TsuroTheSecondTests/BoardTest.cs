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
        public void TestPlaceAndFreeTileSpace() {
            Board board = new Board(6);
            Tile tile = new Tile(1, new List<int> { 1, 2, 3, 4, 5, 6, 7 }); 
            Assert.IsNull(board.tiles[1][1]);
            board.PlaceTile(tile, 1, 1);
            Assert.IsNotNull(board.tiles[1][1]);
        }
    }
}
