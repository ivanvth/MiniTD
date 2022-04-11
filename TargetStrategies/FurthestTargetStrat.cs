using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD.TargetStrategies
{
    class FurthestTargetStrat : ITargetStrategy
    {
        static FurthestTargetStrat instance = null;
        public static ITargetStrategy GetStaticInstance()
        {
            if (instance is null)
            {
                instance = new FurthestTargetStrat();
            }
            return instance;
        }

        public Enemy GetTarget(List<Enemy> enemies, Vector2 towerPosition, float towerRange)
        {
            float currentDistance = 0;
            Enemy currentTarget = null;
            foreach (Enemy enemy in enemies)
            {
                float newDistance = Vector2.Distance(enemy.Position, towerPosition);
                if (enemy.IsTargetable && newDistance < towerRange && newDistance > currentDistance)
                {
                    currentTarget = enemy;
                    currentDistance = newDistance;
                }
            }
            return currentTarget;
        }
    }
}
