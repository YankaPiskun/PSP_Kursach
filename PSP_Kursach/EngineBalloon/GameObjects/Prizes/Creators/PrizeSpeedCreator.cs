using EngineBalloon.Graphics;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects.Prizes.Creators
{
    public class PrizeSpeedCreator : PrizeCreator
    {
        public PrizeSpeedCreator(Sprite sprite) 
            : base(sprite)
        {
        }

        public override Prize GetPrize(Vector2 position)
        {
            return new PrizeSpeed(_sprite, position);
        }
    }
}
