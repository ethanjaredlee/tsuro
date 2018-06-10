﻿using System;
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
            HashSet<int> pathHashSet = new HashSet<int>(path);
            if (pathHashSet.Count != 8 || path.Count != 8) {
                throw new ArgumentException("There must be 8 unique ports specified");
            }

            List<int> outOfRangePorts = path.Where(x => x > 7 || x < 0).ToList();
            if (outOfRangePorts.Count > 0) {
                throw new ArgumentException("Ports must be in range 0 to 7");
            }

            id = _id;
            this.paths = new List<List<int>>(4);
            for (int i = 0; i < path.Count; i+=2) {
                List<int> p = new List<int> { path[i], path[i + 1] };
                this.paths.Add(p);
            }

            this.JudgeSymmetric();
        }

        public Tile (Tile tile){
            this.id = tile.id;
            this.paths = new List<List<int>>{
                new List<int>{tile.paths[0][0], tile.paths[0][1]},
                new List<int>{tile.paths[1][0], tile.paths[1][1]},
                new List<int>{tile.paths[2][0], tile.paths[2][1]},
                new List<int>{tile.paths[3][0], tile.paths[3][1]},
            };
            this.symmetricity = tile.symmetricity;
        }

        public Tile(int _id, List<List<int>> path) {
            this.paths = path;
            this.id = _id;
        }

        public Tile Copy() {
            return new Tile(this.id, this.paths);
        }

        public void Rotate() {
            foreach(List<int> item in this.paths){
                item[0] = (item[0] + 2) % 8;
                item[1] = (item[1] + 2) % 8;
            }
        }

        public int FindEndofPath(int start) {
            if (start > 7 || start < 0) {
                throw new ArgumentException("Start is out of range");
            }

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
            throw new ArgumentException("Port doesn't exist in tile");
        }

        public override string ToString()
        {
            string repr = "<Tile(id=" + this.id;
            if (this.id < 10) repr += " ";
            repr += " | ";
            foreach (List<int> each in this.paths){
                repr += "[" + each[0] + "," + each[1] + "],";
            }
            repr = repr.Substring(0, repr.Length - 1);
            repr += ")>";
            return repr;
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
            if (sym == 1 || sym == 2 || sym == 4) {
                this.symmetricity = sym;
            } else {
                throw new System.Exception("symmetricity of a tile can only be 1, 2, or 4");
            }
        }

        public Boolean CompareByPath(Tile comparison){
            return this.PathMap() == comparison.PathMap();
        }

        public override bool Equals(object obj) {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) {
                return false;
            }

            Tile tile = (Tile)obj;
            for (int i = 0; i < 4; i++) {
                tile.Rotate();
                if (this.CompareByPath(tile)) {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return PathMap().GetHashCode();
        }

    }
}
