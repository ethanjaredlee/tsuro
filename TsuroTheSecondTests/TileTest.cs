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
        [TestMethod]
        public void CheckJudgeSymmetric1()
        {
            Tile testTile1 = new Tile(1, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 });
            Tile testTile2 = new Tile(2, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 });

            testTile1.JudgeSymmetric();
            Assert.AreEqual(1, testTile1.symmetricity);
            Assert.AreEqual(testTile1.PathMap(), testTile2.PathMap());
        }
        [TestMethod]
        public void CheckJudgeSymmetric4()
        {
            Tile testTile1 = new Tile(1, new List<int> { 0, 5, 1, 3, 2, 6, 4, 7 });
            Tile testTile2 = new Tile(2, new List<int> { 0, 5, 1, 3, 2, 6, 4, 7 });

            testTile1.JudgeSymmetric();
            Assert.AreEqual(4, testTile1.symmetricity);
            Assert.AreEqual(testTile1.PathMap(), testTile2.PathMap());

        }

    }
}
