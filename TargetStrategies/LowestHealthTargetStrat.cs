using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD.TargetStrategies
{
    class LowestHealthTargetStrat : ITargetStrategy
    {
        static LowestHealthTargetStrat instance = null;
        public static ITargetStrategy GetStaticInstance()
        {
            if (instance is null)
            {
                instance = new LowestHealthTargetStrat();
            }
            return instance;
        }
        public Enemy GetTarget(List<Enemy> enemies, Vector2 towerPosition, float towerRange)
        {
            int currentLowestHealth = int.MaxValue;
            Enemy currentTarget = null;
            foreach (Enemy enemy in enemies)
            {
                int newHealth = enemy.Health;
                float newDistance = Vector2.Distance(enemy.Position, towerPosition);
                if (enemy.IsTargetable && newDistance < towerRange && newHealth < currentLowestHealth)
                {
                    currentTarget = enemy;
                    currentLowestHealth = newHealth;
                }
            }
            return currentTarget;
        }
    }
}
