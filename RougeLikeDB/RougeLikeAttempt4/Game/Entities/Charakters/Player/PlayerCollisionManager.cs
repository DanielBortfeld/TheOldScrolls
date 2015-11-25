using RougeLikeAttempt4.Game.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeAttempt4
{
    static class PlayerCollisionManager
    {
        public delegate void EntityCollisionEventHandler(Entity entity);
        public static event EntityCollisionEventHandler OnEntityCollision;

        public static void EntityCollision(Entity entity)
        {
            if (OnEntityCollision != null)
                OnEntityCollision(entity);
        }

        public static void CheckCollision(Character character)
        {
            foreach (var entity in GameManager.Entities)
            {
                if (entity is Character)
                    Debug.Print("{2}: X: {0} Y: {1}", entity.PositionX, entity.PositionY, ((Character)entity).Name);

                if (entity == character)
                    continue;

                if (entity.PositionX == character.PositionX && entity.PositionY == character.PositionY)
                    EntityCollision(entity);
            }
        }
    }
}
