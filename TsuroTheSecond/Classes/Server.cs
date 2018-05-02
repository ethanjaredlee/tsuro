using System;
using System.Linq;
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
            deck = new List<Tile>(Constants.tiles);
            alive = new List<Player>();
            dead = new List<Player>();
            board = new Board(Constants.boardSize);
        }

        public void AddPlayer(IPlayer p, int age) {
            // somehow check that at least 2 players are in teh game?

            if (alive.Count >= 8) {
                throw new InvalidOperationException("Only 8 players allowed in game");
            }
            // todo organize alive by age and don't let players pick duplicate colors
            alive.Add(new Player(p, age));
            alive = alive.OrderBy(x => x.age).ToList();
            alive.Reverse();
        }

        public List<Tile> ShuffleDeck(List<Tile> deck)
        {
            // doesnt quite work the way we want it to yet
            List<Tile> shuffledDeck = new List<Tile>(new Tile[deck.Count]);
            Random rng = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int rot = rng.Next(0, 3);
                Tile tile = deck[k];
                deck[k] = deck[n];
                for (int i = 0; i < rot; i++) {
                    tile.Rotate();
                }
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
                        foreach(Tile other_tile in player.Hand) 
                        {
                            if ( other_tile.id != tile.id ) 
                            {
                                return !(ValidTilePlacement(b, player, other_tile) && player.TileinHand(other_tile));
                            }
                        }
                        break;
                    case 3:
                        List<bool> other_tiles = new List<bool>();
                        foreach (Tile other_tile in player.Hand)
                        {
                            if (other_tile.id != tile.id)
                            {
                                other_tiles.Add(!(ValidTilePlacement(b, player, other_tile) && player.TileinHand(other_tile)));
                            }
                        }

                        return (other_tiles[0] && other_tiles[1]);
                    default:
                        break;
                }
            }
            return false;
        }

        public Boolean ValidTilePlacement(Board b, Player player, Tile tile) {
            // checks if placing a tile on the board will kill the player 
            Boolean playerAlive = true;
            var origNext = player.position.WhatNext();
            b.PlaceTile(tile, origNext.Item1, origNext.Item2);
            Position origPosition = player.position;
            player.UpdatePosition(b);

            playerAlive = !player.IsDead();

            // undoing changes to the board
            b.PlaceTile(null, origNext.Item1, origNext.Item2);
            player.position = origPosition;
            return playerAlive;
        }

        public (List<Tile>, List<Player>, List<Player>, Board, Boolean) PlayATurn(List<Tile> _deck, 
                                                                                  List<Player> _alive, 
                                                                                  List<Player> _dead, 
                                                                                  Board _board, 
                                                                                  Tile tile) 
        {
            Player currentPlayer = _alive[0];
            currentPlayer.RemoveTilefromHand(tile);
            var next = currentPlayer.position.WhatNext();
            _board.PlaceTile(tile, next.Item1, next.Item2);
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

            // fix this shouldnt return false
            return (_deck, _alive, _dead, _board, false);
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
            // make server
            Server server = new Server();

            // add players
            MPlayer1 player1 = new MPlayer1("joe");
            MPlayer1 player2 = new MPlayer1("bob");
            server.AddPlayer(player1, 20);
            server.AddPlayer(player2, 12);

            // init positions of players

            // game loop
                // pop from alive
                // player plays turn
                // checks if its legal
                // hopefully doesnt loop back and play differnt tile
                // place tile
                // move players
                // check alive/dead and update
                // add player to end of alive if alivew
        }
    }
}
