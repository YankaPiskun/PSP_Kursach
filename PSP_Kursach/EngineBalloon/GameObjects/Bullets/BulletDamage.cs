namespace EngineBalloon.GameObjects.Bullets
{
    public class BulletDamage : BulletDecorator
    {
        public BulletDamage(Bullet bullet) 
            : base(bullet)
        {
        }


        public override int Damage { get => _bullet.Damage + 1; set => _bullet.Damage = value; }

        public override object Clone()
        {
            return new BulletDamage((Bullet)_bullet.Clone());
        }
    }
}
