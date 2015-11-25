using System;

namespace RougeLikeAttempt4
{
    class GiantRat : Enemy
    {
        public GiantRat(int positionX, int positionY) : base(positionX, positionY)
        {
            this.Health = 1;
            this.attackStrength = 1;
            this.Initiative = Symbols.random.Next(3, 10);

            this.Name = "GiantRat";

            this.Symbol = 'R';
            this.Color = ConsoleColor.DarkGray;
        }
    }
}
