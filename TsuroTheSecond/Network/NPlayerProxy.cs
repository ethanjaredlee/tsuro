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

        public NPlayerProxy(string name)
        {
            player = new MostSymmetricPlayer(name);
            ToXml = new ToXml();
            Parser = new Parser();
        }

        /* 
         * returns an xml-string representing proper output
         * to the input xml
         */
        public (string, Boolean) ParseInput(string input)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(input);

            string message = document.FirstChild.Name;
            string response;
            Boolean done = false;
            switch (message)
            {
                case "get-name":
                    string name = player.GetName();
                    response = ToXml.FormatXml(ToXml.NametoXml(name));
                    Console.WriteLine(response);

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
                    done = true;
                    break;
                default:
                    throw new ArgumentException("invalid xml given");
            }

            return (response, done);
        }


        public static void RunNPlayerProxy(string name, int port) // input host IP address and port number as 2 args
        {
            NPlayerProxy player = new NPlayerProxy(name);

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
            IPAddress ipAddress = ipHostInfo.AddressList[0];  
            IPEndPoint remoteEP = new IPEndPoint(ipAddress,port);  

            // Create a TCP/IP  socket.  
            Socket sender = new Socket(ipAddress.AddressFamily,   
                SocketType.Stream, ProtocolType.Tcp );

            sender.Connect(remoteEP);
            Console.WriteLine("connected to tournament!");

            NetworkStream networkStream = new NetworkStream(sender);
            StreamWriter writer = new StreamWriter(networkStream);
            StreamReader reader = new StreamReader(networkStream);

            while (true)
            {

                string incoming = reader.ReadLine();
                Console.WriteLine("Got message: " + incoming);

                (string, Boolean) response = player.ParseInput(incoming);
                Console.WriteLine("Sending message: " + response.Item1);

                writer.WriteLine(response.Item1);
                writer.Flush();

                if (response.Item2) {
                    Console.WriteLine("done");

                    break; 
                }
            }

        }

    }
}