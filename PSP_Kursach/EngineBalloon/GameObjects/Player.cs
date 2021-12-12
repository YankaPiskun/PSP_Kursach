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

        public Player(Sprite sprite, Vector2 position = default, float angle = 0) 
            : base(sprite, position, angle)
        {
            GravityScale = 1.0f;
        }

        public void Move(KeyboardState keyboardState, float deltaTime)
        {
            if (keyboardState.IsKeyDown(Keys.S))
            {
                Y -= MoveY * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                Y += MoveY * deltaTime;
            }

            if (keyboardState.IsKeyPressed(Keys.Space))
            {
            }
        }
    }
}
