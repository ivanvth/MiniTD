using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD
{
    class Bullet
    {
        public Vector2 Position { get; private set; }
        Vector2 direction;
        float speed;

        public Bullet(Vector2 position, Vector2 direction, float speed)
        {
            Position = position;
            this.direction = direction;
            this.speed = speed;
        }

        public void Update()
        {
            UpdatePosition();
        }
        private void UpdatePosition()
        {
            float positionX = Position.X;
            float positionY = Position.Y;
            Position = new Vector2(positionX + 0.01f * speed, positionY + 0.01f * speed);
        }
    }
}
