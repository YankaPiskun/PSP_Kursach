using EngineBalloon.Graphics;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects.Bullets
{
    public abstract class BulletDecorator : Bullet
    {
        protected Bullet _bullet;

        public BulletDecorator(Bullet bullet) 
            : base()
        {
            _bullet = bullet;
        }

        public override Vector2 Position { get => _bullet.Position; set => _bullet.Position = value; }
        public override float X { get => _bullet.X; set => _bullet.X = value; }
        public override float Y { get => _bullet.Y; set => _bullet.Y = value; }
        public override Vector2 Direction { get => _bullet.Direction; set => _bullet.Direction = value; }
        public override Vector2 Scale { get => _bullet.Scale; set => _bullet.Scale = value; }
        public override float Angle { get => _bullet.Angle; set => _bullet.Angle = value; }
        public override bool FlipY { get => _bullet.FlipY; set => _bullet.FlipY = value; }
        public override Sprite Sprite { get => _bullet.Sprite; set => _bullet.Sprite = value; }
        public override float GravityScale { get => _bullet.GravityScale; set => _bullet.GravityScale = value; }
        public override int Damage { get => _bullet.Damage; set => _bullet.Damage = value; }
        public override float Speed { get => _bullet.Speed; set => _bullet.Speed = value; }
        public override float Radius { get => _bullet.Radius; set => _bullet.Radius = value; }
        public override Player Parent { get => _bullet.Parent; set => _bullet.Parent = value; }

        public override object Clone()
        {
            return _bullet.Clone();
        }

        public override void Draw()
        {
            _bullet.Draw(Scale);
        }

        public override void FixedUpdate(float deltaTime)
        {
            Move(deltaTime);
        }

        public override void Move(float deltaTime)
        {
            Position += Direction * Speed * deltaTime;
            Direction.Normalize();
        }

        public override void ResizeByWindow(int width, int height)
        {
            _bullet.ResizeByWindow(width, height);
        }
    }
}
