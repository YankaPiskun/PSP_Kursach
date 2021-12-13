using EngineBalloon.Graphics;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects.Prizes
{
    public class PrizeHealth : Prize
    {
        public PrizeHealth(Sprite sprite, Vector2 position = default, float angle = 0) 
            : base(sprite, position, angle)
        {
        }

        public override void UsePrize(Player player)
        {
            player.HP += 10;
        }
    }
}
