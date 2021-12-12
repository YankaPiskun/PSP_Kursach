using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects.Bullets
{
    public class BulletRadius : BulletDecorator
    {
        public BulletRadius(Bullet bullet) 
            : base(bullet)
        {
        }

        public override float Radius { get => _bullet.Radius + 10f; set => _bullet.Radius = value; }
        public override Vector2 Scale { get => _bullet.Scale * 1.10f; set => _bullet.Scale = value; }

        public override object Clone()
        {
            return new BulletRadius((Bullet)_bullet.Clone());
        }
    }
}
