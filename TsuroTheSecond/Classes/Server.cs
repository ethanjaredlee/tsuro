using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class Server 
    {
        // TODO: refactor so that everything mutates the fields and doesn't really use inputs

        public List<Tile> deck;
        public List<Player> alive;
        public List<Player> dead;
        public Board board;
        public List<Player> dragonQueue; // whoever is the first person of the queue has the tile
        public enum State { start, ready, loop, safe, end };
        public State gameState;

        public Server() {
            gameState = State.start;
            // initializes the game
            dragonQueue = new List<Player>();
            deck = ShuffleDeck(Constants.tiles);
            alive = new List<Player>();
            dead = new List<Player>();
            board = new Board(Constants.boardSize);
        }

        public void AddPlayer(IPlayer p, string color) {
            if (gameState != State.start) {
                throw new Exception("Invalid game state");
            }

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

        public void ReplacePlayer(Player player) {
            // might want a better way to do this
            List<IPlayer> iplayers = new List<IPlayer>{
                new RandomPlayer(player.iplayer.GetName()),
                new LeastSymmetricPlayer(player.iplayer.GetName()),
                new MostSymmetricPlayer(player.iplayer.GetName()),
            };

            Random random = new Random();
            IPlayer replacement = iplayers[random.Next(0, 3)];

            while (replacement.GetType() == player.iplayer.GetType()) {
                replacement = iplayers[random.Next(0, 3)];
            }

            List<string> colorCopy = new List<string>();
            foreach (string color in Constants.colors) {
                if (color != player.Color) {
                    colorCopy.Add(color);
                }
            }

            replacement.Initialize(player.Color, colorCopy);
            player.ReplaceIPlayer(replacement);
        }

        public void InitPlayerPositions() {
            Position position;
            if (gameState != State.start)
            {
                throw new Exception("Invalid game state");
            }

            if (alive.Count < 2) {
                throw new Exception("Not enough players in game");
            }

            foreach(Player p in alive) {
                p.iplayer.Initialize(p.Color, alive.Select(x => x.Color).ToList());
            }
            gameState = State.loop;

            foreach(Player p in alive) {
                position = new Position(6, 1, 7);
                try
                {
                    position = p.iplayer.PlacePawn(this.board);
                    break;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Player initialized invalid position and has been replaced");
                    ReplacePlayer(p);
                }
                Console.WriteLine("added player! " + p.Color);
                this.board.AddPlayerToken(p.Color, position);
            }
            // board.AddPlayerToken for all players
            //for (int i = 0; i < alive.Count; i++) {
            //    // this seems hacky and unsafe
            //    while (true) {
            //        try
            //        {
            //            position = alive[i].iplayer.PlacePawn(this.board);
            //            break;
            //        }
            //        catch (ArgumentException)
            //        {
            //            Console.WriteLine("Player initialized invalid position and has been replaced");
            //            ReplacePlayer(alive[i]);
            //        }
            //    }
            //    Console.WriteLine("added player! " + alive[i].Color);
            //    this.board.AddPlayerToken(alive[i].Color, position);
            //}
        }

        public List<Tile> ShuffleDeck(List<Tile> deck)
        {
            // doesnt quite work the way we want it to yet
            List<Tile> shuffledDeck = new List<Tile>();
            Random rng = new Random();
            HashSet<int> chosen = new HashSet<int>();

            while (shuffledDeck.Count < deck.Count)
            {
                int k = rng.Next(deck.Count);
                if (chosen.Contains(k)) { continue; }

                Tile tile = deck[k];
                int r = rng.Next(4);

                for (int i = 0; i < r; i++) {
                    tile.Rotate();
                }

                shuffledDeck.Add(tile);
                chosen.Add(k);
            }
            return shuffledDeck;
        }

        public Boolean LegalPlay(Player player, Board b, Tile tile) {
            if (gameState != State.loop)
            {
                throw new Exception("Invalid game state");
            }
            gameState = State.safe;

            // Check for valid tile
            if (tile == null || !player.TileinHand(tile)) {
                ReplacePlayer(player);
                Console.WriteLine("Player yielded an illegal tile and has been replaced");
                return false;
            }

            List<Tile> all_options = b.AllPossibleTiles(player.Color, player.Hand);

            // if there's no options that don't kill you, then any tile is legal
            if (all_options.Count == 0) {
                return true;
            }

            // try if the tile is in all_options
            // If so, return true
            foreach(Tile goodTile in all_options){
                if(goodTile.CompareByPath(tile)){
                    return true;
                }
            }

            ReplacePlayer(player);
            Console.WriteLine("Player has played an illegal tile and has been replaced");
            return false;
        }

        public (List<Tile>, List<Player>, List<Player>, Board, Boolean, List<Player>) PlayATurn(List<Tile> _deck, 
                                                                                  List<Player> _alive, 
                                                                                  List<Player> _dead, 
                                                                                  Board _board, 
                                                                                  Tile tile) 
        {
            if (gameState != State.safe) {
                throw new Exception("Invalid game state");
            }
            gameState = State.loop;

            Player currentPlayer = _alive[0];

            if (currentPlayer.Hand.Count > 2) {
                throw new ArgumentException("Player should have 2 or less tiles in hand");
            }

            // check to make sure player tiles in hand and tile to be played are unique
            HashSet<string> tilePaths = new HashSet<string>();
            tilePaths.Add(tile.PathMap());
            foreach(Tile t in currentPlayer.Hand) {
                tilePaths.Add(t.PathMap());
            }

            if (tilePaths.Count != currentPlayer.Hand.Count + 1) {
                throw new ArgumentException("Tile to be played and tiles in hand are not unique");
            }

            int tileCount = 0;
            foreach(List<Tile> row in _board.tiles) {
                foreach(Tile t in row) {
                    if (t == null) { continue; }
                    tilePaths.Add(t.PathMap());
                    tileCount++;
                } 
            }

            int total = currentPlayer.Hand.Count + 1 + tileCount;
            if (tilePaths.Count != total) {
                throw new ArgumentException("Tile to be placed, tiles in hand, and tiles on board are not unique");
            }


            var next = _board.ReturnNextSpot(currentPlayer.Color);
            _board.PlaceTile(tile, next.Item1, next.Item2);

            List<Player> fatalities = new List<Player>();
            foreach (Player p in _alive) {
                _board.MovePlayer(p.Color);
                if (_board.IsDead(p.Color)) {
                    fatalities.Add(p);
                }
            }

            Boolean gameOver = _alive.Count == 1 || _alive.Count == 0;
            // can decide to return here if thats better
            // everything under here isn't necessary if game is over
            if (_alive.Count == 1) {
                return (_deck, _alive, _dead, _board, true, _alive);
            } else if (_alive.Count == 0) {
                return (_deck, _alive, _dead, _board, true, fatalities);
            }

            foreach (Player p in fatalities) {
                KillPlayer(p);
            }

            if (!board.IsDead(currentPlayer.Color)) {
                DrawTile(currentPlayer);
            }

            // put currentPlayer to end of _alive
            // check if this is necessary?
            for (int i = 0; i < _alive.Count; i++){
                if(_alive[i].Color == currentPlayer.Color){
                    Player move_to_end = _alive[i];
                    _alive.Remove(move_to_end);
                    _alive.Add(move_to_end);
                }
            }

            return (_deck, _alive, _dead, _board, false, null);
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
                    DrawTile(dragonQueue[i]);
                    dragonQueue.Remove(dragonQueue[i]);
                }
            }
        }

        public void WinGame(List<Player> winners) {
            List<string> winColors = winners.Select(w => w.Color).ToList();
            foreach (Player p in alive) {
                p.iplayer.EndGame(board, winColors);
            }

            foreach (Player p in dead) {
                p.iplayer.EndGame(board, winColors);
            }
        }

        public void DrawTile(Player player) {

            if (player.Hand.Count >= 3) {
                throw new InvalidOperationException("Player can't have more than 3 cards in hand");
            }

            if (deck.Count <= 0) {
                dragonQueue.Add(player);   
            } else {
                Tile t = deck[0];
                deck.RemoveAt(0);
                player.AddTiletoHand(t); 
            }

        }

        public void Play(Dictionary<string, IPlayer> players) {
            // input: players is color, IPlayer
            foreach (KeyValuePair<string, IPlayer> p in players) {
                AddPlayer(p.Value, p.Key);
            }

            InitPlayerPositions();
            ShuffleDeck(this.deck);

            Boolean game = true;
            while (game && alive.Count > 0) {
                Player currentPlayer = alive[0];
                Tile playTile = currentPlayer.iplayer.PlayTurn(board, currentPlayer.Hand, deck.Count);
                if (!LegalPlay(currentPlayer, board, playTile)) {
                    ReplacePlayer(currentPlayer); 
                }

                // playturn
                PlayATurn(deck, alive, dead, board, playTile);
            }
        }

        static void Main(string[] args)
        {
            // make server
            Server server = new Server();

            // add players
            MPlayer1 mplayer1 = new MPlayer1("Adam");
            MPlayer2 mplayer2 = new MPlayer2("John");
            MPlayer3 mplayer3 = new MPlayer3("Cathy");

            server.AddPlayer(mplayer1, "blue");
            server.AddPlayer(mplayer1, "green");
            server.AddPlayer(mplayer1, "hotpink");

            // init positions of players
            server.InitPlayerPositions();

            server.ShuffleDeck(server.deck);

            // game loop
            bool game = true;
            while (game && server.alive.Count > 0) {
                Player currentPlayer = server.alive[0];
                Tile playTile = currentPlayer.iplayer.PlayTurn(server.board, currentPlayer.Hand, server.deck.Count);
            }
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
