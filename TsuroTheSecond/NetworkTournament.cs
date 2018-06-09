using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace TsuroTheSecond
{
    public static class NetworkTournament
    {
        public static void RunNetworkTournament(int playerCount, int port=12345)
        {
            Server server = new Server();


            /********* game initialization ************/
            List<IPlayer> gamePlayers = new List<IPlayer>();

            List<NPlayer> player = GetNetworkPlayers(port, playerCount);
            gamePlayers.AddRange(player);

            while (gamePlayers.Count < 8) {
                Console.WriteLine("Adding filler player");
                gamePlayers.Add(new RandomPlayer("filler player"));
            }

            Console.WriteLine("\n\nStarting game with " + gamePlayers.Count + " players");
            List<string> winners = server.Play(gamePlayers);
            foreach (string win in winners) {
                Console.WriteLine(win + " won!");
            }

        }


        public static List<NPlayer> GetNetworkPlayers(int port, int count)
        {
            List<NPlayer> players = new List<NPlayer>();
            /********* network initialization ***********/
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress localAddress = ipHostInfo.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(localAddress, port);


            Socket receiver = new Socket(localAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            receiver.Bind(endPoint);
            receiver.Listen(1000);

            while (players.Count < count) {

                Console.WriteLine("Waiting for connection ...");

                Socket connected = receiver.Accept();
                NetworkStream networkStream = new NetworkStream(connected);

                players.Add(new NPlayer(networkStream, "nPlayer"));
                Console.WriteLine("connected player " + players.Count + "/" + count);
            }

            return players;
        }
    }
}
