using EngineBalloon.Graphics;
using OpenTK.Mathematics;
using System;

namespace EngineBalloon.GameObjects.Bullets
{
    public class Bullet : GameObject, IBullet, ICloneable
    {
        public Bullet(Sprite sprite, Vector2 position = default, float angle = 0) 
            : base(sprite, position, angle)
        {
            Direction = Vector2.Zero;
            Damage = 1;
            Speed = 1.0f;
            Radius = 10.0f;
            GravityScale = 50f;
        }
        public Bullet() : base(){}

        public virtual int Damage { get; set; }
        public virtual float Speed { get; set; }
        public virtual float Radius { get; set; }

        public virtual void Move(float deltaTime)
        {
            Position += Direction * Speed * deltaTime;
            Direction.Normalize();
        }

        public virtual object Clone()
        {
            return new Bullet(Sprite, Position, Angle) { Scale = Scale, GravityScale = GravityScale };
        }

        public override void FixedUpdate(float deltaTime)
        {
            Move(deltaTime);
            base.FixedUpdate(deltaTime);
        }
    }
}
