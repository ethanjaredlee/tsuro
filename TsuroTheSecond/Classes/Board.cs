using System;
using System.Linq;
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
            if (size <= 0) {
                throw new ArgumentException("Board size must be > 0");
            }

            tiles = new List<List<Tile>>();
            for (int i = 0; i < size; i++)
            {
                tiles.Add(new List<Tile>(new Tile[size]));
            }

            tokenPositions = new Dictionary<string, List<int>>();
        }

        public void PlaceTile(Tile tile, int x, int y)
        {
            if (x < 0 || x > 5 || y < 0 || y > 5) {
                throw new ArgumentException("Tile placement is out of board range");
            }

            this.tiles[x][y] = tile;
        }

        public Boolean FreeTokenSpot(List<int> position) {
            return !tokenPositions.ContainsValue(position);
        }

        public void AddPlayerToken(string color, List<int> position) {
            // add safety check to make sure tiles arent initialized?
            // make sure color is in list of acceptable colors?

            if (tokenPositions.ContainsKey(color)) {
                throw new Exception("Initializing a second player of color " + color);
            }

            foreach (List<int> pos in tokenPositions.Values) {
                if (pos.SequenceEqual(position)) {
                    throw new Exception("Initializing a second player at position " +
                                        position[0].ToString() + ", " +
                                        position[1].ToString() + ", " +
                                        position[2].ToString());
                }
            }

            tokenPositions.Add(color, position);
        }
    }
}
