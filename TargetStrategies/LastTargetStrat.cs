using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD.TargetStrategies
{
    class LastTargetStrat : ITargetStrategy
    {
        static LastTargetStrat instance = null;
        public static ITargetStrategy GetStaticInstance()
        {
            if (instance is null)
            {
                instance = new LastTargetStrat();
            }
            return instance;
        }

        public Enemy GetTarget(List<Enemy> enemies, Vector2 towerPosition, float towerRange)
        {
            int currentHighestID = int.MinValue;
            Enemy currentTarget = null;
            foreach (Enemy enemy in enemies)
            {
                int newID = enemy.ID;
                float newDistance = Vector2.Distance(enemy.Position, towerPosition);
                if (enemy.IsTargetable && newDistance < towerRange && newID > currentHighestID)
                {
                    currentTarget = enemy;
                    currentHighestID = newID;
                }
            }
            return currentTarget;
        }
    }
}

