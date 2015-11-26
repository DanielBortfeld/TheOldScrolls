using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game.Entities.Items
{
    class Gold : Item
    {
        public Gold(int positionX, int positionY) : base(positionX, positionY)
        {
            this.Symbol = Symbols.ItemGold;
            this.Color = ConsoleColor.Yellow;

            this.Name = "Gold";
        }
    }
}
