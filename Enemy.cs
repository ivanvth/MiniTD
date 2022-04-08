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
        public Vector2 Position { get; private set; }
        Vector2[] route;
        int routeIndex = 0;
        int hp;
        float speed;
        Animation animation;

        public Enemy(Vector2 position, int hp, Vector2[] route, Animation animation)
        {
            this.animation = animation;
            Position = position;
            this.hp = hp;
            this.route = route;
            speed = 0.5f;
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            Move();
        }
        public Rectangle GetAnimation()
        {
            return animation.GetRectangle();
        }
        private void Move()
        {
            if (routeIndex < route.Length - 1 && 
                Math.Abs(Position.X - (route[routeIndex]).X) < 0.1f &&
                Math.Abs(Position.Y - (route[routeIndex]).Y) < 0.1f)
            {
                routeIndex++;
            }

            Vector2 direction = route[routeIndex] - this.Position;
            direction.Normalize();
            this.Position += direction * speed;
        }
    }
}
