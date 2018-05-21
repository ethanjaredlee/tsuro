using System;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class Tournament
    {
        static void Main(string[] args) {
            Server server = new Server();

            List<IPlayer> gamePlayers = new List<IPlayer>{
                new LeastSymmetricPlayer("lPlayer1"),
                new MostSymmetricPlayer("mPlayer2"),
                new RandomPlayer("rPlayer3"),
                new LeastSymmetricPlayer("lPlayer4"),
                new MostSymmetricPlayer("mPlayer5"),
                new RandomPlayer("rPlayer6")
            };

            server.Play(gamePlayers);
        }
    }
}
