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
            //if (tile == null) {
            //    return false;
            //}

            //if (!player.TileInHand(tile)) {
            //    return false;
            //}

            //// copy board
            //Board copyBoard = CopyBoard(b);
            //copyBoard.PlaceTile(tile, player);
            //player.UpdatePosition(copyBoard);

            //if (player.IsDead()) {
            //    return false;
            //}

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
        }

        //void DrawTile(Player player, List<Tile> deck) {
        //    // how is this supposed to work with an interface?
        //    //if (player.Hand.Count) > 3) {
        //    //    throw new Exception("Player can't have more than 3 cards in hand");
        //    //}

        //    Tile t = deck[0];
        //    deck.RemoveAt(0);
        //    // implement this
        //    player.addTileToHand(t); 
        //}

        static void Main(string[] args)
        {
            Board board = new Board(6);
            Console.WriteLine(board.tiles[0].Count);
        }
    }
}
