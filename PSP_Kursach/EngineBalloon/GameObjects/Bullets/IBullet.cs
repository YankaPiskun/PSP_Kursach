using EngineBalloon.Physics.Interfaces;

namespace EngineBalloon.GameObjects.Bullets
{
    public interface IBullet: ICircle
    {

        public int Damage { get; set; }

        public float Speed { get; set; }

        public void Move(float deltaTime);
    }
}
