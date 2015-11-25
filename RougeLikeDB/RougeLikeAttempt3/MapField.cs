using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt3
{
    public class MapField
    {
        public bool IsPlayer;
        public bool IsEnemy;
        public bool IsWalkable;
        public bool IsCollectable;

        public char Symbol;

        private int positionX;
        private int positionY;

        public int PositionX
        {
            get { return positionX; }
            set { positionX = value; }
        }

        public int PositionY
        {
            get { return positionY; }
            set { positionY = value; }
        }

        public MapField(char symbol)
        {
            this.Symbol = symbol;
        }
    }
}
