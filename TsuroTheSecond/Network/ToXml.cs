using System;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
namespace TsuroTheSecond
{
    public class ToXml
    {

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

        public XElement XYtoXml((int, int) xy) {
            XElement xyElement = new XElement("xy",
                                              new XElement("x", xy.Item1),
                                              new XElement("y", xy.Item2));
            return xyElement;
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

        ////public string BoardtoXml(Board b) { }

        //public string LocationtoXml(int x, int y, int p)
        //{
        //    string orientation;
        //    string line;
        //    string path;
        //    if (p == 0 || p == 1)
        //    {
        //        orientation = horizontal;
        //        line = y.ToString();
        //        if (p == 0) { path = (x * 2).ToString(); }
        //        else { path = ((x * 2) + 1).ToString(); }
        //    }
        //    else if (p == 4 || p == 5)
        //    {
        //        orientation = horizontal;
        //        line = (y + 1).ToString();
        //        if (p == 5) { path = (x * 2).ToString(); }
        //        else { path = ((x * 2) + 1).ToString(); }
        //    }
        //    else if(p == 2 || p == 3)
        //    {
        //        orientation = vertical;
        //        line = x.ToString();
        //        if (p == 2) { path = (y * 2).ToString(); }
        //        else { path = ((y * 2) + 1).ToString(); }
        //    }
        //    else if (p == 6 || p == 7)
        //    {
        //        orientation = vertical;
        //        line = (x + 1).ToString();
        //        if (p == 7) { path = (y * 2).ToString(); }
        //        else { path = ((y * 2) + 1).ToString(); }
        //    }
        //    else
        //    {
        //        throw new ArgumentException("This is not a valid path position");
        //    }

        //    string ret =  "<pawn-loc>{0}<n>{1}</n><n>{2}</n></pawn-loc>", orientation, line, path;
        //    return ret;
        //}

    }
}
