using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game.Map.Doors
{
    abstract class Door : MapField
    {
        public Door(int x, int y, bool isWalkable) : base(x, y, isWalkable)
        {
        }
    }
}
