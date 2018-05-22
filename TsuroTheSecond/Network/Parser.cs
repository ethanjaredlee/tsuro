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

    }
}
