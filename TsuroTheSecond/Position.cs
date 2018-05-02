using System;
namespace TsuroTheSecond
{
    public class Position
    {
        public int x;
        public int y;
        public int port;
        public Position(int _x, int _y, int _port)
        {
            if ((_port < 8 && _port > -1) && (((_x == -1) || (_x == 6)) ^ ((_y == -1) || (_y == 6))))
            {
                x = _x;
                y = _y;
                port = _port;
            }
            else
            {
                throw new ArgumentException("Illegal position to initialize player", "_x, _y, _port");
            }
            switch (port / 2)
            {
                case 0:
                    // onward is the tile above
                    if (y == 6) { }
                    else { throw new ArgumentException("Illegal port and xy combination"); }
                    break;
                case 1:
                    if (x == -1) { }
                    else { throw new ArgumentException("Illegal port and xy combination"); }
                    break;
                case 2:
                    if (y == -1) { }
                    else { throw new ArgumentException("Illegal port and xy combination"); }
                    break;
                case 3:
                    if (x == 6) { }
                    else { throw new ArgumentException("Illegal port and xy combination"); }
                    break;
                default:
                    throw new ArgumentException("Illegal onward value", "_onward");
            }
        }

    }
}
