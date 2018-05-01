using System;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class Board : IBoard
    {
        // "board describes where tiles are, what orientation they're in, 
        // where each pawn is, and what color it is" - hw5
        public readonly List<List<Tile>> tiles;
        public readonly Dictionary<string, List<int>> tokenPositions;

        public Board(int size)
        {
            tiles = new List<List<Tile>>();
            for (int i = 0; i < size; i++)
            {
                tiles.Add(new List<Tile>(new Tile[size]));
            }
        }

        public void PlaceTile(Tile tile, int x, int y)
        {

            this.tiles[x][y] = tile;
        }

        public Boolean FreeTokenSpot(List<int> position) {
            return !tokenPositions.ContainsValue(position);
        }
    }
}
