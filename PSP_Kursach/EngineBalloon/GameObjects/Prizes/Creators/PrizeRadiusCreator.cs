using EngineBalloon.Graphics;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects.Prizes.Creators
{
    public class PrizeRadiusCreator : PrizeCreator
    {
        public PrizeRadiusCreator(Sprite sprite) 
            : base(sprite)
        {
        }

        public override Prize GetPrize(Vector2 position)
        {
            return new PrizeRadius(_sprite, position);
        }
    }
}
