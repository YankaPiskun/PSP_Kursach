namespace EngineBalloon.GameObjects.Bullets
{
    public class BulletSpeed : BulletDecorator
    {
        public BulletSpeed(Bullet bullet) 
            : base(bullet)
        {
        }

        public override float Speed { get => _bullet.Speed + 0.1f; set => _bullet.Speed = value; }

        public override object Clone()
        {
            return new BulletSpeed((Bullet)_bullet.Clone());
        }
    }
}
