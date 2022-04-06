using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD
{
    class Tower
    {
        Vector2 position;

        public Tower(int positionX, int positionY)
        {
            this.position = new Vector2(positionX, positionY);
        }
    }
}
