using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD
{
    class Bullet
    {
        public Vector2 Position { get; private set; }
        private Vector2 StartingPosition;
        Vector2 targetPosition;
        Vector2 Direction;
        float speed;
        float range;

        public Bullet(Vector2 position, Vector2 targetPosition, float speed, float range)
        {
            Position = position;
            StartingPosition = position;
            this.targetPosition = targetPosition;
            this.Direction = targetPosition - Position;
            Direction.Normalize();
            this.speed = speed;
            this.range = range;
        }

        public bool Update()
        {
            if (Vector2.Distance(StartingPosition, Position) > range)
            {
                return false;
            }
            UpdatePosition();
            return true;
        }
        private void UpdatePosition()
        {
            Position += Direction * speed;
        }
    }
}
