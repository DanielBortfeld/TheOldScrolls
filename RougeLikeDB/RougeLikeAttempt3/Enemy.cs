using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougeLikeBase;

namespace RougeLikeAttempt3
{
    public class Enemy
    {
        public char Symbol;

        private int positionX;
        private int positionY;
        private int attackPower = RougeBasics.ItemLifeContainer;
        private int health;

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

        public virtual void Attack(Player player)
        {
            player.Health -= attackPower;
        }
    }
}
