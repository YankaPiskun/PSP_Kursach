using EngineBalloon.Graphics;
using EngineBalloon.Physics.Interfaces;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects
{
    internal class GameObject : IPhysic
    {
        public Vector2 Position { get; set; }
        public float X { get => Position.X; set => Position = new Vector2(value, Y); }
        public float Y { get => Position.Y; set => Position = new Vector2(X, value); }

        public Vector2 Scale { get; set; }

        public float Angle { get; set; }

        public bool FlipY { get; set; }

        public Sprite Sprite { get; set; }

        public float GravityScale { get; set; }

        public GameObject(Sprite sprite, Vector2 position = default, float angle = default)
        {
            Position = position;
            Scale = Vector2.One;
            Angle = angle;
            Sprite = sprite;
        }

        public void ResizeByWindow(int width, int height)
        {
            Scale = new Vector2(Sprite.Texture.Width / (float)width, Sprite.Texture.Height / (float)height);
        }

        public virtual void Draw()
        {
            Sprite.Draw(Position, Scale, Angle, FlipY);
        }
    }
}
