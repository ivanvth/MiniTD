using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD
{
    class Enemy
    {
        Vector2 Position { get; set; }
        int hp;

        public Enemy(Vector2 position, int hp)
        {
            Position = position;
            this.hp = hp;
        }
    }
}
