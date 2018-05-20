using System;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class Tournament
    {
        static void Main(string[] args) {
            Server server = new Server();

            List<IPlayer> gamePlayers = new List<IPlayer>{
                new LeastSymmetricPlayer("lPlayer"),
                new MostSymmetricPlayer("mPlayer"),
                new RandomPlayer("rPlayer")
            };

            server.Play(gamePlayers);
        }
    }
}
