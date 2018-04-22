using System;
using System.Collections.Generic;
namespace TsuroTheSecond
{
    public class SPlayer : IPlayer
    {
        private class PlayerPosition {
            public int x;
            public int y;
            public int port;
            public PlayerPosition(int _x, int _y, int _port) {
                x = _x;
                y = _y;
                port = _port;
            }
        }

        private List<Tile> Hand;
        private PlayerPosition position;
        private string Color;

        public SPlayer(string color)
        {
            Color = color;
        }

        public void SetMarker(int x, int y, int port) {
            position = new PlayerPosition(x, y, port);
        }

        public bool Move() {
            return true; 
        }
    }
}
