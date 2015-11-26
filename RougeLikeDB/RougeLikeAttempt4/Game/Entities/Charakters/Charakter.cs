using System;
using System.Collections.Generic;
using RougeLikeAttempt4.Game.Entities;
using System.Diagnostics;
using RougeLikeAttempt4.Game;

namespace RougeLikeAttempt4
{
    abstract class Character : Entity
    {
        public int Health;
        public int Initiative;

        private int lastPositionX;
        private int lastPositionY;

        protected int attackStrength;

        public Character(int positionX, int positionY) : base(positionX, positionY)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

        public virtual void Attack(Character otherCharakter)
        {
            otherCharakter.Health -= attackStrength;
        }

        public virtual void CastSpellAoE(int damage)
        {
            GameManager.ClearSubtext();
            GameManager.WriteSubtext(0, "Casting AoE spell around you.");

            for (int y = this.PositionY-1; y < this.PositionY+2; y++)
                for (int x = this.PositionX-1; x < this.PositionX+2; x++)
                    foreach (var entity in GameManager.Entities)
                    {
                        if (entity == this)
                            continue;

                        if (entity is Character)
                        {
                            if (entity.PositionY == y && entity.PositionX == x)
                                ((Character)entity).Health -= damage;

                            if (((Character)entity).Health <= 0)
                                GameManager.DeclareAsDefeated((Character)entity);
                        }
                    }
        }

        public virtual void Move(int directionX, int directionY)
        {
            if (this is Player && TestingDevice.collisionIsEnabled)
                if (!GameManager.IsWalkable(this.PositionX + directionX, this.PositionY + directionY))
                    return;

            lastPositionX = this.PositionX;
            lastPositionY = this.PositionY;

            this.PositionX += directionX;
            this.PositionY += directionY;
        }

        public override void Draw()
        {
            base.Draw();

            if (lastPositionX >= 0 && lastPositionX < Map.MapWidth)
                if (lastPositionY >= 0 && lastPositionY < Map.MapHeight)
                    GameManager.DrawCurrentField(lastPositionX, lastPositionY);

            Debug.Print("{2} drawn at: X: {0} Y: {1}", PositionX, PositionY, Name);
        }
    }
}
