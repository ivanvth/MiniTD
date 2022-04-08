using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD
{
    class Animation
    {
        Texture2D source;
        Rectangle[] rectangles;
        Rectangle currentRectangle;
        float timer;
        int threshold;
        int index = 0;

        public Animation(Texture2D source)
        {
            timer = 0;
            threshold = 250;
            this.source = source;
            int cols = source.Width / 16;
            int rows = source.Height / 16;
            rectangles = new Rectangle[cols * rows];
            for (int row=0; row<rows; row++)
            {
                for (int col=0; col<cols; col++)
                {
                    int i = row * cols + col;
                    rectangles[i] = new Rectangle(col * 16, row * 16, 16, 16);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (timer > threshold)
            {
                index++;
                if (index >= rectangles.Length)
                {
                    index = 0;
                }
                timer = 0;
            }
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            currentRectangle = rectangles[index];
        }
        public Rectangle GetRectangle()
        {
            return currentRectangle;
        }
    }
}
