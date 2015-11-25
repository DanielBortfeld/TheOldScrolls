using RougeLikeAttempt4.Game.Entities;
using RougeLikeAttempt4.Game.Entities.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game.Entities.Cake
{
    class Cake : Item
    {
        public Cake(int positionX, int positionY) : base(positionX, positionY)
        {
            this.Symbol = 'O';
            this.Color = ConsoleColor.White;
        }
    }
}
