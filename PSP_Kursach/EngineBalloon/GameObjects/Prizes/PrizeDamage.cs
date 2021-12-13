using EngineBalloon.GameObjects.Bullets;
using EngineBalloon.Graphics;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects.Prizes
{
    public class PrizeDamage : Prize
    {
        public PrizeDamage(Sprite sprite, Vector2 position = default, float angle = 0)
            : base(sprite, position, angle)
        {
        }

        public override void UsePrize(Player player)
        {
            player.BaseBullet = new BulletDamage(player.BaseBullet);
        }
    }
}
