﻿using System;
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
                throw new ArgumentException("Illegal position to initialize player");
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

        public Position(int _x, int _y, int _port, bool existing) {
            if (!existing) {
                throw new ArgumentException("Position shouldn't exist");
            }

            if (_x > 6 || _x < -1 || _y > 6 || _y < -1 || _port < 0 || _port > 7) {
                throw new ArgumentException("Invalid position: position not on board");
            }

            x = _x;
            y = _y;
            port = _port;
        }

        public Position(Position copy)
        {
            x = copy.x;
            y = copy.y;
            port = copy.port;
        }

        public bool OnEdge() {
            return ((port < 8 && port > -1) && (((x == -1) || (x == 6)) ^ ((y == -1) || (y == 6))));
        }

        public Position FlipPosition() {
            int newx = this.x;
            int newy = this.y;
            int newport = this.port;

            if (port > 7 || port < -1) {
                throw new ArgumentException("port not in range");
            }

            // top side
            if (port == 0 || port == 1) {
                newy--;
                if (port == 0) newport = 5;
                if (port == 1) newport = 4;
            } else if (port == 2 || port == 3) {
                // right side
                newx++;
                if (port == 2) newport = 7;
                if (port == 3) newport = 6;
            } else if (port == 4 || port == 5) {
                // bot
                newy++;
                if (port == 4) newport = 1;
                if (port == 5) newport = 0;
            } else {
                // left 
                newx--;
                if (port == 6) newport = 3;
                if (port == 7) newport = 2;
            }

            return new Position(newx, newy, newport, true);
        }

        public static bool operator ==(Position a, Position b) {
            return a.x == b.x && a.y == b.y && a.port == b.port;
        }

        public static bool operator !=(Position a, Position b)
        {
            return !(a.x == b.x && a.y == b.y && a.port == b.port);
        }

		public override bool Equals(object obj)
		{
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            Position position = (Position)obj;
            return this.x == position.x && this.y == position.y && this.port == position.port;
		}

		public override int GetHashCode()
		{
            String s = this.x.ToString() + this.y.ToString() + this.port.ToString();
            return s.GetHashCode();
		}

        public override string ToString()
        {
            return "<Position(" + x + "," + y + "," + port + ")>";
        }

        public (int, int) WhatNext(){
            // invalid _onward
            int next_x = x;
            int next_y = y;
            switch (port / 2)
            {
                case 0:
                    // onward is the tile above
                    next_y -= 1;
                    if (next_y < 0)
                    {
                        throw new ArgumentException("I think the player is dead?");
                    }
                    break;
                case 1:
                    // onward is the tile to the right
                    next_x += 1;
                    if (next_x > Constants.boardSize - 1)
                    {
                        throw new ArgumentException("I think the player is dead?");
                    }
                    break;
                case 2:
                    // onward is the tile below
                    next_y += 1;
                    if (next_y > Constants.boardSize - 1)
                    {
                        throw new ArgumentException("I think the player is dead?");
                    }
                    break;
                case 3:
                    // onward is the tile to the left
                    next_x -= 1;
                    if (next_x < 0)
                    {
                        throw new ArgumentException("I think the player is dead?");
                    }
                    break;
                default:
                    throw new ArgumentException("Illegal onward value", "_onward");
            }
            return (next_x, next_y);
        }

    }
}
