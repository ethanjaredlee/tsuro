using System;
using System.Collections.Generic;
namespace TsuroTheSecond
{
    public class MPlayer1 : IPlayer
    // MPlayer with a random chooseTile strategy
    {
        private string name;
        private string color;
        private List<string> other_players;

        public MPlayer1(string _name)
        {
            name = _name;
        }

        public Tile ChooseTile(List<Tile> hand)
        {
            return null;
        }

        public String GetName()
        {
            return name;
        }

        public void Initialize(string _color, List<string> other_colors)
        {
            color = _color;
            other_players = other_colors;
        }

        public List<int> PlacePawn(Board board)
        {
            // the board should hold other player start positions so that it can be checked
            // if other players are already at this spot
            List<int> position = new List<int> { 0, -1, 5 };
            while (!board.FreeTokenSpot(position)) {
                // make this thoroughly checking every position on the board
                // but for right now just check all the top tiles
                position[0] += 1; 
                if (position[0] > Constants.boardSize-1) {
                    throw new Exception("incomplete place pawn check");
                }
            }
            return position;
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
