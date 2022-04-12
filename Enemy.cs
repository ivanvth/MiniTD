using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MiniTD
{
    class Enemy
    {
        static int GlobalID; 
        public int ID { get; private set; } 
        public Vector2 Position { get; private set; }
        Vector2[] route;
        int routeIndex = 0;
        public int Health { get; private set; }
        float speed;
        Animation animation;

        Vector2 minPosition = new Vector2(0, 0);
        Vector2 maxPosition;
        public bool IsTargetable { get; private set; } = false; 

        public Enemy(Vector2 position, int hp, Vector2[] route, Animation animation, int maxX, int maxY)
        {
            this.ID = GlobalID++;
            maxPosition = new Vector2(maxX, maxY);
            this.animation = animation;
            Position = position;
            this.Health = hp;
            this.route = route;
            speed = 0.5f;
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            Move();
            SetTargetable();
        }

        private void SetTargetable()
        {
            if (Position.X > 0 &&
                Position.X < maxPosition.X &&
                Position.Y > 0 &&
                Position.Y < maxPosition.Y)
            {
                IsTargetable = true;
            } else
            {
                IsTargetable = false;
            }
        }
        public Rectangle GetAnimation()
        {
            return animation.GetRectangle();
        }
        private void Move()
        {
            if (routeIndex < route.Length - 1 && 
                Math.Abs(Position.X - (route[routeIndex]).X) < 1f &&
                Math.Abs(Position.Y - (route[routeIndex]).Y) < 1f)
            {
                routeIndex++;
            }

            Vector2 direction = route[routeIndex] - this.Position;
            direction.Normalize();
            this.Position += direction * speed;
        }

    }
}
