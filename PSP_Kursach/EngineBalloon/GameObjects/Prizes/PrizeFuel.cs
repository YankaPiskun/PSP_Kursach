using EngineBalloon.Graphics;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects.Prizes
{
    public class PrizeFuel : Prize
    {
        public PrizeFuel(Sprite sprite, Vector2 position = default, float angle = 0)
            : base(sprite, position, angle)
        {
        }

        public override void UsePrize(Player player)
        {
            player.Fuel += 15f;
        }
    }
}
