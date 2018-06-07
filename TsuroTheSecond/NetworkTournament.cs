using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace TsuroTheSecond
{
    public static class NetworkTournament
    {
        public static void RunNetworkTournament(Boolean verbose)
        {
            Server server = new Server();

            if (verbose)
            {
                server.verbose = true;
            }

            TcpListener netserver = new TcpListener(IPAddress.Any, 10048);
            netserver.Start();

            List<IPlayer> gamePlayers = new List<IPlayer>{
                new LeastSymmetricPlayer("lPlayer1"),
                new MostSymmetricPlayer("mPlayer2"),
                getNetworkPlayer(netserver),
                new RandomPlayer("RPlayer4")
            };

        }


        public static NPlayer getNetworkPlayer(TcpListener s)
        {
            Console.WriteLine("waiting for connection from remote NPlayer");
            TcpClient player = s.AcceptTcpClient();
            Console.WriteLine("Connection received");
            NetworkStream stream = player.GetStream();
            return new NPlayer(stream);
        }
    }
}
