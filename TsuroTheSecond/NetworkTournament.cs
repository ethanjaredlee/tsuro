using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace TsuroTheSecond
{
    public static class NetworkTournament
    {
        public static void RunNetworkTournament(int playerCount)
        {
            //Server server = new Server();

            //TcpListener netserver = new TcpListener(IPAddress.Any, 10048);
            //netserver.Start();

            //List<IPlayer> gamePlayers = new List<IPlayer>();

            //Console.WriteLine("Do you want to add a network player? (y/n)");
            //string response = Console.ReadLine();
            //while (response.Trim().ToLower() == "y" && gamePlayers.Count <= playerCount) {
            //    NPlayer player = getNetworkPlayer(netserver);
            //    gamePlayers.Add(player);
            //    Console.WriteLine("Do you want to add another player? (y/n)");
            //    response = Console.ReadLine();
            //}

            //while (gamePlayers.Count < playerCount) {
            //    gamePlayers.Add(new RandomPlayer("filler player"));
            //}

            //Console.WriteLine("\n\nStarting game with " + gamePlayers.Count + " players");
            //List<string> winners = server.Play(gamePlayers);
            //foreach (string win in winners) {
            //    Console.WriteLine(win + " won!");
            //}

        }


        //public static NPlayer getNetworkPlayer(IPAddress address)
        //{
        //    Console.WriteLine("waiting for connection from remote NPlayer");
        //    TcpClient player = s.AcceptTcpClient();
        //    Console.WriteLine("Connection received");
        //    NetworkStream stream = player.GetStream();
        //    return new NPlayer(stream, "Nplayer");
        //}
    }
}
