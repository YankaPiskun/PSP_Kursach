using EngineBalloon.Graphics;
using EngineBalloon.Physics.Interfaces;
using OpenTK.Mathematics;

namespace EngineBalloon.GameObjects.Prizes
{
    public class Prize : GameObject
    {
        public Prize(Sprite sprite, Vector2 position = default, float angle = 0) 
            : base(sprite, position, angle)
        {
            Radius = 20f;
        }

        public virtual void UsePrize(Player player)
        {

        }
    }
}
