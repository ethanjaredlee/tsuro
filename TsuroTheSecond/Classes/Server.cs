using System;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class Server 
    {

        public List<SPlayer> alive;
        public List<SPlayer> dead;
        public List<Tile> deck;
        public Board board;
        public List<String> colors = new List<string> { "blue", "green", "black", "yellow", "red", "orange", "white", "pink" };
               
        public Server(int playerCount) {
            // initializes the game
            alive = new List<SPlayer>();
            dead = new List<SPlayer>();
            board = new Board(Constants.boardSize);

            for (int i = 0; i < playerCount; i++) {
                alive.Add(new SPlayer(colors[i]));
            }

        }

        void DrawTile(SPlayer player) {
            // how is this supposed to work with an interface?
            //if (player.Hand.Count) > 3) {
            //    throw new Exception("Player can't have more than 3 cards in hand");
            //}

            Tile t = deck[0];
            deck.RemoveAt(0);
            // implement this
            player.addTile(t); 
        }

        static void Main(string[] args)
        {
            Board board = new Board(6);
            Console.WriteLine(board.tiles[0].Count);
        }
    }
}
