using System;
using System.Collections.Generic;
namespace TsuroTheSecond
{
    public class Tile : ITile
    {
        private int id;
        public List<List<int>> paths;

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

    }
}
