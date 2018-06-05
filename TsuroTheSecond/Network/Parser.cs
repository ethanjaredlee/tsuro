using System;
using System.Collections.Generic;
using System.Xml;

namespace TsuroTheSecond
{
    public class Parser
    {
        public Parser()
        {
        }

        public Tile TileParse(string tileXML) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(tileXML);

            if (document.DocumentElement.Name != "tile") {
                throw new ArgumentException("This is not a <tile> tag");
            }

            List<(int, int)> ports = new List<(int, int)>();
            foreach(XmlNode node in document.DocumentElement.ChildNodes) {
                ports.Add(ConnectParse(node.OuterXml));
            }

            List<int> portList = new List<int>();
            foreach((int, int) pair in ports) {
                portList.Add(pair.Item1);
                portList.Add(pair.Item2);
            }

            Tile tile = new Tile(1, portList);

            return tile;
        }

        public int NParse(string nXml) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(nXml);

            if (document.DocumentElement.Name != "n")
            {
                throw new ArgumentException("This is not a <n> tag");
            }

            int n = Int32.Parse(document.InnerText.Trim());

            return n;
        }

        public (int, int) ConnectParse(string connectXml) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(connectXml);

            if (document.DocumentElement.Name != "connect")
            {
                throw new ArgumentException("This is not a <connect> tag");
            }

            List<int> intermediate = new List<int>();
            foreach(XmlNode node in document.DocumentElement.ChildNodes) {
                intermediate.Add(NParse(node.OuterXml));
            }


            if (intermediate.Count > 2) {
                throw new ArgumentException("Connect should only have 2 <n> tags");
            }

            return (intermediate[0], intermediate[1]);
        }

        public string PlayerNameParse(string pNameXml) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(pNameXml);
            return document.InnerText.Trim();
        }

        public string ColorParse(string colorXml) {
            List<string> acceptedColors = new List<string> {
                "blue",
                "red",
                "green",
                "orange",
                "sienna",
                "hotpink",
                "darkgreen",
                "purple"
            };

            XmlDocument document = new XmlDocument();
            document.LoadXml(colorXml);

            string color = document.InnerText;

            if (!acceptedColors.Contains(color)) {
                throw new ArgumentException("Invalid color: " + color);
            }

            return color;

        }

        public List<Tile> ListOfTileParse(string listTileXml) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(listTileXml);

            List<Tile> tiles = new List<Tile>();
            foreach(XmlNode node in document.DocumentElement) {
                tiles.Add(TileParse(node.OuterXml)); 
            }

            return tiles;
        }

        public List<Tile> SetOfTileParse(string listTileXml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(listTileXml);

            List<Tile> tiles = new List<Tile>();
            foreach (XmlNode node in document.DocumentElement)
            {
                tiles.Add(TileParse(node.OuterXml));
            }

            HashSet<Tile> htiles = new HashSet<Tile>(tiles);
            // are we checking for unique tiles or orientations?
            if (htiles.Count != tiles.Count) {
                throw new ArgumentException("Tiles are not unique");
            }

            return tiles;
        }

        public List<string> ListOfColorParse(string listColorXml) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(listColorXml);

            List<string> colors = new List<string>();
            foreach(XmlNode node in document.DocumentElement) {
                colors.Add(ColorParse(node.OuterXml));
            }

            return colors;
        }

        public List<(string, List<Tile>)> ListOfSplayerParse(string listofSPlayerXml) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(listofSPlayerXml);

            List<(string, List<Tile>)> players = new List<(string, List<Tile>)>();
            foreach(XmlNode node in document.DocumentElement) {
                players.Add(SPlayerParse(node.OuterXml));
            }

            return players;
        }

        public (string, List<Tile>) SPlayerParse(string sPlayerXml) {
            // our server keeps track of the dragon tile so i think (hope)
            // we dont care about the dragontile tag for parsing
            XmlDocument document = new XmlDocument();
            document.LoadXml(sPlayerXml);

            string color = ColorParse(document.FirstChild.FirstChild.OuterXml);
            List <Tile> hand = ListOfTileParse(document.FirstChild.LastChild.OuterXml);

            return (color, hand);
        }

        public (int, int) XYParse(string xyXml) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xyXml);

            int x = Int32.Parse(document.DocumentElement.FirstChild.FirstChild.InnerText);
            int y = Int32.Parse(document.DocumentElement.LastChild.LastChild.InnerText);

            return (x, y);

        }

        public bool HVIsHorizontalParse(string hv)
        {
            // returns true if h
            XmlDocument document = new XmlDocument();
            document.LoadXml(hv);

            return (document.OuterXml.Trim() == "<h></h>");
        }

        public Position PawnLocationParse(string pLocation) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(pLocation);

            // position is for the tile lowest
            bool horizontal = HVIsHorizontalParse(document.FirstChild.ChildNodes[0].OuterXml);
            int line = NParse(document.FirstChild.ChildNodes[1].OuterXml);
            int tick = NParse(document.FirstChild.ChildNodes[2].OuterXml);

            int x = -1;
            int y = -1;
            int port = -1;

            if (horizontal) {
                y = line;
                x = tick / 2;
                port = tick % 2;
            } else {
                x = line;
                y = tick / 2;
                if (tick % 2 == 0) {
                    port = 7;
                } else {
                    port = 6;
                }
            }

            return new Position(x, y, port, true);
        }

        public Dictionary<string, Position> PawnsParse(string pawnsXml) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(pawnsXml);

            Dictionary<string, Position> pawnLocs = new Dictionary<string, Position>();
            XmlNode info = document.FirstChild;
            foreach (XmlNode ent in info.ChildNodes) {
                string color = ColorParse(ent.FirstChild.OuterXml);
                Position position = PawnLocationParse(ent.LastChild.OuterXml);
                pawnLocs[color] = position;
            }

            return pawnLocs;
        }

        public Dictionary<(int, int), Tile> MultiTilesParse(string tilesXml) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(tilesXml);

            Dictionary<(int, int), Tile> tileLocs = new Dictionary<(int, int), Tile>();
            XmlNode info = document.FirstChild;
            foreach (XmlNode ent in info.ChildNodes)
            {
                (int, int) coords = XYParse(ent.FirstChild.OuterXml);
                Tile tile = TileParse(ent.LastChild.OuterXml);
                tileLocs[coords] = tile;
            }

            return tileLocs;
        }

        public List<(string, List<Tile>)> MaybeSPlayerParse(string maybeXml) {
            if (maybeXml.Trim() == "<false></false>") {
                return null;
            }

            return ListOfSplayerParse(maybeXml);
        }

        public Board BoardParse(string boardXml) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(boardXml);

            string tilesXml = document.FirstChild.FirstChild.OuterXml;
            string pawnsXml = document.FirstChild.LastChild.OuterXml;

            Board board = new Board(6);
            Dictionary<(int, int), Tile> tiles = MultiTilesParse(tilesXml);

            foreach (KeyValuePair<(int, int), Tile> tilePos in tiles)
            {
                board.PlaceTile(tilePos.Value, tilePos.Key.Item1, tilePos.Key.Item2);
            }

            Dictionary<string, Position> pawns = PawnsParse(pawnsXml);
            foreach (KeyValuePair<string, Position> pawn in pawns) {
                Position position = pawn.Value;
                Position flipped = position.FlipPosition();

                // start of extremely ugly error checking / figuring out which
                // position a parsed position represents
                if (!position.OnEdge()) {
                    if (!flipped.OnEdge()) {
                        if (board.tiles[position.x][position.y] != null)
                        {
                            // on tile
                            if (board.tiles[flipped.x][flipped.y] != null)
                            {
                                // flipped on tile
                                throw new ArgumentException("Invalid Position on parsed board: in between two tiles");
                            }
                        }
                        else
                        {
                            // not on tile
                            if (board.tiles[flipped.x][flipped.y] != null)
                            {
                                // flipped on tile
                                position = flipped;
                            }
                            else
                            {
                                throw new ArgumentException("Invalid Position on parsed board: in between two empty spaces");
                            }
                        }
                    } else {
                        position = flipped;
                    }
                }

                board.tokenPositions[pawn.Key] = position;
            }

            return board;
        }

        public (Board, List<string>) EndGameParse(string end) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(end);

            string boardXml = document.FirstChild.FirstChild.OuterXml;
            Board board = BoardParse(boardXml);

            string colorXml = document.FirstChild.LastChild.OuterXml;
            List<string> winners = ListOfColorParse(colorXml);

            return (board, winners);
        }

        public Board PlacePawnParse(string place) {
            return BoardParse(place);
        }

        public Boolean VoidParse(string voidXml) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(voidXml);

            return (document.FirstChild.Name == "void");
        }

        public (string, List<string>) InitializeParse(string initXml) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(initXml);

            if (document.FirstChild.Name != "initialize") {
                throw new ArgumentException("<initialize> tag not found");
            }

            string color = ColorParse(document.FirstChild.FirstChild.OuterXml);
            List<string> allColors = ListOfColorParse(document.FirstChild.LastChild.OuterXml);
            return (color, allColors);
        }

        public (Board, List<Tile>, int) PlayTurnParse(string turn) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(turn);

            RootTagCheck("play-turn", turn);

            Board board = BoardParse(document.FirstChild.ChildNodes[0].OuterXml);
            List<Tile> hand = SetOfTileParse(document.FirstChild.ChildNodes[1].OuterXml);
            int unused = NParse(document.FirstChild.ChildNodes[2].OuterXml);

            return (board, hand, unused);
        }

        public void GetNameCheck(string input) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(input);

            RootTagCheck("get-name", input);
            if (document.FirstChild.InnerXml.Trim() != "") {
                throw new ArgumentException("invalid <get-name> tag format");
            }
        }

        public void RootTagCheck(string tag, string input) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(input);

            if (document.FirstChild.Name != tag)
            {
                throw new ArgumentException(tag + " tag not found");
            }
        }

    }
}
