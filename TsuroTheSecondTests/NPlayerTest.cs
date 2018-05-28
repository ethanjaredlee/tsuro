using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TsuroTheSecond;

namespace TsuroTheSecondTests
{
    [TestClass]
    public class NPlayerTest
    {
        NPlayer player;

        [TestInitialize]
        public void Initialize() {
            player = new NPlayer();
        }

        [TestMethod]
        public void GetNameTest() {
            // not really sure how name works
            string name = player.GetName();
        }

        [TestMethod]
        public void InitializeTest()
        {
        }
    }
}
