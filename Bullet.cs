using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD
{
    class Bullet
    {
        public Vector2 Position { get; private set; }
        Vector2 startingPosition;
        Vector2 direction;

        float minX = 0;
        float minY = 0;
        float maxX;
        float maxY;

        public bool ReadyForDeletion { get; set; } = false;
        float speed;
        float range;

        public Bullet(Vector2 position, Vector2 targetPosition, float speed, float range, int width, int height)
        {
            Position = position;
            startingPosition = position;
            this.direction = targetPosition - Position;
            direction.Normalize();
            this.speed = speed;
            this.range = range;
            this.maxX = width;
            this.maxY = height;
        }

        public bool Update()
        {
            float distanceTravelled = Vector2.Distance(startingPosition, Position);
            if (distanceTravelled > range || Position.X < minX || Position.X > maxX || Position.Y < minY || Position.Y > maxY)
            {
                return false;
            }
            UpdatePosition();
            return true;
        }
        private void UpdatePosition()
        {
            Position += direction * speed;
        }
    }
}
