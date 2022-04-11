using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD
{
    interface ITargetStrategy
    {
        Enemy? GetTarget(List<Enemy> enemies, Vector2 towerPosition, float towerRange);
    }
}
