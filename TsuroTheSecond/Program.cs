using System;
using System.Linq;
namespace TsuroTheSecond
{
    public class Program
    {
        static void Main(string[] args) {
            if (args.Contains("--tournament")) {
                Tournament.RunTournament(args.Contains("--verbose"));
            }

            if (args.Contains("--play")) {
                PlayATurnNetwork.PlayATurn();
            }
        }
    }
}
