using System;
using System.Xml;
using System.Xml.Linq;
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

        [TestMethod]
        public void TestNametoXml() {
            XElement xml = converter.NametoXml(name);
            string nameString = converter.FormatXml(xml);
            string check = "<player-name>team23</player-name>";
            Assert.AreEqual(check, nameString);  
        }

        [TestMethod]
        public void TestNXml()
        {
            XElement nElement = converter.NtoXml(2);
            string nString = converter.FormatXml(nElement);
            string check = "<n>2</n>";
            Assert.AreEqual(nString, check);
        }

        [TestMethod]
        public void TestTiletoXml()
        {
            XElement tile = converter.TiletoXml(testTile);
            string tileString = converter.FormatXml(tile);
            string check = "<tile><connect><n>0</n><n>5</n></connect><connect><n>1</n><n>3</n></connect><connect><n>2</n><n>6</n></connect><connect><n>4</n><n>7</n></connect></tile>";
            Assert.AreEqual(check, tileString);
        }

        [TestMethod]
        public void TestXYtoXml()
        {
            XElement xy = converter.XYtoXml((1, 2));
            string xyString = converter.FormatXml(xy);
            string check = "<xy><x><n>1</n></x><y><n>2</n></y></xy>";
            Assert.AreEqual(check, xyString);
        }

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
