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

        public void ParseXML() {
            
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
                throw new ArgumentException("Invalid color");
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

            foreach(Tile t in htiles) {
                Console.WriteLine(t);
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
        }
    }
}
