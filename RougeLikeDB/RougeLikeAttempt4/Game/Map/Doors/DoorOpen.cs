using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game.Map.Doors
{
    class DoorOpen : Door
    {
        public DoorOpen(int x, int y) : base(x, y, true)
        {
            this.Symbol = Symbols.DoorOpen;
            this.Color = ConsoleColor.DarkGray;
        }
    }
}
