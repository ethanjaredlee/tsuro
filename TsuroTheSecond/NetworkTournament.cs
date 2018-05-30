using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace TsuroTheSecond
{
    public class Tournament
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> tournament = new Dictionary<string, int>();
            for (int i = 0; i < 50; i++)
            {
                Server server = new Server();
                TcpListener netserver = new TcpListener(IPAddress.Any, 10048);
                netserver.Start();

                List<IPlayer> gamePlayers = new List<IPlayer>{
                    new LeastSymmetricPlayer("lPlayer1"),
                    new MostSymmetricPlayer("mPlayer2"),
                    getNetworkPlayer(netserver),
                    new NPlayer("NPlayer4")
                };

                List<string> winners = server.Play(gamePlayers);
                foreach (string win in winners)
                {
                    if (tournament.ContainsKey(win))
                    {
                        tournament[win]++;
                    }
                    else
                    {
                        tournament[win] = 1;
                    }
                }
            }

            foreach (KeyValuePair<string, int> result in tournament)
            {
                Console.WriteLine(result.Key + " wins: " + result.Value);
            }
        }
        public static NPlayer getNetworkPlayer(TcpListener s)
        {
            Console.WriteLine("waiting for connection from remote NPlayer");
            TcpClient player = s.acceptTcpClient();
            Console.WriteLine("Connection received");
            NetworkStream stream = player.GetStream();
            return new NPlayer(stream);
        }
    }
}
