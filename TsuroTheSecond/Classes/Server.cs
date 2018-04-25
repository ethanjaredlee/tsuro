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
            dragonQueue = new List<Player>();
            deck = new List<Tile>{
                new Tile(1, new List<int>{0, 1, 2, 3, 4, 5, 6, 7}),
                new Tile(2, new List<int>{0, 1, 2, 4, 3, 6, 5, 7}),
                new Tile(3, new List<int>{0, 6, 1, 5, 2, 4, 3, 7}),
                new Tile(4, new List<int>{0, 5, 1, 4, 2, 7, 3, 6}),
                new Tile(5, new List<int>{0, 2, 1, 4, 3, 7, 5, 6}),
                new Tile(6, new List<int>{0, 4, 1, 7, 2, 3, 5, 6}),
                new Tile(7, new List<int>{0, 1, 2, 6, 3, 7, 4, 5}),
                new Tile(8, new List<int>{0, 2, 1, 6, 3, 7, 4, 5}),
                new Tile(9, new List<int>{0, 4, 1, 5, 2, 6, 3, 7}),
                new Tile(10, new List<int>{0, 1, 2, 7, 3, 4, 5, 6}),
                new Tile(11, new List<int>{0, 2, 1, 7, 3, 4, 5, 6}),
                new Tile(12, new List<int>{0, 3, 1, 5, 2, 7, 4, 6}),
                new Tile(13, new List<int>{0, 4, 1, 3, 2, 7, 5, 6}),
                new Tile(14, new List<int>{0, 3, 1, 7, 2, 6, 4, 5}),
                new Tile(15, new List<int>{0, 1, 2, 5, 3, 6, 4, 7}),
                new Tile(16, new List<int>{0, 3, 1, 6, 2, 5, 4, 7}),
                new Tile(17, new List<int>{0, 1, 2, 7, 3, 5, 4, 6}),
                new Tile(18, new List<int>{0, 7, 1, 6, 2, 3, 4, 5}),
                new Tile(19, new List<int>{0, 7, 1, 2, 3, 4, 5, 6}),
                new Tile(20, new List<int>{0, 2, 1, 4, 3, 6, 5, 7}),
                new Tile(21, new List<int>{0, 7, 1, 3, 2, 5, 4, 6}),
                new Tile(22, new List<int>{0, 7, 1, 5, 2, 6, 3, 4}),
                new Tile(23, new List<int>{0, 4, 1, 5, 2, 7, 3, 6}),
                new Tile(24, new List<int>{0, 1, 2, 4, 3, 5, 6, 7}),
                new Tile(25, new List<int>{0, 2, 1, 7, 3, 5, 4, 6}),
                new Tile(26, new List<int>{0, 7, 1, 5, 2, 3, 4, 6}),
                new Tile(27, new List<int>{0, 4, 1, 3, 2, 6, 5, 7}),
                new Tile(28, new List<int>{0, 6, 1, 3, 2, 5, 4, 7}),
                new Tile(29, new List<int>{0, 1, 2, 7, 3, 6, 4, 5}),
                new Tile(30, new List<int>{0, 3, 1, 2, 4, 6, 5, 7}),
                new Tile(31, new List<int>{0, 3, 1, 5, 2, 6, 4, 7}),
                new Tile(32, new List<int>{0, 7, 1, 6, 2, 5, 3, 4}),
                new Tile(33, new List<int>{0, 2, 1, 3, 4, 6, 5, 7}),
                new Tile(34, new List<int>{0, 5, 1, 6, 2, 7, 3, 4}),
                new Tile(35, new List<int>{0, 5, 1, 3, 2, 6, 4, 7})
            };
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

        public Boolean LegalPlay(Player player, Board b, Tile tile) {
            // keep this in for iterating through the loop
            if (tile == null) {
                return false;
            }

            if (ValidTilePlacement(b, player, tile) && player.TileinHand(tile)) {
                return true;    
            } else {
                // check hand lengh
                // if 1, return true;
                // if 2, try the other one and if legal for that tile, return false else return true;
                // if 3, try the other two and if both of them are invalid return true;
                switch (player.Hand.Count) {
                    case 1:
                        return true;
                    case 2:
                        foreach(Tile other_tile in player.Hand) {
                            if ( other_tile.id != tile.id ) {
                                return !(ValidTilePlacement(b, player, tile) && player.TileinHand(tile));
                            }
                        }
                        break;
                    case 3:
                        List<bool> other_tiles = new List<bool>();
                        foreach (Tile other_tile in player.Hand)
                        {
                            if (other_tile.id != tile.id)
                            {
                                other_tiles.Add(!(ValidTilePlacement(b, player, tile) && player.TileinHand(tile)));
                            }
                        }

                        return (other_tiles[0] && other_tiles[1]);
                    default:
                        break;
                }
            }
            return false;
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

        void PlayATurn(List<Tile> _deck, List<Player> _alive, List<Player> _dead, Board _board, Tile tile) {
            
            Player currentPlayer = _alive[0];
            currentPlayer.RemoveTilefromHand(tile);
            _board.PlaceTile(tile, currentPlayer.nextTilePosition[0], currentPlayer.nextTilePosition[1]);
            List<Player> fatalities = new List<Player>();
            foreach (Player p in _alive) {
                p.UpdatePosition(board);
                if (p.IsDead()) {
                    fatalities.Add(p);
                }
            }

            if (_alive.Count == 1) {
                WinGame(_alive);
            }

            if (_alive.Count == 0) {
                WinGame(fatalities);
            }

            foreach (Player p in fatalities) {
                KillPlayer(p);
            }

            DrawTile(currentPlayer, _deck);
        }

        public void KillPlayer(Player player) {
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
            Console.WriteLine(d.Count);
            if (player.Hand.Count >= 3) {
                throw new InvalidOperationException("Player can't have more than 3 cards in hand");
            }

            if (d.Count <= 0) {
                dragonQueue.Add(player);   
            } else {
                Tile t = d[0];
                d.RemoveAt(0);
                player.AddTiletoHand(t); 
            }

        }

        static void Main(string[] args)
        {
        }
    }
}
