using System;
namespace TsuroTheSecond
{
    public interface IPlayer
    {
        Boolean ChooseLocation();
        void ChooseOrientation();
        void updatePosition();
    }
}
