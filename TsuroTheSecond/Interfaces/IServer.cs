using System;
namespace TsuroTheSecond
{
    public interface IServer
    {
        void PlayTurn(Player player);
        Boolean LegalPlay(SPlayer player, Board board, Tile tile);
        void InitGame();
    }
}
