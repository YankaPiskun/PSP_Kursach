using EngineBalloon.GameObjects;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace EngineBalloon.Physics
{
    internal static class PhysicController
    {
        static PhysicController()
        {
            Gravity = new Vector2(0.0f, -0.02f);
        }

        public static Vector2 Gravity { get; set; }



        public static void UseGravity(List<GameObject> gameObjects, float deltaTime)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Position += Gravity * gameObject.GravityScale * deltaTime;
            }
        }
    }
}
