using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class Player
    {
        public List<int> position;

        public List<Tile> Hand;
        int age;
        string color;
        public List<int> nextTilePosition;
        private IPlayer player;
        public Boolean dragonTile;

        public Player(IPlayer p, int _age, string _color) {
            dragonTile = false;
            Hand = new List<Tile>();
            age = _age;
            color = _color;
            player = p;
        }

        public void InitPlayerPosition(List<int> _position)
        {
            // port, x, y range check
            // i think? this should be the phantom block. like if we want our player in the very
            // top left notch, then the initial position is [0, -1, 0]
            if ( (_position[2] < 8 && _position[2] > -1) && ( ((_position[0] == -1) || (_position[0] == 6)) ^ ( (_position[1] == -1) || (_position[1] == 6)) ) ) {
                position = _position;
            } else {
                throw new ArgumentException("Illegal position to initialize player", "_position");
            }
            // invalid _onward
            int onward = position[2] / 2;
            nextTilePosition = new List<int> { position[0], position[1] };
            switch(onward) {
                case 0:
                    // onward is the tile above
                    if (_position[1] == 6) { }
                    else { throw new ArgumentException("Illegal port and xy combination"); }
                    nextTilePosition[1] -= 1;
                    if (nextTilePosition[1] < 0) {
                        throw new ArgumentException("I think the player is dead?");
                    }
                    break;
                case 1:
                    if (_position[0] == -1) { }
                    else { throw new ArgumentException("Illegal port and xy combination"); }
                    // onward is the tile to the right
                    nextTilePosition[0] += 1;
                    if (nextTilePosition[0] > Constants.boardSize - 1)
                    {
                        throw new ArgumentException("I think the player is dead?");
                    }
                    break;
                case 2:
                    if (_position[1] == -1) { }
                    else { throw new ArgumentException("Illegal port and xy combination"); }
                    // onward is the tile below
                    nextTilePosition[1] += 1;
                    if (nextTilePosition[1] > Constants.boardSize - 1)
                    {
                        throw new ArgumentException("I think the player is dead?");
                    }
                    break;
                case 3:
                    if (_position[0] == 6) { }
                    else { throw new ArgumentException("Illegal port and xy combination"); }
                    // onward is the tile to the left
                    nextTilePosition[0] -= 1;
                    if (nextTilePosition[0] < 0)
                    {
                        throw new ArgumentException("I think the player is dead?");
                    }
                    break;
                default:
                    throw new ArgumentException("Illegal onward value", "_onward");
            }
        }   

        public Boolean IsDead() {
            if ( (this.position[0] < 0) || (this.position[0] > 5) || (this.position[1] < 0) || (this.position[1] > 5) ) {
                return true;
            } else {
                return false;
            }
        }

        public void UpdatePosition(Board board){
            List<int> cur_pos = new List<int>(3){0, 0, 0};
            List<int> nxt_pos = new List<int>(3){0, 0, 0};
            int[] port_table = new int[]{5, 4, 7, 6, 1, 0, 3, 2};
            Tile nxt_tile = null;
            int enter_port, heading;
            bool recur = true;
            cur_pos = this.position;
            heading = cur_pos[2]/2;

            while (recur) {
                // calculate next x, y from onward
                switch (heading) {
                    case 0:
                        nxt_pos[0] = cur_pos[0];
                        nxt_pos[1] = cur_pos[1] - 1;
                        break;
                    case 1:
                        nxt_pos[0] = cur_pos[0] + 1;
                        nxt_pos[1] = cur_pos[1];
                        break;
                    case 2:
                        nxt_pos[0] = cur_pos[0];
                        nxt_pos[1] = cur_pos[1] + 1;
                        break;
                    case 3:
                        nxt_pos[0] = cur_pos[0] - 1;
                        nxt_pos[1] = cur_pos[1];
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
                    Console.WriteLine(cur_pos[0]);
                    Console.WriteLine(cur_pos[1]);
                    Console.WriteLine(cur_pos[2]);
                    recur = false;
                } else {
                    cur_pos[0] = nxt_pos[0];
                    cur_pos[1] = nxt_pos[1];
                    enter_port = port_table[cur_pos[2]];
                    // find destination port in tile by enterport and update cur_pos
                    if( recur ){
                        cur_pos[2] = nxt_tile.FindEndofPath(enter_port);
                    } else {
                        cur_pos[2] = enter_port;
                    }
                    heading = cur_pos[2] / 2;
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
