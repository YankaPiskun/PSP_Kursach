using EngineBalloon.Graphics;
using Network.Models;
using OpenTK.Mathematics;
using System;

namespace EngineBalloon.GameObjects.Prizes.Creators
{
    public abstract class PrizeCreator
    {
        protected Sprite _sprite;

        public static int MaxPrize { get; set; }

        public static int PrizeCount { get; set; }

        public static Vector2i SizeWindow { get; set; }

        public static PrizeCreator[] Creators { get; set; }

        public Sprite Sprite => _sprite;

        public PrizeCreator(Sprite sprite)
        {
            _sprite = sprite;
        }

        public abstract Prize GetPrize(Vector2 position);

        public static Prize GetPrize(PrizeModel prizeModel)
        {
            PrizeCount++;
            var prize = Creators[prizeModel.Id].GetPrize(new Vector2(prizeModel.X, prizeModel.Y));
            prize.ResizeByWindow(SizeWindow.X, SizeWindow.Y);
            return prize;
        }

        public static Prize GetPrize(out PrizeModel prizeModel)
        {
            var rnd = new Random(DateTime.Now.Millisecond);

            prizeModel = new PrizeModel
            {
                Id = rnd.Next(Creators.Length),
                X = (float)rnd.NextDouble() * 2 - 1.0f,
                Y = (float)rnd.NextDouble() * 1.4f - 0.7f,
            };

            return GetPrize(prizeModel);
        }
    }
}
