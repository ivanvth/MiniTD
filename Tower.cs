using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD
{
    class Tower
    {
        Vector2 position;
        Texture2D towerImage;

        public Tower(int positionX, int positionY, Texture2D towerImage)
        {
            this.position = new Vector2(positionX, positionY);
            this.towerImage = towerImage;
        }

        public Texture2D GetImage()
        {
            return this.towerImage;
        }

        public Vector2 GetPosition()
        {
            return this.position;
        }
    }
}
