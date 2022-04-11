using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD
{
    class NearestTargetStrat : ITargetStrategy
    {
        static NearestTargetStrat instance = null;
        public static ITargetStrategy GetStaticInstance()
        {
            if (instance is null)
            {
                instance = new NearestTargetStrat();
            }
            return instance;
        }

        public Enemy GetTarget(List<Enemy> enemies, Vector2 towerPosition, float towerRange)
        {
            float currentDistance = float.MaxValue;
            Enemy currentTarget = null;
            foreach (Enemy enemy in enemies)
            {
                float newDistance = Vector2.Distance(enemy.Position, towerPosition);
                if (enemy.IsTargetable && newDistance < towerRange && newDistance < currentDistance)
                {
                    currentTarget = enemy;
                    currentDistance = newDistance;
                }
            }
            return currentTarget;
        }
    }
}
