using System;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class Constants
    {
        public const int boardSize = 6;
        public readonly List<Tile> tiles = new List<Tile>{
            new Tile(1, new List<int>{0, 1, 2, 3, 4, 5, 6, 7}),
            new Tile(2, new List<int>{0, 1, 2, 4, 3, 6, 5, 7}),
            new Tile(3, new List<int>{0, 6, 1, 5, 2, 4, 3, 7}),
            new Tile(4, new List<int>{0, 5, 1, 4, 2, 7, 3, 6}),
            new Tile(5, new List<int>{0, 2, 1, 4, 3, 7, 5, 6}),
            new Tile(6, new List<int>{0, 4, 1, 7, 2, 3, 5, 6}),
            new Tile(7, new List<int>{0, 1, 2, 6, 3, 7, 4, 5}),

            new Tile(8, new List<int>{0, 2, 1, 6, 3, 7, 4, 5}),
            new Tile(9, new List<int>{0, 1, 2, 3, 4, 5, 6, 7}),
            new Tile(10, new List<int>{0, 1, 2, 3, 4, 5, 6, 7}),
            new Tile(11, new List<int>{0, 1, 2, 3, 4, 5, 6, 7}),
            new Tile(12, new List<int>{0, 1, 2, 3, 4, 5, 6, 7}),
            new Tile(13, new List<int>{0, 1, 2, 3, 4, 5, 6, 7}),
            new Tile(14, new List<int>{0, 1, 2, 3, 4, 5, 6, 7}),
            new Tile(15, new List<int>{0, 1, 2, 3, 4, 5, 6, 7}),
            new Tile(16, new List<int>{0, 1, 2, 3, 4, 5, 6, 7}),
            //((0 1) (2 3) (4 5) (6 7))
            //((0 1) (2 4) (3 6) (5 7))
            //((0 6) (1 5) (2 4) (3 7))
            //((0 5) (1 4) (2 7) (3 6))
            //((0 2) (1 4) (3 7) (5 6))
            //((0 4) (1 7) (2 3) (5 6))
            //((0 1) (2 6) (3 7) (4 5))
            new Tile(1, new List<int>{0, 2, 1, 6, 3, 7, 4, 5}),
            new Tile(1, new List<int>{0, 4, 1, 5, 2, 6, 3, 7}),
            new Tile(1, new List<int>{0, 1, 2, 7, 3, 4, 5, 6}),
            new Tile(1, new List<int>{0, 2, 1, 7, 3, 4, 5, 6})
             //new Tile(1, new List<int>{           ((0 3) (1 5) (2 7) (4 6))
             //new Tile(1, new List<int>{           ((0 4) (1 3) (2 7) (5 6))
             //new Tile(1, new List<int>{           ((0 3) (1 7) (2 6) (4 5))
             //new Tile(1, new List<int>{           ((0 1) (2 5) (3 6) (4 7))
             //new Tile(1, new List<int>{           ((0 3) (1 6) (2 5) (4 7))
             //new Tile(1, new List<int>{           ((0 1) (2 7) (3 5) (4 6))
             //new Tile(1, new List<int>{           ((0 7) (1 6) (2 3) (4 5))
             //new Tile(1, new List<int>{           ((0 7) (1 2) (3 4) (5 6))
             //new Tile(1, new List<int>{           ((0 2) (1 4) (3 6) (5 7))
             //new Tile(1, new List<int>{           ((0 7) (1 3) (2 5) (4 6))
             //new Tile(1, new List<int>{           ((0 7) (1 5) (2 6) (3 4))
             //new Tile(1, new List<int>{           ((0 4) (1 5) (2 7) (3 6))
             //new Tile(1, new List<int>{           ((0 1) (2 4) (3 5) (6 7))
             //new Tile(1, new List<int>{           ((0 2) (1 7) (3 5) (4 6))
             //new Tile(1, new List<int>{           ((0 7) (1 5) (2 3) (4 6))
             //new Tile(1, new List<int>{           ((0 4) (1 3) (2 6) (5 7))
             //new Tile(1, new List<int>{           ((0 6) (1 3) (2 5) (4 7))
             //new Tile(1, new List<int>{           ((0 1) (2 7) (3 6) (4 5))
             //new Tile(1, new List<int>{           ((0 3) (1 2) (4 6) (5 7))
             //new Tile(1, new List<int>{           ((0 3) (1 5) (2 6) (4 7))
             //new Tile(1, new List<int>{           ((0 7) (1 6) (2 5) (3 4))
             //new Tile(1, new List<int>{           ((0 2) (1 3) (4 6) (5 7))
             //new Tile(1, new List<int>{           ((0 5) (1 6) (2 7) (3 4))
             //new Tile(1, new Lisst<int>{0, 5, 1, 3, 2, 6, 4, 7},
        };
    }
}
