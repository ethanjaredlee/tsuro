using System;
using System.Collections.Generic;
namespace TsuroTheSecond
{
    public class MPlayer : IPlayer
    {
        public MPlayer()
        {
            
        }

        public Tile ChooseTile(List<Tile> hand)
        {
            return null;
        }

        public String GetName()
        {
            return null;
        }

        public void Initialize(string color, List<string> other_colors)
        {
        }

        public List<int> PlacePawn(Board board)
        {
            return null;
        }

        public Tile PlayTurn(Board board, List<Tile> hand, int unused)
        {
            return null;
        }

        public void EndGame(Board board, List<string> colors)
        {
        }
    }
}
