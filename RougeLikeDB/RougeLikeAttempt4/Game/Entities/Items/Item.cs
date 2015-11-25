using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game.Entities.Items
{
    abstract class Item : Entity
    {
        public Item(int positionX, int positionY) : base (positionX, positionY)
        {
        }
    }
}
