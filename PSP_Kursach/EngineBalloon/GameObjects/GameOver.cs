using EngineBalloon.Graphics;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects
{
    public class GameOver : GameObject
    {
        public GameOver(Sprite sprite, Vector2 position = default, float angle = 0) 
            : base(sprite, position, angle)
        {
        }
    }
}
