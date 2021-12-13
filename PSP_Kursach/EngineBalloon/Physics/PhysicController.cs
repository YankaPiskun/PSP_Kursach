using EngineBalloon.GameObjects;
using EngineBalloon.GameObjects.Bullets;
using EngineBalloon.GameObjects.Prizes;
using EngineBalloon.Physics.Interfaces;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace EngineBalloon.Physics
{
    internal static class PhysicController
    {
        public static Vector2i SizeWindow { get; set; }

        static PhysicController()
        {
            Gravity = new Vector2(0.0f, -0.02f);
        }

        public static Vector2 Gravity { get; set; }

        public static void Collision(Player player, List<GameObject> gameObjects)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if(IsCollision(player, gameObjects[i]) && player != gameObjects[i])
                {
                    switch (gameObjects[i])
                    {
                        case Bullet bullet:
                            if(player != bullet.Parent)
                            {
                                player.TakeDamage(bullet.Damage);
                                gameObjects.Remove(gameObjects[i]);
                                i--;
                            }
                            break;
                        case Prize prize:
                            prize.UsePrize(player);
                            gameObjects.Remove(gameObjects[i]);
                            i--;
                            break;
                        case Player prize:
                            player.Direction = Vector2.Zero;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static bool IsCollision(GameObject firstObj, GameObject secondObj)
        {
            return Vector2.Distance(firstObj.Position, secondObj.Position) 
                * SizeWindow.EuclideanLength < firstObj.Radius + secondObj.Radius;
        }

        public static void UseGravity(List<GameObject> gameObjects, float deltaTime)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Direction += Gravity * gameObject.GravityScale * deltaTime;
            }
        }
    }
}
