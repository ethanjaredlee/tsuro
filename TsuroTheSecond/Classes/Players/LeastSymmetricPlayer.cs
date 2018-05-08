using System;
using System.Linq;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class LeastSymmetricPlayer : MPlayer, IPlayer
    {
        public LeastSymmetricPlayer(string _name) : base(_name)
        {
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
    }
}
