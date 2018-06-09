using System;
using System.Linq;
namespace TsuroTheSecond
{
    public class Program
    {
        static void Main(string[] args) {
            if (args.Contains("tournament")) {
                Tournament.RunTournament(args.Contains("--verbose"));
            }

            else if (args.Contains("playturn")) {
                PlayATurnNetwork.TestPlayATurn();
            }

            else if (args.Contains("player"))
            {
                int port = args.Length < 3 ? 12345 : Int32.Parse(args[2]);
                string ip = args.Length < 4 ? null : args[3];
                NPlayerProxy.RunNPlayerProxy(args[1], port, ip);
            } 

            else if (args.Contains("host")){
                NetworkTournament.RunNetworkTournament(Int32.Parse(args[1]));
            } else {
                Console.WriteLine("Available arguments:");
                Console.WriteLine("<tournament>: run internal tournament simulation");
                Console.WriteLine("<playturn>: test playATurn function on stdin and stdout");
                Console.WriteLine("<player> name: launch a player to connect to network with name");
                Console.WriteLine("<host> n: host a network tournament with n players in the game");
            }

            //NetworkTournament.RunNetworkTournament(8);
            NPlayerProxy.RunNPlayerProxy("ethan", 12345);

            Console.WriteLine("done with program");
        }
    }
}
