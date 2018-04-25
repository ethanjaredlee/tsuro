using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class Server 
    {

        public List<Tile> deck;
        public List<Player> alive;
        public List<Player> dead;
        public Board board;
        public List<Player> dragonQueue; // whoever is the first person of the queue has the tile

        public Server() {
            // initializes the game
            deck = Constants.tiles; // shuffle on server initialization
            alive = new List<Player>();
            dead = new List<Player>();
            board = new Board(Constants.boardSize);
        }

        public void AddPlayer(IPlayer p, int age, string color) {
            // somehow check that at least 2 players are in teh game?

            if (alive.Count >= 8) {
                throw new InvalidOperationException("Only 8 players allowed in game");
            }
            // todo organize alive by age and don't let players pick duplicate colors
            alive.Add(new Player(p, age, color));
        }

        public List<Tile> ShuffleDeck(List<Tile> deck)
        {
            // doesnt quite work the way i want it to yet
            List<Tile> shuffledDeck = new List<Tile>(new Tile[deck.Count]);
            Random rng = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Tile tile = deck[k];
                deck[k] = deck[n];
                deck[n] = tile;
            }
            return deck;
        }

        Boolean LegalPlay(Player player, Board b, Tile tile) {
            // keep this in for iterating through the loop
            if (tile == null) {
                return false;
            }

            return (ValidTilePlacement(b, player, tile) && player.TileinHand(tile));
        }

        Boolean ValidTilePlacement(Board b, Player player, Tile tile) {
            // checks if placing a tile on the board will kill the player 
            Boolean playerAlive = true;
            b.PlaceTile(tile, player.nextTilePosition[0], player.nextTilePosition[1]);
            List<int> origPosition = new List<int>(player.position);
            player.UpdatePosition(b);

            playerAlive = !player.IsDead();

            // undoing changes to the board
            b.PlaceTile(null, player.nextTilePosition[0], player.nextTilePosition[1]);
            player.position = new List<int>(origPosition);
            return playerAlive;
        }

        void PlayATurn(List<Tile> deck, List<Player> alive, List<Player> dead, Board board, Tile tile) {
            //while (alive.Count > 0) {
            //    Tile chosenTile = null;

            //    while (!LegalPlay(chosenTile)) {
            //        Player currentPlayer = alive[0];
            //        chosenTile = currentPlayer.player.chooseTile(board);
            //    }
            //}
            Player currentPlayer = alive[0];
            currentPlayer.RemoveTilefromHand(tile);
            board.PlaceTile(tile, currentPlayer.nextTilePosition[0], currentPlayer.nextTilePosition[1]);
            List<Player> fatalities = new List<Player>();
            foreach (Player p in alive) {
                p.UpdatePosition(board);
                if (p.IsDead()) {
                    fatalities.Add(p);
                }
            }

            if (alive.Count == 1) {
                WinGame(alive);
            }

            if (alive.Count == 0) {
                WinGame(fatalities);
            }

            foreach (Player p in fatalities) {
                KillPlayer(p);
            }

            DrawTile(currentPlayer, deck);
        }

        void KillPlayer(Player player) {
            dead.Add(player);
            alive.Remove(player);

            if (dragonQueue.Contains(player)) {
                dragonQueue.Remove(player);
            }

            // distribute player hand to whoevers waiting in queue or just add to deck
            if (player.Hand.Count > 0) {
                deck.AddRange(player.Hand);
                int dragonCount = dragonQueue.Count;
                for (int i = 0; i < dragonCount; i++) {
                    DrawTile(dragonQueue[i], deck);
                    dragonQueue.Remove(dragonQueue[i]);
                }
            }
        }

        public void WinGame(List<Player> winners) {
        }

        public void DrawTile(Player player, List<Tile> d) {
            // dragon tile isnt implemented
            // how is this supposed to work with an interface?
            Console.WriteLine(player.Hand.Count);
            if (player.Hand.Count >= 3) {
                throw new InvalidOperationException("Player can't have more than 3 cards in hand");
            }

            if (deck.Count <= 0) {
                dragonQueue.Add(player);   
            } else {
                Tile t = d[0];
                d.RemoveAt(0);
                player.AddTiletoHand(t); 
            }

        }

        static void Main(string[] args)
        {
            Server server = new Server();
            server.deck = server.deck.GetRange(0, 5);
            server.DrawTile(server.alive[0], server.deck);
            server.DrawTile(server.alive[1], server.deck);
            server.DrawTile(server.alive[2], server.deck);
            server.DrawTile(server.alive[3], server.deck);
            server.DrawTile(server.alive[0], server.deck);

            server.DrawTile(server.alive[1], server.deck);
        }
    }
}
