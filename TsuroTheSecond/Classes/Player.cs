using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class Player
    {
        public Position position;
        public List<Tile> Hand;
        public readonly int age;
        private IPlayer player;

        public Player(IPlayer p, int _age) {
            Hand = new List<Tile>();
            age = _age;
            player = p;
        }
        public void InitPlayerPosition(int x, int y, int port){
            position = new Position(x, y, port);

        }


        public Boolean IsDead() {
            if ( (this.position.x < 0) || (this.position.x > 5) || (this.position.y < 0) || (this.position.y > 5) ) {
                return true;
            } else {
                return false;
            }
        }

        public void UpdatePosition(Board board){
            Position cur_pos = this.position;
            List<int> nxt_pos = new List<int>(3){0, 0, 0};
            int[] port_table = new int[]{5, 4, 7, 6, 1, 0, 3, 2};
            Tile nxt_tile = null;
            int enter_port, heading;
            bool recur = true;
            heading = cur_pos.port/2;

            while (recur) {
                // calculate next x, y from onward
                switch (heading) {
                    case 0:
                        nxt_pos[0] = cur_pos.x;
                        nxt_pos[1] = cur_pos.y - 1;
                        break;
                    case 1:
                        nxt_pos[0] = cur_pos.x + 1;
                        nxt_pos[1] = cur_pos.y;
                        break;
                    case 2:
                        nxt_pos[0] = cur_pos.x;
                        nxt_pos[1] = cur_pos.y + 1;
                        break;
                    case 3:
                        nxt_pos[0] = cur_pos.x - 1;
                        nxt_pos[1] = cur_pos.y;
                        break;
                    default:
                        break;
                }
                // see if that position if the next tile is there.
                // if not, break
                // if so, update cur_pos

                try{
                    nxt_tile = board.tiles[nxt_pos[0]][nxt_pos[1]];
                } catch(Exception) {
                    recur = false;
                }
                if (nxt_tile == null) {
                    recur = false;
                } else {
                    cur_pos.x = nxt_pos[0];
                    cur_pos.y = nxt_pos[1];
                    enter_port = port_table[cur_pos.port];
                    // find destination port in tile by enterport and update cur_pos
                    if( recur ){
                        cur_pos.port = nxt_tile.FindEndofPath(enter_port);
                    } else {
                        cur_pos.port = enter_port;
                    }
                    heading = cur_pos.port / 2;
                }
            }
            this.position = cur_pos;
        }

        public void AddTiletoHand(Tile tile){
            if( this.Hand.Count > 3 ) {
                throw new Exception("Player hand is already full!");
            }
            this.Hand.Add(tile);
        }

        public void RemoveTilefromHand(Tile tile) {
            if( this.Hand.Count < 0 ) {
                throw new Exception("Player hand is already empty!");
            }
            this.Hand.Remove(tile);
        }

        public bool TileinHand(Tile tile) {
            if ( this.Hand.Find(each => each.id == tile.id) == null ) {
                return false;
            } else {
                return true;
            }

        }
    }
}
