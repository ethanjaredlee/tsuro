using System;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class Server 
    {

        public List<Tile> deck;
        public List<Player> alive;
        public List<Player> dead;
        public Board board;

        public Server() {
            // initializes the game
            deck = ShuffleDeck(Constants.tiles);
            alive = new List<Player>();
            dead = new List<Player>();
            board = new Board(Constants.boardSize);
        }

        //public void InitializeGame(int playerCount) {
            
        //}

        public void AddPlayer() {
            alive.Add(new Player());
        }

        public List<Tile> ShuffleDeck(List<Tile> deck)
        {
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
            if (tile == null) {
                return false;
            }

            if (!player.TileInHand(tile)) {
                return false;
            }

            // copy board
            Board copyBoard = CopyBoard(b);
            copyBoard.PlaceTile(tile, player.nextTilePosition[0], player.nextTilePosition[1]);
            player.UpdatePosition(copyBoard);

            if (player.IsDead()) {
                return false;
            }

            return true;
        }

        Board CopyBoard(Board b) {
            Board copy = new Board(board.tiles.Count);

            for (int i = 0; i < board.tiles.Count; i++){
                for (int j = 0; j < board.tiles.Count; j++) {
                    copy.PlaceTile(board.tiles[i][j], i, j);
                }
            }

            return copy;
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
            currentPlayer.RemoveTileFromHand(tile);
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
                dead.Add(p);
                alive.Remove(p);
            }

            DrawTile(currentPlayer, deck);
        }

        public void WinGame(List<Player> winners) {
        }

        void DrawTile(Player player, List<Tile> d) {
            // how is this supposed to work with an interface?
            //if (player.Hand.Count) > 3) {
            //    throw new Exception("Player can't have more than 3 cards in hand");
            //}

            Tile t = d[0];
            d.RemoveAt(0);
            // implement this
            player.AddTileToHand(t); 
        }

        static void Main(string[] args)
        {
        }
    }
}
