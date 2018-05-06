using System;
using System.Linq;
using System.Collections.Generic;
namespace TsuroTheSecond
{
    public class MPlayer2 : IPlayer
    // MPlayer with a random chooseTile strategy
    {
        private string name;
        private string color;
        private List<string> other_players;

        public MPlayer2(string _name)
        {
            name = _name;
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

        public Position PlacePawn(Board board)
        {
            // the board should hold other player start positions so that it can be checked
            // if other players are already at this spot
            Position position = new Position(-1, 0, 2);
            while (!board.FreeTokenSpot(position))
            {
                // make this thoroughly checking every position on the board
                // but for right now just check all the top tiles
                position.y += 1;
                if (position.y > Constants.boardSize - 1)
                {
                    throw new Exception("incomplete place pawn check");
                }
            }
            return position;
        }

        public Tile PlayTurn(Board board, List<Tile> hand, int unused)
        {
            // all legal options
            List<Tile> legal_options = board.AllPossibleTiles(this.color, hand);
            // all legal options, rid of overlapped.
            IDictionary<string, Tile> unique_legal_options = new Dictionary<string, Tile>();
            foreach (Tile each in legal_options)
            {
                string path_map = each.PathMap();
                if (!(unique_legal_options.ContainsKey(path_map)))
                {
                    unique_legal_options.Add(path_map, each);
                }
            }
            // new list of legal tiles sorted by symmetricity.
            List<Tile> sorted_legal_options = unique_legal_options.Values.ToList().OrderBy(obj => obj.symmetricity).ToList();
            return sorted_legal_options[0];
        }

        public void EndGame(Board board, List<string> colors)
        {
            if (colors.Contains(color))
            {
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("You lose!");
            }
        }
    }
}
