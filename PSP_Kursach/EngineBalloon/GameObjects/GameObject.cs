using EngineBalloon.Graphics;
using EngineBalloon.Physics.Interfaces;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects
{
    public class GameObject : IPhysic
    {
        public virtual Vector2 Position { get; set; }
        public virtual float X { get => Position.X; set => Position = new Vector2(value, Y); }
        public virtual float Y { get => Position.Y; set => Position = new Vector2(X, value); }

        public virtual Vector2 Direction { get; set; }

        public virtual Vector2 Scale { get; set; }

        public virtual float Angle { get; set; }

        public virtual bool FlipY { get; set; }

        public virtual Sprite Sprite { get; set; }

        public virtual float GravityScale { get; set; }

        public GameObject(Sprite sprite, Vector2 position = default, float angle = default)
        {
            Position = position;
            Scale = Vector2.One;
            Angle = angle;
            Sprite = sprite;
        }

        public GameObject() { }

        public virtual void ResizeByWindow(int width, int height)
        {
            Scale = new Vector2(Sprite.Texture.Width / (float)width, Sprite.Texture.Height / (float)height);
        }

        public virtual void Draw()
        {
            Sprite.Draw(Position, Scale, Angle, FlipY);
        }

        public virtual void Draw(Vector2 Scale)
        {
            Sprite.Draw(Position, Scale, Angle, FlipY);
        }

        public virtual void FixedUpdate(float deltaTime)
        {
        }
    }
}
