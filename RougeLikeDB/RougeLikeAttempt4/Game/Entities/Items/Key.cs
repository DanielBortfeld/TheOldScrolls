using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game.Entities.Items
{
    class Key : Item
    {
        public Key(int positionX, int positionY) : base(positionX, positionY)
        {
            this.Symbol = Symbols.ItemKey;
            this.Color = ConsoleColor.DarkYellow;
        }
    }
}
