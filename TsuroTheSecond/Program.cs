using System;

namespace TsuroTheSecond
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(6);
            Console.WriteLine(board.tiles[0].Count);
        }
    }
}
