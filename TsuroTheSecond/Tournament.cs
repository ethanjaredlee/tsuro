using System;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public static class Tournament
    {
        public static void RunTournament(Boolean verbose) {
            int games = 1000;
            Dictionary<string, int> tournament = new Dictionary<string, int>();
            for (int i = 0; i < games; i++) {

                if (i % 100 == 0) {
                    Console.WriteLine("playing game: " + i);
                }
                Server server = new Server();

                if (verbose) {
                    server.verbose = true;
                }

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

            Console.WriteLine("After " + games + " games played, here are the results!");
            foreach (KeyValuePair<string, int> result in tournament) {
                Console.WriteLine(result.Key + " wins: " + result.Value);
            }
        }
    }
}
