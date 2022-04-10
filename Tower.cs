using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MiniTD
{
    class Tower
    {
        Vector2 position;
        Texture2D towerImage;

        float range;
        int minDamage;
        int maxDamage;
        DamageType damageType;

        TimeSpan cooldown = TimeSpan.FromSeconds(1);
        DateTime lastShot;
        public bool ReadyToShoot { get; private set; }
        TargetLockType targetLock;

        public Tower(int positionX, int positionY, Texture2D towerImage)
        {
            this.position = new Vector2(positionX, positionY);
            this.towerImage = towerImage;
            damageType = DamageType.Bullet;
            range = 150;
            minDamage = 90;
            maxDamage = 110;
            targetLock = TargetLockType.Nearest;
            lastShot = DateTime.Now;
        }

        public void Update()
        {
            if (!ReadyToShoot)
            {
                if (DateTime.Now - lastShot >= cooldown)
                {
                    ReadyToShoot = true;
                }
            }
        }
        public Bullet Shoot(List<Enemy> enemies)
        {
            ReadyToShoot = false;
            lastShot = DateTime.Now;
            Vector2 targetPosition = GetTarget(enemies).Position;
            return new Bullet(this.position, targetPosition, 3, range);
            
        }

        private Enemy GetTarget(List<Enemy> enemies)
        {
            if (enemies.Count != 0)
            {
                Enemy currentTarget = enemies[0];
                float currentDistance = Vector2.Distance(currentTarget.Position, position);
                for (int i=1; i<enemies.Count; i++)
                {
                    float newDistance = Vector2.Distance(enemies[i].Position, this.position);
                    if (newDistance < range && newDistance < currentDistance)
                    {
                        currentTarget = enemies[i];
                        currentDistance = newDistance;
                    }
                }
                return currentTarget;
            }
            return null;
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
