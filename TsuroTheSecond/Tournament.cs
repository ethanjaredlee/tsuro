using System;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class Tournament
    {
        static void Main(string[] args) {
            Dictionary<string, int> tournament = new Dictionary<string, int>();
            for (int i = 0; i < 50; i++) {
                Server server = new Server();

                List<IPlayer> gamePlayers = new List<IPlayer>{
                    new LeastSymmetricPlayer("lPlayer1"),
                    new MostSymmetricPlayer("mPlayer2"),
                    new RandomPlayer("rPlayer3"),
                    new LeastSymmetricPlayer("lPlayer4"),
                    new MostSymmetricPlayer("mPlayer5"),
                    new RandomPlayer("rPlayer6")
                };

                List<string> winners = server.Play(gamePlayers);
                foreach (string win in winners)
                {
                    if (tournament.ContainsKey(win)) {
                        tournament[win]++;
                    } else {
                        tournament[win] = 1;
                    }
                }
            }

            foreach (KeyValuePair<string, int> result in tournament) {
                Console.WriteLine(result.Key + " wins: " + result.Value);
            }
        }
    }
}
