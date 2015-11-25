using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game.Map
{
    class Floor : MapField
    {
        public Floor(int x, int y) : base(x, y, true)
        {
            this.Symbol = Symbols.EmptyField;
            this.Color = ConsoleColor.DarkGreen;
        }
    }
}
