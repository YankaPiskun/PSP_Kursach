using EngineBalloon.GameObjects;
using EngineBalloon.GameObjects.Bullets;
using EngineBalloon.GameObjects.Prizes;
using EngineBalloon.GameObjects.Prizes.Creators;
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
                            PrizeCreator.PrizeCount--;
                            prize.UsePrize(player);
                            gameObjects.Remove(gameObjects[i]);
                            i--;
                            break;
                        case Player:
                            player.Direction = Vector2.Zero;
                            break;
                        default:
                            break;
                    }
                }
                if (gameObjects[i] is Bullet _bullet && 
                    (MathHelper.Abs(_bullet.X) > 1.0f || MathHelper.Abs(_bullet.Y) > 1.0f))
                {
                    gameObjects.Remove(gameObjects[i]);
                    i--;
                }
                if (gameObjects[i] is Earth)
                {
                    if (player.Y < ((Earth)gameObjects[i]).DeadY) player.HP = 0;
                }
                if (gameObjects[i] is Wind wind && wind.Active)
                {
                    if (MathHelper.Abs(player.Y - wind.Position.Y) < 0.1f) 
                        player.Direction = new Vector2(wind.FlipY ? -wind.Speed : wind.Speed, player.Direction.Y);
                }
            }
            if (player.Position.X >= 1.0f)
                player.Direction = new Vector2(-0.001f, player.Direction.Y);
            if (player.Position.X <= -1.0f)
                player.Direction = new Vector2(0.001f, player.Direction.Y);
            if (player.Position.Y >= 1.0f)
                player.Direction = new Vector2(player.Direction.X, -0.001f);
            if (player.Position.Y <= -1.0f)
                player.Direction = new Vector2(player.Direction.X, 0.001f);
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
