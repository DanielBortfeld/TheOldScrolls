using RougeLikeAttempt4.Game;
using System;
using System.Collections.Generic;

namespace RougeLikeAttempt4
{
    public abstract class MapField : GameObject
    {
        private bool isWalkable;
        
        public bool IsWalkable
        {
            get { return isWalkable; }
            set { isWalkable = value; }
        }

        public MapField(int positionX, int positionY, bool isWalkable) : base(positionX, positionY)
        {
            this.IsWalkable = isWalkable;
        }
    }
}
