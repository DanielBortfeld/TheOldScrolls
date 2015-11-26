using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game.Entities
{
    public abstract class Entity : GameObject
    {
        public string Name;

        public Entity(int positionX, int positionY) : base(positionX, positionY)
        {
        }
    }
}
