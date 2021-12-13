using EngineBalloon.Graphics;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects
{
    public class Earth : GameObject
    {
        public Earth(Sprite sprite, Vector2 position = default, float angle = 0) 
            : base(sprite, position, angle)
        {
            DeadY = -0.6f;
            Radius = 0f;
            Scale = new Vector2(Scale.X, 0.2f);
        }

        public float DeadY { get; set; }
    }
}
