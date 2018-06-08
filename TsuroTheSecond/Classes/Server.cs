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
        public enum State { start, ready, handEmpty, loop, safe, end };
        public State gameState;
        public bool verbose;

        public Server() {
            gameState = State.start;
            // initializes the game
            dragonQueue = new List<Player>();
            deck = ShuffleDeck(Constants.tiles);
            alive = new List<Player>();
            dead = new List<Player>();
            board = new Board(Constants.boardSize);
            verbose = false;
        }

        public Server(List<Tile> _deck, List<Player> _alive, List<Player> _dead, Board _board, List<Player> _dragonQueue, State _gameState) {
            deck = _deck;
            alive = _alive;
            dead = _dead;
            board = _board;
            dragonQueue = _dragonQueue;
            gameState = _gameState;
            verbose = false;
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
            if (this.verbose) Console.WriteLine("Added player " + p.GetName() + ", color: " + color);
        }

        public void ReplacePlayer(Player player) {
            // might want a better way to do this
            List<MPlayer> mplayers = new List<MPlayer>{
                new RandomPlayer("Replacement Player"),
                new LeastSymmetricPlayer("Replacement Player"),
                new MostSymmetricPlayer("Replacement Player"),
            };

            Random random = new Random();
            MPlayer replacement = mplayers[random.Next(0, 3)];

            while (replacement.GetType() == player.iplayer.GetType()) {
                replacement = mplayers[random.Next(0, 3)];
            }

            List<string> colorCopy = new List<string>();
            foreach (string color in Constants.colors) {
                if (color != player.Color) {
                    colorCopy.Add(color);
                }
            }

            replacement.Initialize(player.Color, colorCopy);
            player.ReplaceMPlayer(replacement);
        }

        public void InitPlayerPositions() {
            if (this.verbose) Console.WriteLine("Initializing " + alive.Count + " player positions");

            Position position;
            if (gameState != State.start)
            {
                throw new Exception("Invalid game state");
            }

            if (alive.Count < 2) {
                throw new Exception("Not enough players in game");
            }

            foreach(Player p in alive) {
                try {
                    p.iplayer.Initialize(p.Color, alive.Select(x => x.Color).ToList());
                } catch (Exception) {
                    Console.WriteLine("Player initialized failed and has been replaced");
                    ReplacePlayer(p);
                }
            }
            gameState = State.handEmpty;

            foreach(Player p in alive) {
                position = new Position(6, 1, 7);
                try
                {
                    position = p.iplayer.PlacePawn(this.board);
                }
                catch (Exception)
                {
                    Console.WriteLine("Player placed pawn in an invalid position and has been replaced");
                    ReplacePlayer(p);
                    position = p.iplayer.PlacePawn(this.board);
                }
                if (this.verbose) Console.WriteLine("Added player to board " + p.Color);
                this.board.AddPlayerToken(p.Color, position);
            }
        }

        public void InitPlayerHands() {
            if (gameState != State.handEmpty)
            {
                throw new Exception("State should be handempty");
            }

            foreach (Player p in alive) {
                DrawTile(p);
                DrawTile(p);
                DrawTile(p);
            }

            gameState = State.loop;
        }

        public List<Tile> ShuffleDeck(List<Tile> deck)
        {
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
            if (this.verbose) Console.WriteLine("the deck has been shuffled");
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
                //ReplacePlayer(player);
                //Console.WriteLine("Player yielded an illegal tile and has been replaced");
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

            //ReplacePlayer(player);
            //Console.WriteLine("Player has played an illegal tile and has been replaced");
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

            Player currentPlayer = alive[0];
            if (this.verbose) Console.WriteLine(currentPlayer.iplayer.GetName() + " color: " + currentPlayer.Color + " "  + currentPlayer.Hand.Count + " tiles in hand and is in position " + board.tokenPositions[currentPlayer.Color]);

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
            foreach(List<Tile> row in board.tiles) {
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


            var next = board.ReturnNextSpot(currentPlayer.Color);
            board.PlaceTile(tile, next.Item1, next.Item2);
            if (this.verbose) Console.WriteLine(currentPlayer.iplayer.GetName() + " has placed tile " + tile + " at position (" + next.Item1 + "," + next.Item2 + ")");

            List<Player> fatalities = new List<Player>();
            foreach (Player p in alive) {
                Position orig_position = board.tokenPositions[p.Color];
                board.MovePlayer(p.Color);

                if (this.verbose) {
                    if (orig_position != board.tokenPositions[p.Color]) {
                        Console.WriteLine("Moving player from " + orig_position + " to " + board.tokenPositions[p.Color]);  
                    }
                }

                if (board.IsDead(p.Color)) {
                    if (this.verbose) Console.WriteLine("Player " + p.iplayer.GetName() + " died at position " + board.tokenPositions[p.Color]);
                    fatalities.Add(p);
                }
            }

            // can decide to return here if thats better
            // everything under here isn't necessary if game is over
            foreach (Player p in fatalities) {
                KillPlayer(p);
            }

            if (alive.Count == 1)
            {
                return (deck, alive, dead, board, true, alive);
            }
            if (alive.Count == 0)
            {
                return (deck, alive, dead, board, true, fatalities);
            }

            if (!board.IsDead(currentPlayer.Color)) {
                DrawTile(currentPlayer);
            }

            // put currentPlayer to end of _alive
            // check if this is necessary?
            for (int i = 0; i < alive.Count; i++){
                if(alive[i].Color == currentPlayer.Color){
                    Player move_to_end = alive[i];
                    alive.Remove(move_to_end);
                    alive.Add(move_to_end);
                }
            }

            if (board.TilesOnBoard() >= 35)
            {
                return (deck, alive, dead, board, true, alive);
            }

            return (deck, alive, dead, board, false, null);
        }

        public void KillPlayer(Player player) {
            if (this.verbose) Console.WriteLine("Killing player " + player.iplayer.GetName());

            dead.Add(player);
            alive.Remove(player);

            while (dragonQueue.Contains(player)) {
                dragonQueue.Remove(player);
                if (this.verbose) Console.WriteLine(player.iplayer.GetName() + " was removed from dragonQueue, size: " + dragonQueue.Count);
            }

            // distribute player hand to whoevers waiting in queue or just add to deck
            int playerHandCount = player.Hand.Count;
            if (playerHandCount > 0) {
                deck.AddRange(player.Hand);

                for (int i = 0; i < playerHandCount; i++) {
                    player.Hand.RemoveAt(0);
                }

                if (this.verbose) Console.WriteLine("Adding " + player.Hand.Count + " cards to deck");
                int dragonCount = dragonQueue.Count;
                for (int i = 0; (i < dragonCount && deck.Count > 0); i++) {
                    if (this.verbose) Console.WriteLine("dragon tile holder " + player.iplayer.GetName() + " is now drawing");
                    DrawTile(dragonQueue[0]);
                    dragonQueue.Remove(dragonQueue[0]);
                }
            }

        }

        public void WinGame(List<Player> winners) {
            List<string> winColors = winners.Select(w => w.Color).ToList();
            foreach (Player p in winners) {
                if (this.verbose) Console.WriteLine("Player: " + p.iplayer.GetName() + " won!");
            }
            foreach (Player p in alive) {
                try {
                    p.iplayer.EndGame(board, winColors);
                } catch(Exception) {
                    Console.WriteLine("Invalid Engame Response, player has been replaced");
                    ReplacePlayer(p);
                    p.iplayer.EndGame(board, winColors);
                }
            }

            foreach (Player p in dead) {
                try {
                    p.iplayer.EndGame(board, winColors);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Engame Response, player has been replaced");
                    ReplacePlayer(p);
                    p.iplayer.EndGame(board, winColors);
                }
            }
        }

        public void DrawTile(Player player) {

            if (!alive.Contains(player) || dead.Contains(player)) {
                throw new ArgumentException("Dead players can't draw tiles");
            }

            if (player.Hand.Count >= 3) {
                throw new InvalidOperationException("Player can't have more than 3 cards in hand");
            }

            if (deck.Count <= 0) {
                dragonQueue.Add(player);   
                if (this.verbose) Console.WriteLine("Deck size is " + deck.Count + " adding " + player.iplayer.GetName() + " to dragonQueue, size: " + dragonQueue.Count);
            } else {
                Tile t = deck[0];
                deck.RemoveAt(0);
                if (this.verbose) Console.WriteLine(player.iplayer.GetName() + " is drawing tile " + t);
                player.AddTiletoHand(t); 
            }

        }

        public List<string> Play(List<IPlayer> players) {
            // returns winner type
            int counter = 0;
            // input: players is color, IPlayer
            for (int i = 0; i < players.Count; i++) {
                AddPlayer(players[i], Constants.colors[i]);
            }

            InitPlayerPositions();
            InitPlayerHands();

            Boolean gameLoop = true;
            List<Player> winners = new List<Player>();
            while (gameLoop && alive.Count > 0) {
                counter++;
                if (this.verbose) Console.WriteLine("\nTurn: " + counter + ", deck size: " + deck.Count + " players left in game: " + alive.Count);
                Player currentPlayer = alive[0];

                Tile playTile = null;
                try {
                    playTile = currentPlayer.iplayer.PlayTurn(board, currentPlayer.Hand, deck.Count);
                } catch(Exception) {
                    Console.WriteLine("PlayTurn didn't return correctly, player is being replaced");
                    ReplacePlayer(currentPlayer);
                    playTile = currentPlayer.iplayer.PlayTurn(board, currentPlayer.Hand, deck.Count);
                }

                if (!LegalPlay(currentPlayer, board, playTile)) {
                    ReplacePlayer(currentPlayer);
                    Console.WriteLine("Player made an illegal move and is being replaced");
                    // assuming this makes a legal move...
                    playTile = currentPlayer.iplayer.PlayTurn(board, currentPlayer.Hand, deck.Count);
                    if (!LegalPlay(currentPlayer, board, playTile)) {
                        // just kidding, never assume, but this shouldn't be thrown
                        throw new Exception("the replaced player played an illegal tile");
                    }
                }

                currentPlayer.RemoveTilefromHand(playTile);

                // playturn
                var playResult = PlayATurn(deck, alive, dead, board, playTile);
                if (this.verbose) Console.WriteLine("deck size is: " + playResult.Item1.Count);
                if (this.verbose) Console.WriteLine("gameLoop is: " + playResult.Item5);
                if (playResult.Item5) {
                    gameLoop = false;
                    winners = playResult.Item6;
                }
            }

            WinGame(winners);
            List<string> winTypes = winners.Select(w => w.iplayer.GetType().ToString()).ToList();
            return winTypes;
        }
    }
}
