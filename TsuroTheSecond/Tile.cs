using System;
using System.Collections.Generic;
namespace TsuroTheSecond
{
    public class Tile
    {
        private int id;
        public List<List<int>> paths;
        public Tile(int _id, List<List<int>> path)
        {
            id = _id;
            paths = new List<List<int>>(4);
            foreach(List<int> item in path){
                paths.Add(item);
            }
        }

        public void rotate() {
            foreach(List<int> item in this.paths){
                item[0] = (item[0] + 2) % 8;
                item[1] = (item[1] + 2) % 8;
            }
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
