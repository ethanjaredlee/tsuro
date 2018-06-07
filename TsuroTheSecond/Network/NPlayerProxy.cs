using System;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TsuroTheSecond
{
    public class NPlayerProxy
    {
        IPlayer player;
        ToXml ToXml;
        Parser Parser;

        public NPlayerProxy()
        {
            player = new MostSymmetricPlayer("network player");
            ToXml = new ToXml();
            Parser = new Parser();

        }

        /* 
         * returns an xml-string representing proper output
         * to the input xml
         */
        public string ParseInput(string input)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(input);

            string message = document.FirstChild.Name;
            string response;
            switch (message)
            {
                case "get-name":
                    string name = player.GetName();
                    response = ToXml.FormatXml(ToXml.NametoXml(name));

                    break;
                case "initialize":
                    (string, List<string>) init = Parser.InitializeParse(input);

                    player.Initialize(init.Item1, init.Item2);

                    response = ToXml.VoidtoXml();
                    break;
                case "place-pawn":
                    Board board = Parser.PlacePawnParse(input);

                    Position initPosition = player.PlacePawn(board);

                    response = ToXml.FormatXml(ToXml.PawnLoctoXml(initPosition));
                    break;
                case "play-turn":
                    (Board, List<Tile>, int) turnInput = Parser.PlayTurnParse(input);

                    Tile playTile = player.PlayTurn(turnInput.Item1, turnInput.Item2, turnInput.Item3);

                    response = ToXml.FormatXml(ToXml.TiletoXml(playTile));
                    break;
                case "end-game":
                    (Board, List<string>) endGame = Parser.EndGameParse(input);

                    player.EndGame(endGame.Item1, endGame.Item2);

                    response = ToXml.VoidtoXml();
                    break;
                default:
                    throw new ArgumentException("invalid xml given");
            }

            return response;
        }


        public static void RunNPlayerProxy() // input host IP address and port number as 2 args
        {
            NPlayerProxy player = new NPlayerProxy();

            // line comments here are alternate statements for passing these values as args to Main
            string hostname = "localhost";  // args[0];
            int port = 10048;               // Convert.ToInt32(args[1]);
            TcpClient client = new TcpClient(hostname, port);

            NetworkStream stream = client.GetStream();
            byte[] readBuffer = new byte[1024];
            byte[] writeBuffer = new byte[8192];     // this may need to be bigger. not sure the # of bytes of a full board xml string 
            int numberOfBytesRead = 0;
            StringBuilder completeMessage = new StringBuilder();

            while (true)
            {
                completeMessage.Clear();

                do
                {
                    numberOfBytesRead = stream.Read(readBuffer, 0, readBuffer.Length);

                    completeMessage.AppendFormat("{0}", Encoding.ASCII.GetString(readBuffer, 0, numberOfBytesRead));

                }
                while (stream.DataAvailable);

                string response = player.ParseInput(completeMessage.ToString());

                writeBuffer = Encoding.ASCII.GetBytes(response);
                stream.Write(writeBuffer, 0, writeBuffer.Length);

            }


        }

    }
}