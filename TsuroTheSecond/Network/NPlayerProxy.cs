using System;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;

namespace TsuroTheSecond
{
    public class NPlayerProxy
    {
        IPlayer player;
        ToXml ToXml;
        Parser Parser;

        public NPlayerProxy()
        {
            player = new MostSymmetricPlayer("network player");
            ToXml = new ToXml();
            Parser = new Parser();
        }

        /* 
         * returns an xml-string representing proper output
         * to the input xml
         */
        public string ParseInput(string input) {
            XmlDocument document = new XmlDocument();
            document.LoadXml(input);

            string message = document.FirstChild.Name;
            string response;
            switch (message) {
                //case "get-name":

                    //break;
                case "initialize":
                    (string, List<string>) init = Parser.InitializeParse(input);

                    player.Initialize(init.Item1, init.Item2);

                    response = ToXml.VoidtoXml();
                    break;
                case "place-pawn":
                    Board board = Parser.PlacePawnParse(input);

                    Position initPosition = player.PlacePawn(board);

                    response = ToXml.FormatXml(ToXml.PawnLoctoXml(initPosition));
                    break;
                case "play-turn":
                    (Board, List<Tile>, int) turnInput = Parser.PlayTurnParse(input);

                    Tile playTile = player.PlayTurn(turnInput.Item1, turnInput.Item2, turnInput.Item3);

                    response = ToXml.FormatXml(ToXml.TiletoXml(playTile));
                    break;
                case "end-game":
                    (Board, List<string>) endGame = Parser.EndGameParse(input);

                    player.EndGame(endGame.Item1, endGame.Item2);

                    response = ToXml.VoidtoXml();
                    break;
                default:
                    throw new ArgumentException("invalid xml given");
            }

            return response;
        }

    }
}
