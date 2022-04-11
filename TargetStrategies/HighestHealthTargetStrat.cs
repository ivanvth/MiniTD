using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD.TargetStrategies
{
    class HighestHealthTargetStrat : ITargetStrategy
    {
        static HighestHealthTargetStrat instance = null;
        public static ITargetStrategy GetStaticInstance()
        {
            if (instance is null)
            {
                instance = new HighestHealthTargetStrat();
            }
            return instance;
        }
        public Enemy GetTarget(List<Enemy> enemies, Vector2 towerPosition, float towerRange)
        {
            int currentHighestHealth = 0;
            Enemy currentTarget = null;
            foreach (Enemy enemy in enemies)
            {
                int newHealth = enemy.Health;
                float newDistance = Vector2.Distance(enemy.Position, towerPosition);
                if (enemy.IsTargetable && newDistance < towerRange && newHealth > currentHighestHealth)
                {
                    currentTarget = enemy;
                    currentHighestHealth = newHealth;
                }
            }
            return currentTarget;
        }
    }
}
