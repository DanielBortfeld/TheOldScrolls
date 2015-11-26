using RougeLikeAttempt4.Game.Map.Doors;
using System;

namespace RougeLikeAttempt4
{
    abstract class Enemy : Character
    {
        public Enemy(int positionX, int positionY) : base(positionX, positionY)
        {
        }

        public void MoveRandomly()
        {
            int direction = Symbols.random.Next(4);

            switch (direction)
            {
                case 0:
                    if (CheckForDoor(-1, 0))
                        Move(-1, 0);
                    break;
                case 1:
                    if (CheckForDoor(0, 1))
                        Move(0, 1);
                    break;
                case 2:
                    if (CheckForDoor(1, 0))
                        Move(1, 0);
                    break;
                case 3:
                    if (CheckForDoor(0, -1))
                        Move(0, -1);
                    break;
                default:
                    break;
            }
        }

        private bool CheckForDoor(int directionX, int directionY)
        {
            if (GameManager.currentMap.map[this.PositionX + directionX, this.PositionY + directionY] is Door)
                return false;
            return true;
        }

        public override void Move(int directionX, int directionY)
        {
            if (!GameManager.IsWalkable(this.PositionX + directionX, this.PositionY + directionY))
                    return;

            base.Move(directionX, directionY);
        }
    }
}
