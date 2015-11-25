using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game.Map.Legend
{
    class LegendField : MapField
    {
        public LegendField(int x, int y) : base(x, y, false)
        {
            this.Color = ConsoleColor.White;
            this.Symbol = ' ';
        }

        public override void Draw()
        {
            switch (this.Symbol)
            {
                case Symbols.PlayerSymbol:
                    this.Color = ConsoleColor.Cyan;
                    break;
                case Symbols.ItemKey:
                    this.Color = ConsoleColor.DarkYellow;
                    break;
                case Symbols.ItemGold:
                    this.Color = ConsoleColor.Yellow;
                    break;
                case Symbols.ItemLifeContainer:
                    this.Color = ConsoleColor.Red;
                    break;
                case Symbols.DoorLocked:
                    this.Color = ConsoleColor.DarkBlue;
                    break;
                case Symbols.DoorUnlocked:
                    this.Color = ConsoleColor.Blue;
                    break;
                case Symbols.DoorOpen:
                    this.Color = ConsoleColor.DarkGray;
                    break;
                default:
                    this.Color = ConsoleColor.White;
                    break;
            }

            base.Draw();
        }
    }
}
