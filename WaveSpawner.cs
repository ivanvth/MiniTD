using Microsoft.Xna.Framework;
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
        List<Enemy> spawnedEnemies = new List<Enemy>();
        public int Level { get; private set; }
        GameTime startTime;
        GameTime elapsedTime;
        float spawnTimer;
        float spawnThreshold;
        float levelTimer;
        float levelThreshold;

        public WaveSpawner(Vector2[] route, Animation animation, int maxX, int maxY)
        {
            spawnTimer = 0f;
            spawnThreshold = 1;
            levelTimer = 0f;
            levelThreshold = 30;
            this.maxX = maxX;
            this.maxY = maxY;
            this.route = route;
            this.animation = animation;
            this.Level = 1;
        }

        private void Spawn()
        {
            spawnedEnemies.Add(new Enemy(route[0], 100, route[1..], animation, maxX, maxY));
        }

        public List<Enemy> GetEnemies()
        {
            List<Enemy> enemies = this.spawnedEnemies;
            spawnedEnemies = new List<Enemy>();
            return enemies;
        }

        public void Update(GameTime gameTime)
        {
            levelTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (levelTimer > levelThreshold)
            {
                Level++;
                levelTimer = 0;
                spawnTimer = 0;
            } 
            else if (spawnTimer > spawnThreshold)
            {
                Spawn();
                spawnTimer = 0;
            } 
        }
    }
}
