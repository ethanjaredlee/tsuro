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

        public void AddPlayer(IPlayer p, string color) {
            if (!Constants.colors.Contains(color)) {
                throw new ArgumentException("Invalid color");
            }

            List<string> colors = alive.Select(x => x.Color).ToList();
            if (colors.Contains(color)) {
                throw new ArgumentException("Duplicate color");
            }

            // somehow check that at least 2 players are in teh game?

            if (alive.Count >= 8) {
                throw new InvalidOperationException("Only 8 players allowed in game");
            }
            // todo organize alive by age and don't let players pick duplicate colors
            alive.Add(new Player(p, color));
        }

        public void InitPlayerPosition() {
            for (int i = 0; i < alive.Count; i++) {
                this.board.AddPlayerToken(alive[i].Color, alive[i].iplayer.PlacePawn(this.board));
            }
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
            // Check for valid tile
            if (tile == null || !player.TileinHand(tile)) {
                throw new Exception("Invalid Tile was passed into LegalPlay");
            }
            List<Tile> all_options = b.AllPossibleTiles(player.Color, player.Hand);
            // try if the tile is in all_options
            // If so, return true
            foreach(Tile goodTile in all_options){
                if(goodTile.CompareByPath(tile)){
                    return true;
                }
            }
            // try if other tiles are in the options
            // If so, return false
            foreach(Tile hand_tile in player.Hand){
                if( hand_tile.id != tile.id ){
                    foreach (Tile goodTile in all_options)
                    {
                        if (goodTile.CompareByPath(hand_tile))
                        {
                            return false;
                        }
                    }
                }
            }
            // If all rotated tiles fail,
            // return true
            return true;
        }

        public (List<Tile>, List<Player>, List<Player>, Board, Boolean) PlayATurn(List<Tile> _deck, 
                                                                                  List<Player> _alive, 
                                                                                  List<Player> _dead, 
                                                                                  Board _board, 
                                                                                  Tile tile) 
        {
            Player currentPlayer = _alive[0];
            currentPlayer.RemoveTilefromHand(tile);
            var next = _board.ReturnNextSpot(currentPlayer.Color);
            _board.PlaceTile(tile, next.Item1, next.Item2);

            List<Player> fatalities = new List<Player>();
            foreach (Player p in _alive) {
                _board.MovePlayer(p.Color);
                if (_board.IsDead(p.Color)) {
                    fatalities.Add(p);
                }
            }

            if (_alive.Count == 1) {
                WinGame(_alive);
            } else if (_alive.Count == 0) {
                WinGame(fatalities);
            }

            foreach (Player p in fatalities) {
                KillPlayer(p);
            }

            DrawTile(currentPlayer, _deck);

            // put currentPlayer to end of _alive
            for (int i = 0; i < _alive.Count; i++){
                if(_alive[i].Color == currentPlayer.Color){
                    Player move_to_end = _alive[i];
                    _alive.Remove(move_to_end);
                    _alive.Add(move_to_end);
                }
            }

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
