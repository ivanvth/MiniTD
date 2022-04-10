﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD
{
    class WaveSpawner
    {
        Vector2[] route;
        Animation animation;
        int maxX;
        int maxY;

        public WaveSpawner(Vector2[] route, Animation animation, int maxX, int maxY)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.route = route;
            this.animation = animation;
        }

        public List<Enemy> SpawnWave(int level)
        {
            return new List<Enemy>
            {
                new Enemy(new Vector2(192, -10), 10, route[1..], animation, maxX, maxY),
                new Enemy(new Vector2(192, -20), 10, route[1..], animation, maxX, maxY),
                new Enemy(new Vector2(192, -30), 10, route[1..], animation, maxX, maxY),
                new Enemy(new Vector2(192, -40), 10, route[1..], animation, maxX, maxY)
            };
        }
    }
}