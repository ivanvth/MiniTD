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

        Enemy currentTarget;
        public bool HasTarget { get; private set; }

        float range;
        int minDamage;
        int maxDamage;
        DamageType damageType;

        int screenWidth;
        int screenHeight;

        public bool IsActive = false;

        TimeSpan cooldown = TimeSpan.FromSeconds(1);
        DateTime lastShot;
        public bool ReadyToShoot { get; private set; }
        TargetLockType targetLock;
        ITargetStrategy targetStrategy;

        public Tower(int positionX, int positionY, Texture2D towerImage, int width, int height)
        {
            this.screenWidth = width;
            this.screenHeight = height;
            this.position = new Vector2(positionX, positionY);
            this.towerImage = towerImage;
            damageType = DamageType.Bullet;
            range = 150;
            minDamage = 90;
            maxDamage = 110;
            targetLock = TargetLockType.Nearest;
            targetStrategy = NearestTargetStrat.GetStaticInstance();
            lastShot = DateTime.Now;
        }

        public void Update(List<Enemy> enemies)
        {
            SetTarget(enemies);
            if (!ReadyToShoot)
            {
                if (DateTime.Now - lastShot >= cooldown && HasTarget)
                {
                    ReadyToShoot = true;
                }
            }
        }
        public Bullet Shoot(List<Enemy> enemies)
        {
            ReadyToShoot = false;
            lastShot = DateTime.Now;
            return new Bullet(this.position, currentTarget.Position, 3, range, screenWidth, screenHeight);
        }

        private void SetTarget(List<Enemy> enemies)
        {
            currentTarget = targetStrategy.GetTarget(enemies, this.position, range);
            HasTarget = currentTarget != null;
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
