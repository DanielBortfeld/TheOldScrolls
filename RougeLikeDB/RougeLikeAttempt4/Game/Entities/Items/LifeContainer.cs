using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game.Entities.Items
{
    class LifeContainer : Item
    {
        public LifeContainer(int positionX, int positionY) : base(positionX, positionY)
        {
            this.Symbol = Symbols.ItemLifeContainer;
            this.Color = ConsoleColor.Red;

            this.Name = "Life Container";
        }
    }
}
