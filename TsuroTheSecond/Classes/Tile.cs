using System;
using System.Linq;
using System.Collections.Generic;
namespace TsuroTheSecond
{
    public class Tile : ITile
    {
        public readonly int id;
        public List<List<int>> paths;
        public int symmetricity;

        public Tile(int _id, List<int> path)
        {
            if (path.Count > 8) {
                Console.WriteLine("Warning: more than 8 ports specified");
            }

            id = _id;
            this.paths = new List<List<int>>(4);
            for (int i = 0; i < path.Count; i+=2) {
                List<int> p = new List<int> { path[i], path[i + 1] };
                this.paths.Add(p);
            }
            //for (int i = 0; i < 4; i++) {
            //    for (int j = 0; j < 2; j++) {
            //        Console.WriteLine(this.paths[i][j]);
            //    }
            //}
        }


        public void Rotate() {
            foreach(List<int> item in this.paths){
                item[0] = (item[0] + 2) % 8;
                item[1] = (item[1] + 2) % 8;
            }
        }

        public int FindEndofPath(int start) {
            // find destination port in tile by entering port
            for (int i = 0; i < 4; i++)
            {
                List<int> route = this.paths[i];
                if (route[0] == start){
                    return route[1];
                }
                else if (route[1] == start)
                {
                    return route[0];
                }
            }
            return -1;
        }

        public void PrintMe()
        {
            foreach (List<int> each in this.paths){
                Console.WriteLine(each[0]);
                Console.WriteLine(" leads to ");
                Console.WriteLine(each[1]);
                Console.WriteLine("\n");
            }
        }
        public string PathMap(){
            // init all to -1.
            int[] path_map = Enumerable.Repeat(-1, 8).ToArray();
            for (int i = 0; i < 8; i++){
                // check if it's initial
                if( path_map[i] == -1 ) {
                    int end_of_i = this.FindEndofPath(i);
                    path_map[i] = end_of_i;
                    path_map[end_of_i] = i;
                }
            }
            return string.Join("", path_map);
        }
        public void JudgeSymmetric() {
            int sym;
            List<Tile> rotatos = new List<Tile>();
            HashSet<string> rotatos_paths = new HashSet<string>();
            for (int i = 0; i < 4; i++) {
                rotatos.Add(this);
                rotatos_paths.Add(rotatos[i].PathMap());
                this.Rotate();
            }
            sym = rotatos_paths.Count;
            Console.WriteLine(sym);
            if (sym == 1 || sym == 2 || sym == 4) {
                this.symmetricity = sym;
            } else {
                throw new System.Exception("symmetricity of a tile can only be 1, 2, or 4");
            }
        }

    }
}
