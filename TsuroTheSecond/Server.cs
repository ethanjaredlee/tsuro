using System;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    class Server 
    {
        


        List<SPlayer> alive;
        List<SPlayer> dead;
        List<Tile> deck;
        Board board;
        List<String> colors = new List<string> { "blue", "green", "black", "yellow", "red", "orange", "white", "pink" };
               
        public Server(int playerCount) {
            // initializes the game
            alive = new List<SPlayer>();
            dead = new List<SPlayer>();
            board = new Board(6);

            for (int i = 0; i < playerCount; i++) {
                alive.Add(new SPlayer(colors[i]));
            }

        }

        static void Main(string[] args)
        {
            Board board = new Board(6);
            Console.WriteLine(board.tiles[0].Count);
        }
    }
}
