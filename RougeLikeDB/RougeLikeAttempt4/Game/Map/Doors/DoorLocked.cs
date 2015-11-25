using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game.Map.Doors
{
    class DoorLocked : Door
    {
        public DoorLocked(int x, int y) : base(x, y, false)
        {
            this.Symbol = Symbols.DoorLocked;
            this.Color = ConsoleColor.DarkBlue;

            Symbols.lockedDoorCounter++;
        }
    }
}
