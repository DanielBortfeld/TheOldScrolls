using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4.Game
{
    public abstract class GameObject
    {
        private int positionX;
        private int positionY;
        private char symbol;
        private ConsoleColor color;

        public int PositionX
        {
            get { return positionX; }
            set { positionX = value; }
        } //<- quatsch

        public int PositionY
        {
            get { return positionY; }
            set { positionY = value; }
        }

        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public ConsoleColor Color
        {
            get { return color; }
            set { color = value; }
        }

        public GameObject(int x, int y)
        {
            this.PositionX = x;
            this.PositionY = y;
        }

        public virtual void Draw()
        {
            ConsoleUtilities.WriteColoredAtPosition(PositionX, PositionY, Symbol, Color);
        }
    }
}
