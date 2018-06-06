using System;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
namespace TsuroTheSecond
{
    public class ToXml
    {
        /*
         * for the functions that we use in NPlayer, can prob return string
         * instead of an XElement
         */

        public string FormatXml(XElement xml)
        {
            return xml.ToString().Replace("\n", "").Replace(" ", "");
        }

        public XElement NametoXml(string name)
        {
            XElement nameXml = new XElement("player-name", name);
            return nameXml;
        }

        public XElement NtoXml(int n) {
            XElement nXml = new XElement("n", n);
            return nXml;
        }

        public XElement TiletoXml(Tile t)
        {
            XElement tile = new XElement("tile");
            foreach(List<int> connect in t.paths) {
                XElement con = new XElement("connect",
                                            NtoXml(connect[0]),
                                            NtoXml(connect[1]));
                tile.Add(con);
            }
            return tile;
        }

        public XElement SetofTiletoXml(List<Tile> tiles) {
            XElement set = new XElement("set");
            foreach (Tile t in tiles) {
                set.Add(TiletoXml(t));
            }

            return set;
        }

        public XElement ListofTiletoXml(List<Tile> tiles)
        {
            XElement list = new XElement("list", "");
            foreach (Tile t in tiles)
            {
                list.Add(TiletoXml(t));
            }

            return list;
        }

        public XElement SetofColortoXml(List<string> colors) {
            XElement set = new XElement("set");
            foreach (string color in colors) {
                set.Add(new XElement("color", color));
            }
            return set;
        }

        public XElement XYtoXml((int, int) xy) {
            XElement xyElement = new XElement("xy",
                                              new XElement("x", xy.Item1),
                                              new XElement("y", xy.Item2));
            return xyElement;
        }

        public XElement ColorListtoXml(List<string> colors) {
            XElement colorList = new XElement("list");
            foreach(string color in colors) {
                XElement colorXml = new XElement("color", color);
                colorList.Add(colorXml);
            }

            return colorList;
        }

        public XElement MultiTilesToXml(List<(Tile, (int, int))> tilePositions) {

            XElement multiTiles = new XElement("map");
            foreach ((Tile, (int, int)) t in tilePositions) {
                XElement ent = new XElement("ent",
                                            XYtoXml(t.Item2),
                                            TiletoXml(t.Item1));
                multiTiles.Add(ent);
            }

            return multiTiles;

        }

        public XElement PawnLoctoXml(Position position)
        {
            XElement p = new XElement("pawn-loc");

            Boolean horizontal;
            int line;
            int dash;

            if (position.port == 0 || position.port == 1)
            {
                horizontal = true;
                line = position.y;
                dash = position.x * 2;
                if (position.port == 1) dash++;
            }
            else if (position.port == 4 || position.port == 5)
            {
                horizontal = true;
                line = position.y + 1;
                dash = position.x * 2;
                if (position.port == 4) dash++;
            }
            else if (position.port == 2 || position.port == 3) {
                horizontal = false;
                line = position.x + 1;
                dash = position.y * 2;
                if (position.port == 3) dash++;
            }
            else if (position.port == 6 || position.port == 7) {
                horizontal = false;
                line = position.x;
                dash = position.y * 2;
                if (position.port == 6) dash++;
            } else {
                throw new ArgumentException("Port number doesn't exist in range");
            }

            XElement hv = horizontal ? new XElement("h", "") : new XElement("v", "");
            p.Add(hv,
                  NtoXml(line),
                  NtoXml(dash));

            return p;
        }

        public XElement BoardtoXml(Board b) {
            XElement board = new XElement("board");

            List<(Tile, (int, int))> tilePositions = new List<(Tile, (int, int))>();
            for (int i = 0; i < b.tiles.Count; i++) {
                for (int j = 0; j < b.tiles.Count; j++) {
                    if (b.tiles[i][j] == null) continue;

                    tilePositions.Add((b.tiles[i][j], (i, j)));
                } 
            }

            XElement tiles = MultiTilesToXml(tilePositions);
            XElement map = new XElement("map");

            foreach (KeyValuePair<string, Position> pawnLoc in b.tokenPositions) {
                XElement pawn = new XElement("ent",
                                             new XElement("color", pawnLoc.Key),
                                             PawnLoctoXml(pawnLoc.Value));
                map.Add(pawn);
            }

            board.Add(tiles, map);

            return board;
        }

        public XElement InitializetoXml(string color, List<string> allColors) {
            XElement initialize = new XElement("initialize",
                                               new XElement("color", color),
                                               ColorListtoXml(allColors));
            return initialize;
        }

        public XElement PlacePawntoXml(Board board) {
            XElement placePawn = new XElement("place-pawn",
                                              BoardtoXml(board));
            return placePawn;
        }

        public XElement PlayTurntoXml(Board board, List<Tile> hand, int unused) {
            XElement turn = new XElement("play-turn",
                                         BoardtoXml(board),
                                         SetofTiletoXml(hand),
                                         NtoXml(unused));
            return turn;
        }

        public XElement EndGametoXml(Board board, List<string> colors) {
            XElement end = new XElement("end-game",
                                        BoardtoXml(board),
                                        SetofColortoXml(colors));
            return end;
        }

        public XElement PlayertoXml(Player player, Boolean dragonTileHolder) {
            XElement root;
            if (dragonTileHolder) {
                root = new XElement("splayer-dragon");
            } else {
                root = new XElement("splayer-nodragon");
            }

            root.Add(new XElement("color", player.Color), SetofTiletoXml(player.Hand));
            return root;
            
        }

        public XElement ListofPlayertoXml(List<Player> players, int indexOfDragonTile) {
            // set indexOfDragonTile <- -1 if there is no dragonTileHolder
            XElement root = new XElement("list");
            for (int i = 0; i < players.Count; i++) {
                if (i == indexOfDragonTile) {
                    XElement dragontilePlayer = PlayertoXml(players[i], true);
                    root.Add(dragontilePlayer);
                } else {
                    XElement nondragonTilePlayer = PlayertoXml(players[i], false);
                    root.Add(nondragonTilePlayer);
                }
            }
            return root;
        }

        public string VoidtoXml() {
            XElement element = new XElement("void", "");
            return FormatXml(element);
        }


    }
}
