using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game.Map
{
    class Wall : MapField
    {
        public enum WallType { Horizontal, Vertical, CornerUpperLeft, CornerUpperRight, CornerLowerLeft, CornerLowerRight, Cave }

        public Wall(int x, int y, WallType wallType) : base(x, y, false)
        {
            if (wallType == WallType.Horizontal)
                this.Symbol = Symbols.WallHorizontal;
            if (wallType == WallType.Vertical)
                this.Symbol = Symbols.WallVertical;
            if (wallType == WallType.CornerUpperLeft)
                this.Symbol = Symbols.WallCornerUpperLeft;
            if (wallType == WallType.CornerUpperRight)
                this.Symbol = Symbols.WallCornerUpperRight;
            if (wallType == WallType.CornerLowerLeft)
                this.Symbol = Symbols.WallCornerLowerLeft;
            if (wallType == WallType.CornerLowerRight)
                this.Symbol = Symbols.WallCornerLowerRight;

            if (wallType == WallType.Cave)
                this.Symbol = '#';

            this.Color = ConsoleColor.DarkRed;
        }
    }
}
