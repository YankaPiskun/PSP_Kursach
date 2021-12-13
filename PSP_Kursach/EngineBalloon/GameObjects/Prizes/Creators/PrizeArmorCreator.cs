using EngineBalloon.Graphics;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects.Prizes.Creators
{
    public class PrizeArmorCreator : PrizeCreator
    {
        public PrizeArmorCreator(Sprite sprite) : base(sprite)
        {
        }

        public override Prize GetPrize(Vector2 position)
        {
            return new PrizeArmor(_sprite, position);
        }
    }
}
