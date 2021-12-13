using EngineBalloon.Graphics;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects.Prizes.Creators
{
    public class PrizeHealthCreator : PrizeCreator
    {
        public PrizeHealthCreator(Sprite sprite) 
            : base(sprite)
        {
        }

        public override Prize GetPrize(Vector2 position)
        {
            return new PrizeHealth(_sprite, position);
        }
    }
}
