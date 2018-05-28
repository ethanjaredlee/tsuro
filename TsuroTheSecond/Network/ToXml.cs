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
                                              new XElement("x", NtoXml(xy.Item1)),
                                              new XElement("y", NtoXml(xy.Item2)));
            return xyElement;
        }

        public XElement MultiTilesToXml(List<(Tile, (int, int))> tilePositions) {

            XElement multiTiles = new XElement("map");
            foreach ((Tile, (int, int)) t in tilePositions) {
                XElement ent = new XElement("ent");
            }

            return multiTiles;

        }

        public XElement BoardtoXml(Board b) {
            XElement board = new XElement("board");


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
