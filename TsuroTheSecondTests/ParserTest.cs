using System;
using System.Xml;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TsuroTheSecond;

namespace TsuroTheSecondTests
{
    [TestClass]
    public class ParserTest
    {
        Parser parser;
        XmlDocument document;

        // example XML
        String tileXML;

        [TestInitialize]
        public void Initialize() {
            parser = new Parser();
            document = new XmlDocument();

            tileXML = "<tile><connect><n>0</n><n>5</n></connect><connect><n>1</n><n>3</n></connect><connect><n>2</n><n>6</n></connect><connect><n>4</n><n>7</n></connect></tile>";
        }

        [TestMethod]
        public void TestTileParse() {
            Tile tile = parser.TileParse(tileXML);
            Tile testTile = new Tile(1, new List<int>{0, 5, 1, 3, 2, 6, 4, 7});
            Assert.AreEqual(testTile, tile);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This is not a <tile> tag")]
        public void TestParseTileNotATileException()
        {
            Tile tile = parser.TileParse("<BadTag>asdf</BadTag>");
        }

        [TestMethod]
        public void TestNParse()
        {
            int n = parser.NParse("<n>5</n>");
            Assert.AreEqual(5, n);
        }

        [TestMethod]
        public void TestNParseWithWhitespace()
        {
            int n = parser.NParse("<n>  5   </n>");
            Assert.AreEqual(5, n);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This is not a <n> tag")]
        public void TestNParseWrongInput()
        {
            int n = parser.NParse("<BadTag>asdf</BadTag>");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestNParseBadN()
        {
            int n = parser.NParse("<n>asdf</n>");
        }

        [TestMethod]
        public void TestConnectParse()
        {
            (int, int) n = parser.ConnectParse("<connect><n>5</n><n>2</n></connect>");
            Assert.AreEqual(5, n.Item1);
            Assert.AreEqual(2, n.Item2);
        }

        [TestMethod]
        public void TestConnectParseWithWhiteSpace()
        {
            (int, int) n = parser.ConnectParse("<connect><n>5</n><n>2</n>   </connect>");
            Assert.AreEqual(5, n.Item1);
            Assert.AreEqual(2, n.Item2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Connect should only have 2 <n> tags")]
        public void TestConnectParse3nTags()
        {
            (int, int) n = parser.ConnectParse("<connect><n>5</n><n>2</n><n>4</n></connect>");
        }

        [TestMethod]
        public void TestPlayerNameParse()
        {
            string name = parser.PlayerNameParse("<player-name>ethan</player-name>");
            Assert.AreEqual("ethan", name);
        }

        [TestMethod]
        public void TestPlayerNameParseWithWhitespace()
        {
            string name = parser.PlayerNameParse("<player-name>  ethan</player-name>");
            Assert.AreEqual("ethan", name);
        }
    }
}
