using System;
namespace TsuroTheSecond
{
    public interface IServer
    {
        void playTurn(Player player);
        Boolean legalPlay(SPlayer player, Board board, Tile tile);
        void initGame();
    }
}
