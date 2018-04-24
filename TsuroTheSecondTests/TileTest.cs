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
            Tile testTile1 = new Tile(1, new List<int>{0, 1, 2, 3, 4, 5, 6, 7});
            testTile1.Rotate();
            Tile testTile2 = new Tile(2, new List<int>{2, 3, 4, 5, 6, 7, 0, 1});
            CollectionAssert.AreEqual(testTile1.paths, testTile2.paths);

        }
    }
}
