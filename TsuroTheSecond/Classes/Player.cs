using System;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class Player
    {
        List<int> position;
        int onward;
        List<Tile> Hand;
        int age;
        string color;

        public Player(List<int> _position, int _onward, List<Tile> _Hand, int _age, string _color)
        {
            Hand = _Hand;
            age = _age;
            color = _color;
            // port, x, y range check
            if ( (_position[2] < 8 || _position[2] > -1) && ( ((_position[0] == -1) || (_position[0] == 6)) ^ ( (_position[1] == -1) || (_position[1] == 6)) ) ) {
                position = _position;
            } else {
                throw new ArgumentException("Illegal position to initialize player", "_position");
            }
            // invalid _onward
            if ( _onward != (position[2] / 2) ) {
                throw new ArgumentException("Illegal onward value", "_onward");
            }
            // invalid combinations of _onward and initial position
            switch(_onward) {
                case 0:
                    if( _position[1] == 6 ){}
                    else{throw new ArgumentException("Illegal onward value", "_onward");}
                    break;
                case 1:
                    if (_position[0] == -1) { }
                    else { throw new ArgumentException("Illegal onward value", "_onward"); }
                    break;
                case 2:
                    if (_position[1] == -1) { }
                    else { throw new ArgumentException("Illegal onward value", "_onward"); }
                    break;
                case 3:
                    if (_position[0] == 6) { }
                    else { throw new ArgumentException("Illegal onward value", "_onward"); }
                    break;
                default:
                    throw new ArgumentException("Illegal onward value", "_onward");
            }
        }   

        private Boolean CheckDead(List<int> _position) {
            if ( (_position[0] < 0) || (_position[0] > 5) || (_position[1] < 0) || (_position[1] > 5) ) {
                return true;
            } else {
                return false;
            }
        }

        void updatePosition(Board board){
            
        }
    }
}
