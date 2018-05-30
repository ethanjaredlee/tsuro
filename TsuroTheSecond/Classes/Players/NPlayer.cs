using System;
using System.Collections.Generic;
using System.Xml.Linq;
namespace TsuroTheSecond
{
    public class NPlayer : IPlayer
    {
        string color;
        string name;
        List<string> allColors;
        ToXml toXml;
        Parser parser;

        public NPlayer(string _name)
        {
            name = _name;
            toXml = new ToXml();
            parser = new Parser();
        }

        public string GetName() {
            return "hi";
        }

        public void Initialize(string _color, List<string> _allColors) {
            XElement InitXml = toXml.InitializetoXml(_color, _allColors);
            string init = toXml.FormatXml(InitXml);

            // server is sending this
            Console.WriteLine(init);

            // receiving back this
            string response = Console.ReadLine();

            if (!parser.VoidParse(response)) {
                throw new ArgumentException("Network should have returned void");
            }
        }

        public Position PlacePawn(Board board) {
            XElement boardXml = toXml.PlacePawntoXml(board);
            string boardString = toXml.FormatXml(boardXml);

            Console.WriteLine(boardString);

            string response = Console.ReadLine();

            Position pawnLocation = parser.PawnLocationParse(response);
            if (!pawnLocation.OnEdge()) {
                pawnLocation.FlipPosition(); 
            }

            if (!pawnLocation.OnEdge()) {
                throw new Exception("Initial pawn location should be on the edge of the board");
            }
            return pawnLocation;
        }

        public Tile PlayTurn(Board board, List<Tile> hand, int unused) {
            XElement playTurnXml = toXml.PlayTurntoXml(board, hand, unused);
            string playTurnString = toXml.FormatXml(playTurnXml);

            Console.WriteLine(playTurnString);

            string response = Console.ReadLine();

            Tile playTile = parser.TileParse(response);
            return playTile;
        }

        public void EndGame(Board board, List<string> colors) {
            XElement endXml = toXml.EndGametoXml(board, colors);
            string endString = toXml.FormatXml(endXml);

            Console.WriteLine(endString);

            string response = Console.ReadLine();

            if (!parser.VoidParse(response))
            {
                throw new ArgumentException("Network should have returned void");
            }
        }
    }
}
