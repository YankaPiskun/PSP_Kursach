using EngineBalloon.Graphics;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects.Prizes.Creators
{
    public class PrizeDamageCreator : PrizeCreator
    {
        public PrizeDamageCreator(Sprite sprite) 
            : base(sprite)
        {
        }

        public override Prize GetPrize(Vector2 position)
        {
            return new PrizeDamage(_sprite, position);
        }
    }
}
