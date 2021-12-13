using EngineBalloon.Graphics;
using Network.Models;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace EngineBalloon.GameObjects
{
    public class Wind : GameObject
    {
        private static Random _random;

        public static Wind[] Winds { get; set; }

        public bool Active { get; set; }

        public float Speed { get; set; }

        static Wind()
        {
            _random = new Random(DateTime.Now.Millisecond);
            Winds = new Wind[8];
        }

        public Wind(Sprite sprite, Vector2 position = default, float angle = 0) 
            : base(sprite, position, angle)
        {
            Scale = new Vector2(1.0f, 0.1f);
        }

        public static List<WindModel> Update()
        {
            var list = new List<WindModel>();
            foreach (var wind in Winds)
            {
                if (wind.Active)
                {
                    if (_random.NextDouble() < 0.0005)
                        wind.Active = false;
                }
                else
                {
                    if (_random.NextDouble() < 0.0001)
                    {
                        wind.Active = true;
                        wind.FlipY = _random.Next(2) > 0;
                        wind.Speed = (float)_random.NextDouble() * 0.005f + 0.001f;
                    }
                }
                list.Add(new WindModel { Active = wind.Active, Flip = wind.FlipY, Speed = wind.Speed });
            }
            return list;
        }

        public static void Update(List<WindModel> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Winds[i].Active = list[i].Active;
                Winds[i].FlipY = list[i].Flip;
                Winds[i].Speed = list[i].Speed;
            }
        }


        public override void Draw()
        {
            if(Active) base.Draw();
        }
    }
}
