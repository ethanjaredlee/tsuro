using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Net.Sockets;
using System.IO;
using System.Text;
namespace TsuroTheSecond
{
      
    public class NPlayer : IPlayer
    {
        ToXml toXml;
        Parser parser;
        string name;
        StreamWriter writer;
        StreamReader reader;


        public NPlayer(NetworkStream s, string name)
        {
            toXml = new ToXml();
            parser = new Parser();
            this.name = name;
            writer = new StreamWriter(s);
            reader = new StreamReader(s);
        }

        private string WriteAndRead(string input) {
            writer.WriteLine(input);
            writer.Flush();

            return reader.ReadLine();
        }

        public string GetName() {
            string getname = "<get-name></get-name>";

            string response = WriteAndRead(getname);

            return parser.PlayerNameParse(response);
        }


        public void Initialize(string _color, List<string> _allColors)
        {
            XElement InitXml = toXml.InitializetoXml(_color, _allColors);
            string init = toXml.FormatXml(InitXml);

            string response = WriteAndRead(init);
        }


        public Position PlacePawn(Board board)
        {
            XElement boardXml = toXml.PlacePawntoXml(board);
            string boardString = toXml.FormatXml(boardXml);

            string response = WriteAndRead(boardString);

            Position pawnLocation = parser.PawnLocationParse(response);
            if (!pawnLocation.OnEdge()) {
                pawnLocation = pawnLocation.FlipPosition(); 
            }

            if (!pawnLocation.OnEdge()) {
                throw new Exception("Initial pawn location should be on the edge of the board");
            }
            return pawnLocation;
        }


        public Tile PlayTurn(Board board, List<Tile> hand, int unused)
        {
            XElement playTurnXml = toXml.PlayTurntoXml(board, hand, unused);
            string playTurnString = toXml.FormatXml(playTurnXml);

            string response = WriteAndRead(playTurnString);

            Tile playTile = parser.TileParse(response);
            return playTile;
        }


        public void EndGame(Board board, List<string> colors)
        {
            XElement endXml = toXml.EndGametoXml(board, colors);
            string endString = toXml.FormatXml(endXml);

            string response = WriteAndRead(endString);

            if (!parser.VoidParse(response))
            {
                throw new ArgumentException("Network should have returned void");
            }
        }

    }
}
