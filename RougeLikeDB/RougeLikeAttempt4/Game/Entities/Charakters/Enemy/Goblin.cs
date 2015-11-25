using System;

namespace RougeLikeAttempt4
{
    class Goblin : Enemy
    {
        public Goblin(int positionX, int positionY) : base(positionX, positionY)
        {
            this.Health = 3;
            this.attackStrength = 2;
            this.Initiative = Symbols.random.Next(2, 7);

            this.Name = "Goblin";

            this.Symbol = 'G';
            this.Color = ConsoleColor.Green;
        }
    }
}
