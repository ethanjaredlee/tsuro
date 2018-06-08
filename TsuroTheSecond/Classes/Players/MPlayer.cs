using System;
using System.Collections.Generic;
namespace TsuroTheSecond
{
    public class MPlayer
    {
        protected string name;
        protected string color;
        protected List<string> player_colors;
        public enum State { start, initialized, loop, end, replacement };
        public State playerState;

        public MPlayer(string _name)
        {
            playerState = State.start;
            name = _name;
        }

        public String GetName()
        {
            return name;
        }

        public void Initialize(string _color, List<string> all_colors)
        {
            if (playerState != State.start && playerState != State.replacement)
            {
                throw new Exception("Player should be in start state");
            }
            color = _color;
            player_colors = all_colors;
            playerState = State.initialized;
        }

        public Position PlacePawn(Board board)
        {
            if (playerState != State.initialized && playerState != State.replacement)
            {
                throw new Exception("Player should be in initialized state");
            }
            // the board should hold other player start positions so that it can be checked
            // if other players are already at this spot
            Position position = new Position(0, -1, 5);
            while (!board.FreeTokenSpot(position))
            {
                // make this thoroughly checking every position on the board
                // but for right now just check all the top tiles
                position.x++;
                if (position.x > Constants.boardSize - 1)
                {
                    throw new Exception("incomplete place pawn check");
                }
            }
            playerState = State.loop;
            return position;
        }

        public void EndGame(Board board, List<string> colors)
        {
            if (playerState != State.loop && playerState != State.replacement)
            {
                throw new Exception("Player is in wrong state");
            }
            playerState = State.end;
            if (colors.Contains(color))
            {
                Console.WriteLine(color + " won!");
            }
            else
            {
                Console.WriteLine(color + " lost!");
            }
        }
    }
}
