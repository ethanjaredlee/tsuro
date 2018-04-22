using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TsuroTheSecond;

namespace TsuroTheSecondTests
{
    [TestClass]
    public class TileTest
    {
        [TestMethod]
        public void CheckRotate()
        {
            List<List<int>> paths = new List<List<int>>(4) {
                new List<int>(2){0, 1},
                new List<int>(2){2, 3},
                new List<int>(2){4, 5},
                new List<int>(2){6, 7},
            };
            Tile testTile1 = new Tile(1, paths);
            testTile1.Rotate();
            List<List<int>> changed_paths = new List<List<int>>(4) {
                new List<int>(2){2, 3},
                new List<int>(2){4, 5},
                new List<int>(2){6, 7},
                new List<int>(2){0, 1},
            };
            Tile testTile2 = new Tile(2, paths);
            Assert.AreNotEqual(paths, changed_paths);
            testTile1.PrintMe();
            testTile2.PrintMe();
            CollectionAssert.AreEqual(testTile1.paths, testTile2.paths);

        }
    }
}
