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
        Tile testTile1;
        Tile testTile2;
        string checkTile1;
        string checkTile2;

        [TestInitialize]
        public void Initialize() {
            converter = new ToXml();

            name = "team23";

            testTile1 = new Tile(1, new List<int> { 0, 5, 1, 3, 2, 6, 4, 7 });
            testTile2 = new Tile(1, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 });

            checkTile1 = "<tile><connect><n>0</n><n>5</n></connect><connect><n>1</n><n>3</n></connect><connect><n>2</n><n>6</n></connect><connect><n>4</n><n>7</n></connect></tile>";
            checkTile2 = "<tile><connect><n>0</n><n>1</n></connect><connect><n>2</n><n>3</n></connect><connect><n>4</n><n>5</n></connect><connect><n>6</n><n>7</n></connect></tile>";
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
            XElement tile = converter.TiletoXml(testTile1);
            string tileString = converter.FormatXml(tile);
            Assert.AreEqual(checkTile1, tileString);
        }

        [TestMethod]
        public void TestSetofTiletoXml()
        {
            XElement setTile = converter.SetofTiletoXml(new List<Tile>{testTile1, testTile2});
            string tileString = converter.FormatXml(setTile);
            string check = "<set>" + checkTile1 + checkTile2 + "</set>";
            Assert.AreEqual(check, tileString);
        }


        [TestMethod]
        public void TestXYtoXml()
        {
            XElement xy = converter.XYtoXml((1, 2));
            string xyString = converter.FormatXml(xy);
            string check = "<xy><x>1</x><y>2</y></xy>";
            Assert.AreEqual(check, xyString);
        }

        [TestMethod]
        public void TestMultiTilestoXml() {
            XElement multi = converter.MultiTilesToXml(new List<(Tile, (int, int))>{
                (testTile1, (0, 0)),
                (testTile2, (0, 1))
            });
            string multiString = converter.FormatXml(multi);
            string check = "" +
                "<map>" +
                "<ent>" +
                "<xy><x>0</x><y>0</y></xy>" +
                checkTile1 + 
                "</ent>" +
                "<ent>" +
                "<xy><x>0</x><y>1</y></xy>" +
                checkTile2 + 
                "</ent>" +
                "</map>";
            Assert.AreEqual(check, multiString);
        }

        [TestMethod]
        public void TestMultiTilestoXmlNoTiles()
        {
            XElement multi = converter.MultiTilesToXml(new List<(Tile, (int, int))>());
            string multiString = converter.FormatXml(multi);
            string check = "" +
                "<map>" +
                "<ent>" +
                "</ent>" +
                "</map>";
            Assert.AreEqual(check, multiString);
        }

        [TestMethod]
        public void TestPositionXml()
        {
            Position p1 = new Position(0, 0, 3, true);
            XElement pos1 = converter.PawnLoctoXml(p1);
            string posString1 = converter.FormatXml(pos1);

            Position p2 = new Position(1, 0, 6, true);
            XElement pos2 = converter.PawnLoctoXml(p2);
            string posString2 = converter.FormatXml(pos2);

            string check = "<pawn-loc><v></v><n>1</n><n>1</n></pawn-loc>";
            Assert.AreEqual(check, posString1);
            Assert.AreEqual(check, posString2);
        }

        [TestMethod]
        public void TestBoardToXml()
        {
            Board board = new Board(6);
            Tile tile = new Tile(1, new List<int> { 0, 1, 2, 4, 3, 6, 5, 7 });
            board.PlaceTile(tile, 0, 0);
            board.tokenPositions["red"] = new Position(0, 0, 3, true);

            XElement boardXml = converter.BoardtoXml(board);
            string bString = converter.FormatXml(boardXml);
            // board given in the assignment
            string check = "<board><map><ent><xy><x>0</x><y>0</y></xy><tile><connect><n>0</n><n>1</n></connect><connect><n>2</n><n>4</n></connect><connect><n>3</n><n>6</n></connect><connect><n>5</n><n>7</n></connect></tile></ent></map><map><ent><color>red</color><pawn-loc><v></v><n>1</n><n>1</n></pawn-loc></ent></map></board>";
            Assert.AreEqual(check, bString);
        }

        [TestMethod]
        public void TestEmptyBoard() {
            Board board = new Board(6);
            XElement boardXml = converter.BoardtoXml(board);
            string bString = converter.FormatXml(boardXml);
            // board given in the assignment
            string check = "<board><map><ent></ent></map><map><ent></ent></map></board>";
            Assert.AreEqual(check, bString);
        }

        [TestMethod]
        public void TestColorListToXml()
        {
            XElement colorList = converter.ColorListtoXml(new List<string> { "blue", "red" });
            string colors = converter.FormatXml(colorList);

            string check = "<list><color>blue</color><color>red</color></list>";
            Assert.AreEqual(check, colors);
        }

        [TestMethod]
        public void TestInitializetoXml()
        {
            XElement init = converter.InitializetoXml("blue", new List<string> { "blue", "red" });
            string initString = converter.FormatXml(init);

            string check = "<initialize><color>blue</color><list><color>blue</color><color>red</color></list></initialize>";
            Assert.AreEqual(check, initString);
        }

        [TestMethod]
        public void PlayTurntoXmlTest() {
            Board board = new Board(6);
            Tile tile = new Tile(1, new List<int> { 0, 1, 2, 4, 3, 6, 5, 7 });
            board.PlaceTile(tile, 0, 0);
            board.tokenPositions["red"] = new Position(0, 0, 3, true);

            List<Tile> hand = new List<Tile> { testTile1, testTile2 };
            int unused = 30;

            XElement play = converter.PlayTurntoXml(board, hand, unused);
            string playTurn = converter.FormatXml(play);

            string boardCheck = "<board><map><ent><xy><x>0</x><y>0</y></xy><tile><connect><n>0</n><n>1</n></connect><connect><n>2</n><n>4</n></connect><connect><n>3</n><n>6</n></connect><connect><n>5</n><n>7</n></connect></tile></ent></map><map><ent><color>red</color><pawn-loc><v></v><n>1</n><n>1</n></pawn-loc></ent></map></board>";
            string handCheck = "<set>" + checkTile1 + checkTile2 + "</set>";
            string check = "<play-turn>" + boardCheck + handCheck + "<n>30</n></play-turn>";

            Assert.AreEqual(check, playTurn);
        }

        [TestMethod]
        public void EndGametoXmlTest() {
            Board board = new Board(6);
            Tile tile = new Tile(1, new List<int> { 0, 1, 2, 4, 3, 6, 5, 7 });
            board.PlaceTile(tile, 0, 0);
            board.tokenPositions["red"] = new Position(0, 0, 3, true);

            List<string> winners = new List<string> { "red" };

            XElement end = converter.EndGametoXml(board, winners);
            string endString = converter.FormatXml(end);

            string boardCheck = "<board><map><ent><xy><x>0</x><y>0</y></xy><tile><connect><n>0</n><n>1</n></connect><connect><n>2</n><n>4</n></connect><connect><n>3</n><n>6</n></connect><connect><n>5</n><n>7</n></connect></tile></ent></map><map><ent><color>red</color><pawn-loc><v></v><n>1</n><n>1</n></pawn-loc></ent></map></board>";
            string colorsCheck = "<set><color>red</color></set>";
            string check = "<end-game>" + boardCheck + colorsCheck + "</end-game>";

            Assert.AreEqual(check, endString);
        }

        [TestMethod]
        public void SetofColortoXmlTest()
        {
            List<string> colors = new List<string> { "blue", "green", "red" };
            XElement element = converter.SetofColortoXml(colors);
            string colorString = converter.FormatXml(element);
            string check = "<set><color>blue</color><color>green</color><color>red</color></set>";
            Assert.AreEqual(check, colorString);
        }

        [TestMethod]
        public void DragonTileHolderPlayertoXmlTest() {
            Player player = new Player(new RandomPlayer("john"), "blue");
            player.Hand = new List<Tile>{
                testTile1,
                testTile2
            };

            XElement playerXml = converter.PlayertoXml(player, true);
            string playerString = converter.FormatXml(playerXml);
            string check = "<splayer-dragon><color>blue</color><set>" + checkTile1 + checkTile2 + "</set></splayer-dragon>";
            Assert.AreEqual(check, playerString);
        }

        //[TestMethod]
        //public void ListofPlayersToXmlTest() {
        //    Player player1 = new Player(new RandomPlayer("john"), "blue");
        //    player1.Hand = new List<Tile>{
        //        testTile1,
        //        testTile2
        //    };

        //    Player player2 = new Player(new RandomPlayer("john"), "blue");
        //    player2.Hand = new List<Tile>{
        //        testTile1,
        //        testTile2
        //    };

        //    List<Player> players = new List<Player> { player1, player2 };
        //    XElement listPlayersXml = converter.ListofPlayertoXml(players, -1);
        //}
    }
}
