using EngineBalloon.GameObjects.Bullets;
using EngineBalloon.Graphics;
using EngineBalloon.Physics.Interfaces;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineBalloon.GameObjects
{
    internal class Player : GameObject, IPhysic
    {
        private const float MoveY = 0.2f;

        private Bullet _baseBullet;

        public Player(Bullet baseBullet, Sprite sprite, Vector2 position = default, float angle = 0) 
            : base(sprite, position, angle)
        {
            GravityScale = 1.0f;
            _baseBullet = baseBullet;
        }

        public void Move(KeyboardState keyboardState, Action<Bullet> addBullet, float deltaTime)
        {
            if (keyboardState.IsKeyDown(Keys.S))
            {
                Direction -= new Vector2(0.0f, MoveY * deltaTime);
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                Direction += new Vector2(0.0f, MoveY * deltaTime);
            }

            Position += Direction;
            Direction = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _baseBullet = new BulletRadius(_baseBullet);
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                _baseBullet = new BulletSpeed(_baseBullet);
            }

            if (keyboardState.IsKeyPressed(Keys.Space))
            {
                _baseBullet.Position = Position;
                addBullet(_baseBullet);
            }
        }
    }
}
