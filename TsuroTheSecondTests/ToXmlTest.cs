using System;
using System.Xml;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TsuroTheSecond;

namespace TsuroTheSecondTests
{
    [TestClass]
    public class ToXmlTest
    {
        ToXml converter;
        string name;
        Tile testTile;

        [TestInitialize]
        public void Initialize() {
            converter = new ToXml();

            name = "team23";

            testTile = new Tile(1, new List<int> { 0, 5, 1, 3, 2, 6, 4, 7 });

        }

        //[TestMethod]
        //public void TestNametoXml() {
        //    string xml = converter.NametoXml(name);
        //    string check = "player-name>team23</player-name>";
        //    Assert.AreEqual(xml, check);  
        //}

        //[TestMethod]
        //public void TestTiletoXml()
        //{
        //    string tilexml = converter.TiletoXml(testTile);
        //    string check = "<tile><connect><n>0</n><n>5</n></connect><connect><n>1</n><n>3</n></connect><connect><n>2</n><n>6</n></connect><connect><n>4</n><n>7</n></connect></tile>";
        //    Assert.AreEqual(tilexml, check);
        //}


        //[TestMethod]
        //public void TestLocationtoXml()
        //{
        //    int x = 0;
        //    int y = 1;
        //    int p = 2;
        //    string locxml = converter.LocationtoXml(x, y, p);
        //    string check = "<pawn-loc><v></v><n>1</n><n>2</n></pawn-loc>";

        //}


    }
}
