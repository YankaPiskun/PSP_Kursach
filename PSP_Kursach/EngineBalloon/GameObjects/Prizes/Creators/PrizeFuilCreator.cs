using EngineBalloon.Graphics;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects.Prizes.Creators
{
    public class PrizeFuilCreator : PrizeCreator
    {
        public PrizeFuilCreator(Sprite sprite) 
            : base(sprite)
        {
        }

        public override Prize GetPrize(Vector2 position)
        {
            return new PrizeFuel(_sprite, position);
        }
    }
}
