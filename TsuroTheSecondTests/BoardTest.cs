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
            Assert.IsTrue(board.FreeTileSpace(1, 1));
            Tile tile = new Tile(1, new List<List<int>>{
                new List<int>{0, 1},
                new List<int>{2, 3},
                new List<int>{4, 5},
                new List<int>{6, 7}
            });
            board.PlaceTile(tile, 1, 1);
            Assert.IsFalse(board.FreeTileSpace(1, 1));
            Debug.Print(string.Join("\n", string.Join(", ", board.tiles)));
        }
    }
}
