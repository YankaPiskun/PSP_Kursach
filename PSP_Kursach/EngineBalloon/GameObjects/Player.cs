using EngineBalloon.GameObjects.Bullets;
using EngineBalloon.Graphics;
using EngineBalloon.Physics.Interfaces;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace EngineBalloon.GameObjects
{
    public class Player : GameObject, IPhysic
    {
        private const float MoveY = 0.02f;

        public Bullet BaseBullet { get; set; }

        public int HP { get; set; }

        public int Armor { get; set; }

        public float Fuel { get; set; }

        public Player(Bullet baseBullet, Sprite sprite, Vector2 position = default, float angle = 0) 
            : base(sprite, position, angle)
        {
            GravityScale = 1.0f;
            BaseBullet = (Bullet)baseBullet.Clone();
            BaseBullet.Parent = this;
            Radius = 100f;
        }

        public void UseControl(KeyboardState keyboardState, Action<Bullet> addBullet)
        {
            if (keyboardState.IsKeyDown(Keys.S))
            {
                Direction -= new Vector2(0.0f, MoveY);
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                Direction += new Vector2(0.0f, MoveY);
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                BaseBullet = new BulletRadius(BaseBullet);
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                BaseBullet = new BulletSpeed(BaseBullet);
            }

            if (keyboardState.IsKeyPressed(Keys.Space))
            {
                BaseBullet.Position = Position;
                addBullet(BaseBullet);
            }
        }

        public override void FixedUpdate(float deltaTime)
        {
            Position += Direction;
            Direction = Vector2.Zero;
        }

        public void TakeDamage(int damage)
        {
            if(Armor > 0)
            {
                Armor--;
            }
            else
            {
                HP -= damage;
            }
        }
    }
}
