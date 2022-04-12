using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD.TargetStrategies
{
    class FirstTargetStrat : ITargetStrategy
    {
        static FirstTargetStrat instance = null;
        public static ITargetStrategy GetStaticInstance()
        {
            if (instance is null)
            {
                instance = new FirstTargetStrat();
            }
            return instance;
        }

        public Enemy GetTarget(List<Enemy> enemies, Vector2 towerPosition, float towerRange)
        {
            int currentLowestID = int.MaxValue;
            Enemy currentTarget = null;
            foreach (Enemy enemy in enemies)
            {
                int newID = enemy.ID;
                float newDistance = Vector2.Distance(enemy.Position, towerPosition);
                if (enemy.IsTargetable && newDistance < towerRange && newID < currentLowestID)
                {
                    currentTarget = enemy;
                    currentLowestID = newID;
                }
            }
            return currentTarget;
        }
    }
}
