using System;
using System.Xml.Linq;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public static class PlayATurnNetwork
    {
        public static void PlayATurn() {
            Server server = new Server();
            Parser parser = new Parser();
            ToXml toXml = new ToXml();

            string deckXml = Console.ReadLine();
            string aliveXml = Console.ReadLine();
            string deadXml = Console.ReadLine();
            string boardXml = Console.ReadLine();
            string tileXml = Console.ReadLine();

            List<Tile> deck = parser.ListOfTileParse(deckXml);

            List<(string, List<Tile>)> aliveInfo = parser.ListOfSplayerParse(aliveXml);
            List<Player> alive = new List<Player>();
            foreach ((string, List<Tile>) playerInfo in aliveInfo) {
                Player player = new Player(new RandomPlayer("player"), playerInfo.Item1);
                alive.Add(player);
            }

            List<(string, List<Tile>)> deadInfo = parser.ListOfSplayerParse(deadXml);
            List<Player> dead = new List<Player>();
            foreach((string, List<Tile>) playerInfo in deadInfo) {
                Player player = new Player(new RandomPlayer("player"), playerInfo.Item1);
                dead.Add(player);
            }

            Board board = parser.BoardParse(boardXml);

            Tile tile = parser.TileParse(tileXml);

            server.deck = deck;
            server.alive = alive;
            server.dead = dead;
            server.board = board;
            server.gameState = Server.State.safe;

            var results = server.PlayATurn(deck, alive, dead, board, tile);

            string deckResultXml = FormatXmlWrapper(toXml.ListofTiletoXml(results.Item1));
            string aliveResultXml = FormatXmlWrapper(toXml.ListofPlayertoXml(results.Item2, -1));
            string deadResultXml = FormatXmlWrapper(toXml.ListofPlayertoXml(results.Item3, -1));
            string boardResultXml = FormatXmlWrapper(toXml.BoardtoXml(results.Item4));
            string maybeListofSplayerXml;
            if (results.Item5) {
                maybeListofSplayerXml = FormatXmlWrapper(toXml.ListofPlayertoXml(results.Item6, -1));
            } else {
                maybeListofSplayerXml = "<false></false>";
            }

            Console.WriteLine(deckResultXml);
            Console.WriteLine(aliveResultXml);
            Console.WriteLine(deadResultXml);
            Console.WriteLine(boardResultXml);
            Console.WriteLine(maybeListofSplayerXml);
        }

        static string FormatXmlWrapper(XElement xElement) {
            ToXml toXml = new ToXml();
            return toXml.FormatXml(xElement);
        }
    }
}
