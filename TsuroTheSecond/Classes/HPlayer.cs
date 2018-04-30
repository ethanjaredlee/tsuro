using System;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class HPlayer : IPlayer
    {
        public HPlayer()
        {
        }

        public String GetName() { return "hi"; }
        public void Initialize(string color, List<string> other_colors) { }
        public List<int> PlacePawn(Board board) { return new List<int> { 1, 2, 3 }; }
        public Tile PlayTurn(Board board, List<Tile> hand, int unused) { Tile how = new Tile(1, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 }); return how; } //suitably rotated tile is the output
        public void EndGame(Board board, List<string> colors) { } // stat which player won.

        public Tile ChooseTile(List<Tile> hand) {
            return null;
        }
    }
}
