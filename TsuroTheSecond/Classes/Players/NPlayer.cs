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
        NetworkStream stream;


        public NPlayer(NetworkStream s)
        {
            toXml = new ToXml();
            parser = new Parser();
            stream = s;  
        }

        public string GetName() {
            string getname = "<get-name></get-name>";

            string response = byteStreamHelper(getname);

            return parser.PlayerNameParse(response);
        }


        public void Initialize(string _color, List<string> _allColors)
        {
            XElement InitXml = toXml.InitializetoXml(_color, _allColors);
            string init = toXml.FormatXml(InitXml);

            string response = byteStreamHelper(init);

    //        if (!parser.VoidParse(response)) {
    //            throw new ArgumentException("Network should have returned void");
    //        }
        }


        public Position PlacePawn(Board board)
        {
            XElement boardXml = toXml.PlacePawntoXml(board);
            string boardString = toXml.FormatXml(boardXml);

            string response = byteStreamHelper(boardString);

            Position pawnLocation = parser.PawnLocationParse(response);
            if (!pawnLocation.OnEdge()) {
                pawnLocation.FlipPosition(); 
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

            string response = byteStreamHelper(playTurnString);

            Tile playTile = parser.TileParse(response);
            return playTile;
        }


        public void EndGame(Board board, List<string> colors)
        {
            XElement endXml = toXml.EndGametoXml(board, colors);
            string endString = toXml.FormatXml(endXml);

            string response = byteStreamHelper(endString);

            if (!parser.VoidParse(response))
            {
                throw new ArgumentException("Network should have returned void");
            }
        }




        // takes input string from the various functions
        // returns output string 
        // 
        // converts string to bytes --> sends the bytes --> receives bytes back --> converts received bytes back to string
        public string byteStreamHelper(string inputString)
        {
            byte[] readBuffer = new byte[1024];
            byte[] writeBuffer = new byte[8192];

            // send message
            writeBuffer = Encoding.ASCII.GetBytes(inputString); // string to byte array
            stream.Write(writeBuffer, 0, writeBuffer.Length);   // send byte array

            // receive response
            int numberOfBytesRead = 0;
            StringBuilder completeMessage = new StringBuilder();
            // Incoming message may be larger than the buffer size.
            do
            {
                numberOfBytesRead = stream.Read(readBuffer, 0, readBuffer.Length);

                completeMessage.AppendFormat("{0}", Encoding.ASCII.GetString(readBuffer, 0, numberOfBytesRead));

            }
            while (stream.DataAvailable);
            return completeMessage.ToString();
        }

    }
}
