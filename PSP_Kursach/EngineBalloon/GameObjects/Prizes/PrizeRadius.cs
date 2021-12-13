using EngineBalloon.GameObjects.Bullets;
using EngineBalloon.Graphics;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects.Prizes
{
    public class PrizeRadius : Prize
    {
        public PrizeRadius(Sprite sprite, Vector2 position = default, float angle = 0)
            : base(sprite, position, angle)
        {
        }

        public override void UsePrize(Player player)
        {
            player.BaseBullet = new BulletRadius(player.BaseBullet);
        }
    }
}
