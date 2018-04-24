using System;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class Server 
    {

        public List<String> colors = new List<string> { "blue", "green", "black", "yellow", "red", "orange", "white", "pink" };
               
        public Server(int playerCount) {
            // initializes the game
            List<Tile> deck = new List<Tile>();
            List <SPlayer> alive = new List<SPlayer>();
            List <SPlayer> dead = new List<SPlayer>();
            Board board = new Board(Constants.boardSize);

            for (int i = 0; i < playerCount; i++) {
                alive.Add(new SPlayer(colors[i]));
            }

        }

        Boolean LegalPlay(SPlayer player, Board b, Tile tile) {
            if (tile == null) {
                return false;
            }
            return true;
        }

        void PlayATurn(List<Tile> deck, List<SPlayer> alive, List<SPlayer> dead, Board board, Tile tile) {
            while (alive.Count > 0) {
                Tile chosenTile = null;

                while (!LegalPlay(chosenTile)) {
                    SPlayer currentPlayer = alive[0];
                    chosenTile = currentPlayer.player.chooseTile(board);
                }
            }
        }

        void DrawTile(SPlayer player, List<Tile> deck) {
            // how is this supposed to work with an interface?
            //if (player.Hand.Count) > 3) {
            //    throw new Exception("Player can't have more than 3 cards in hand");
            //}

            Tile t = deck[0];
            deck.RemoveAt(0);
            // implement this
            player.addTileToHand(t); 
        }

        static void Main(string[] args)
        {
            Board board = new Board(6);
            Console.WriteLine(board.tiles[0].Count);
        }
    }
}
