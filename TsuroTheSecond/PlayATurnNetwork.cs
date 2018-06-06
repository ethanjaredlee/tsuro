using System;
using System.Xml.Linq;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public static class PlayATurnNetwork
    {
        public static (string, string, string, string, string) PlayATurn(string deckXml, string aliveXml, string deadXml, string boardXml, string tileXml)
        {
            Parser parser = new Parser();
            ToXml toXml = new ToXml();

            List<Tile> deck = parser.ListOfTileParse(deckXml);

            Tile tile = parser.TileParse(tileXml);

            List<(string, List<Tile>, bool)> aliveInfo = parser.ListOfSplayerParse(aliveXml);
            List<Player> alive = new List<Player>();
            List<Player> dragonQueue = new List<Player>();
            bool addToDragonQueue = false;
            foreach ((string, List<Tile>, bool) playerInfo in aliveInfo)
            {
                Player player = new Player(new RandomPlayer("player"), playerInfo.Item1);

                player.Hand = playerInfo.Item2;

                // fancy or because you want to add all players behind the player with dragontile to dqueue
                if (playerInfo.Item3 || addToDragonQueue)
                {
                    dragonQueue.Add(player);
                    addToDragonQueue = true;
                }
                alive.Add(player);
            }

            // have to add players to dragonQueue if they have to be there multiple times
            foreach (Player p in alive) {
                int playerHandCount = p.Hand.Count;
                // the first player technically has 1 more tile, it's just being played
                if (p.Color == alive[0].Color) playerHandCount++;
                int playerInDragonQueueCount = 0;

                // check how many times the player is in the dragonQueue
                foreach (Player dragonPlayer in dragonQueue) {
                    if (p.Color == dragonPlayer.Color) playerInDragonQueueCount++;
                }

                if (playerHandCount + playerInDragonQueueCount < 3) {
                    dragonQueue.Add(p);
                }
            }

            // run this again, just making sure -- player has 0 tiles and is playing a tile
            foreach (Player p in alive)
            {
                int playerHandCount = p.Hand.Count;
                // the first player technically has 1 more tile, it's just being played
                if (p.Color == alive[0].Color) playerHandCount++;
                int playerInDragonQueueCount = 0;

                // check how many times the player is in the dragonQueue
                foreach (Player dragonPlayer in dragonQueue)
                {
                    if (p.Color == dragonPlayer.Color) playerInDragonQueueCount++;
                }

                if (playerHandCount + playerInDragonQueueCount < 3)
                {
                    dragonQueue.Add(p);
                }
            }

            List<(string, List<Tile>, bool)> deadInfo = parser.ListOfSplayerParse(deadXml);
            List<Player> dead = new List<Player>();
            foreach ((string, List<Tile>, bool) playerInfo in deadInfo)
            {
                Player player = new Player(new RandomPlayer("player"), playerInfo.Item1);
                dead.Add(player);
            }

            Board board = parser.BoardParse(boardXml);

            Server server = new Server(deck, alive, dead, board, dragonQueue, Server.State.safe);

            var results = server.PlayATurn(deck, alive, dead, board, tile);

            string deckResultXml = FormatXmlWrapper(toXml.ListofTiletoXml(results.Item1));

            int dragonQueueIndex = -1;
            if (server.dragonQueue.Count > 0) {
                // find player who has the dragontile
                Player dragonTileHolder = server.dragonQueue[0];
                for (int i = 0; i < alive.Count; i++) {
                    if (alive[i].Color == dragonTileHolder.Color) {
                        dragonQueueIndex = i;
                    }
                }
            } 


            string aliveResultXml = FormatXmlWrapper(toXml.ListofPlayertoXml(results.Item2, dragonQueueIndex));
            string deadResultXml = FormatXmlWrapper(toXml.ListofPlayertoXml(results.Item3, -1));
            string boardResultXml = FormatXmlWrapper(toXml.BoardtoXml(results.Item4));
            string maybeListofSplayerXml;
            if (results.Item5)
            {
                maybeListofSplayerXml = FormatXmlWrapper(toXml.ListofPlayertoXml(results.Item6, -1));
            }
            else
            {
                maybeListofSplayerXml = "<false></false>";
            }

            return (deckResultXml, aliveResultXml, deadResultXml, boardResultXml, maybeListofSplayerXml);
        }

        public static void TestPlayATurn() {

            while (true) {
                string deckXml = Console.ReadLine();
                string aliveXml = Console.ReadLine();
                string deadXml = Console.ReadLine();
                string boardXml = Console.ReadLine();
                string tileXml = Console.ReadLine();

                var result = PlayATurn(deckXml, aliveXml, deadXml, boardXml, tileXml);

                Console.WriteLine(result.Item1);
                Console.WriteLine(result.Item2);
                Console.WriteLine(result.Item3);
                Console.WriteLine(result.Item4);
                Console.WriteLine(result.Item5);
            }
        }

        static string FormatXmlWrapper(XElement xElement)
        {
            ToXml toXml = new ToXml();
            return toXml.FormatXml(xElement);
        }
    }
}
