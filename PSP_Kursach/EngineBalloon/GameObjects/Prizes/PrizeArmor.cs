using EngineBalloon.Graphics;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineBalloon.GameObjects.Prizes
{
    public class PrizeArmor : Prize
    {
        public PrizeArmor(Sprite sprite, Vector2 position = default, float angle = 0) 
            : base(sprite, position, angle)
        {
        }

        public override void UsePrize(Player player)
        {
            player.Armor++;
        }
    }
}
