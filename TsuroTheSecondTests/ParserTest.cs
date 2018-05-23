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
        String tile1XML;
        String tile2XML;
        String tile3XML;

        [TestInitialize]
        public void Initialize() {
            parser = new Parser();
            document = new XmlDocument();

            tile1XML = "<tile><connect><n>0</n><n>5</n></connect><connect><n>1</n><n>3</n></connect><connect><n>2</n><n>6</n></connect><connect><n>4</n><n>7</n></connect></tile>";
            tile2XML = "<tile><connect><n>0</n><n>1</n></connect><connect><n>2</n><n>3</n></connect><connect><n>4</n><n>5</n></connect><connect><n>6</n><n>7</n></connect></tile>";
            tile3XML = "<tile><connect><n>1</n><n>2</n></connect><connect><n>3</n><n>4</n></connect><connect><n>5</n><n>6</n></connect><connect><n>7</n><n>0</n></connect></tile>";
        }

        [TestMethod]
        public void TestTileParse() {
            Tile tile = parser.TileParse(tile1XML);
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

        [TestMethod]
        public void TestColor()
        {
            string color = parser.ColorParse("<color>blue</color>");
            Assert.AreEqual("blue", color);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid color")]
        public void TestBadColor()
        {
            string color = parser.ColorParse("<color>brown</color>");
        }

        [TestMethod]
        public void TestListOfTileParse()
        {
            string listTileString = "<list-of-tile>" + tile1XML + tile2XML + tile3XML + "</list-of-tile>";
            List<Tile> tileList = parser.ListOfTileParse(listTileString);
            Assert.AreEqual(parser.TileParse(tile1XML), tileList[0]);
            Assert.AreEqual(parser.TileParse(tile2XML), tileList[1]);
            Assert.AreEqual(parser.TileParse(tile3XML), tileList[2]);
        }

        [TestMethod]
        public void TestSetOfTileParse()
        {
            string listTileString = "<list-of-tile>" + tile1XML + tile2XML + tile3XML + "</list-of-tile>";
            List<Tile> tileList = parser.SetOfTileParse(listTileString);
            Assert.AreEqual(parser.TileParse(tile1XML), tileList[0]);
            Assert.AreEqual(parser.TileParse(tile2XML), tileList[1]);
            Assert.AreEqual(parser.TileParse(tile3XML), tileList[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Tiles are not unique")]
        public void TestSetOfTileParseDuplicates()
        {
            string listTileString = "<list-of-tile>" + tile1XML + tile1XML + tile3XML + "</list-of-tile>";
            List<Tile> tileList = parser.SetOfTileParse(listTileString);
        }

        [TestMethod]
        public void TestListOfColorsParse()
        {
            string listColorString = "<list-of-color><color>blue</color><color>green</color></list-of-color>";
            List<string> colors = parser.ListOfColorParse(listColorString);
            Assert.AreEqual("blue", colors[0]);
            Assert.AreEqual("green", colors[1]);
        }

        [TestMethod]
        public void TestSplayerParse()
        {
            string splayerstring = "" +
                "<splayer-dragon>" +
                    "<color>blue</color>" +
                    "<list-of-tile>" + 
                        tile1XML + 
                        tile2XML + 
                        tile3XML + 
                    "</list-of-tile>" +
                "</splayer-dragon>";
            (string, List<Tile>) result = parser.SPlayerParse(splayerstring);
            Assert.AreEqual("blue", result.Item1);
            Assert.AreEqual(parser.TileParse(tile1XML), result.Item2[0]);
            Assert.AreEqual(parser.TileParse(tile2XML), result.Item2[1]);
            Assert.AreEqual(parser.TileParse(tile3XML), result.Item2[2]);
        }

        [TestMethod]
        public void TestlistofSplayerParse()
        {
            string splayerstring1 = "" +
                "<splayer-dragon>" +
                    "<color>blue</color>" +
                    "<list-of-tile>" +
                        tile1XML +
                        tile2XML +
                        tile3XML +
                    "</list-of-tile>" +
                "</splayer-dragon>";
            string splayerstring2 = "" +
                "<splayer-dragon>" +
                    "<color>green</color>" +
                    "<list-of-tile>" +
                        tile2XML +
                        tile3XML +
                        tile1XML +
                    "</list-of-tile>" +
                "</splayer-dragon>";
            string listofSplayers = "<list>" + splayerstring1 + splayerstring2 + "</list>";
            List<(string, List<Tile>)> result = parser.ListOfSplayerParse(listofSplayers);
            Assert.AreEqual("blue", result[0].Item1);
            Assert.AreEqual(parser.TileParse(tile1XML), result[0].Item2[0]);
            Assert.AreEqual(parser.TileParse(tile2XML), result[0].Item2[1]);
            Assert.AreEqual(parser.TileParse(tile3XML), result[0].Item2[2]);

            Assert.AreEqual("green", result[1].Item1);
            Assert.AreEqual(parser.TileParse(tile2XML), result[1].Item2[0]);
            Assert.AreEqual(parser.TileParse(tile3XML), result[1].Item2[1]);
            Assert.AreEqual(parser.TileParse(tile1XML), result[1].Item2[2]);
        }

        [TestMethod]
        public void TestXYParse() {
            string xystring = "<xy><x>3</x><y>2</y></xy>";
            (int, int) xy = parser.XYParse(xystring);
            Assert.AreEqual(3, xy.Item1);
            Assert.AreEqual(2, xy.Item2);
        }

        [TestMethod]
        public void TesthvParse()
        {
            string h = "<h></h>";
            bool horizontal = parser.HVIsHorizontalParse(h);
            Assert.IsTrue(horizontal);
        }

        [TestMethod]
        public void TesthvWithWhitespaceParse()
        {
            string h = "<h>  </h>";
            bool horizontal = parser.HVIsHorizontalParse(h);
            Assert.IsTrue(horizontal);
        }

        [TestMethod]
        public void TestvParse()
        {
            string v = "<v></v>";
            bool horizontal = parser.HVIsHorizontalParse(v);
            Assert.IsFalse(horizontal);
        }

        [TestMethod]
        public void TestPawnLocationParseHorizontal()
        {
            string location = "<pawn-loc><h></h><n>2</n><n>1</n></pawn-loc>";
            Position position = parser.PawnLocationParse(location);
            Assert.AreEqual(0, position.x);
            Assert.AreEqual(2, position.y);
            Assert.AreEqual(1, position.port);
        }

        [TestMethod]
        public void TestPawnLocationParseVertical()
        {
            string location = "<pawn-loc><v></v><n>2</n><n>1</n></pawn-loc>";
            Position position = parser.PawnLocationParse(location);
            Assert.AreEqual(2, position.x);
            Assert.AreEqual(0, position.y);
            Assert.AreEqual(6, position.port);
        }

        [TestMethod]
        public void TestPawnLocationParseVerticalBoundary()
        {
            string location = "<pawn-loc><h></h><n>6</n><n>1</n></pawn-loc>";
            Position position = parser.PawnLocationParse(location);
            Assert.AreEqual(0, position.x);
            Assert.AreEqual(6, position.y);
            Assert.AreEqual(1, position.port);
        }
    }
}
