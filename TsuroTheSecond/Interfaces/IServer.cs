using System;
namespace TsuroTheSecond
{
    public interface IServer
    {
        void InitGame();

        Boolean LegalPlay(SPlayer player, Board board, Tile tile);

        void PlayTurn(Player player);

    }
}
