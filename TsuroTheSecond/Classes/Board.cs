using System;
using System.Collections.Generic;
namespace TsuroTheSecond
{
    public class Board : IBoard
    {
        public readonly List<List<Tile>> tiles;

        public Board(int size)
        {
            tiles = new List<List<Tile>>();
            for (int i = 0; i < size; i++) {
                tiles.Add(new List<Tile>(size));
            }
        }

        public Boolean FreeTileSpace(int x, int y) {
            return tiles[y][x] == null;
        }

        public void PlaceTile(Tile tile, int x, int y) {
            tiles[y][x] = tile;
        }


    }
}
