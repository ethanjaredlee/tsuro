using System;
using System.Collections.Generic;
using System.Xml.Linq;
namespace TsuroTheSecond
{
    public class NPlayer : IPlayer
    {
        string color;
        List<string> allColors;
        ToXml toXml;
        Parser parser;

        public NPlayer()
        {
            toXml = new ToXml();
            parser = new Parser();
        }

        public string GetName() {
            return "hi";
        }

        public void Initialize(string _color, List<string> _allColors) {
            XElement InitXml = toXml.InitializetoXml(_color, _allColors);
            string init = toXml.FormatXml(InitXml);

            // send this to the networked server
            Console.WriteLine(init);

            // get response from networked server
            string response = Console.ReadLine();

            if (!parser.VoidParse(response)) {
                throw new ArgumentException("Network should have returned void");
            }
        }

        public Position PlacePawn(Board board) {
            return null;
        }

        public Tile PlayTurn(Board board, List<Tile> hand, int unused) {
            return null;
        }

        public void EndGame(Board board, List<string> colors) {
        }
    }
}
