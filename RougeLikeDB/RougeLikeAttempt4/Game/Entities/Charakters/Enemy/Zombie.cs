using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4
{
    class Zombie : Enemy
    {
        public Zombie(int positionX, int positionY) : base(positionX, positionY)
	    {
            this.Health = 5;
            this.attackStrength = 2;
            this.Initiative = Symbols.random.Next(1, 5);

            this.Name = "Zombie";

            this.Symbol = 'Z';
            this.Color = ConsoleColor.DarkMagenta;
        }
    }
}
