using System;
using System.Collections.Generic;
namespace TsuroTheSecond
{
    public interface IPlayer
    {
        Tile ChooseTile(List<Tile> hand);
    }
}
