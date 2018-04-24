using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
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
            for (int i = 0; i < 4; i++) {
                int tile1_ent_1 = testTile1.paths[i][0];
                Assert.AreEqual(testTile1.FindEndofPath(tile1_ent_1), testTile2.FindEndofPath(tile1_ent_1));
                int tile1_ent_2 = testTile1.paths[i][1];
                Assert.AreEqual(testTile1.FindEndofPath(tile1_ent_2), testTile2.FindEndofPath(tile1_ent_2));
            }
        }

    }
}
