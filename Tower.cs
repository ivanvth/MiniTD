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
            range = 50;
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
        public Bullet Shoot()
        {
            ReadyToShoot = false;
            lastShot = DateTime.Now;
            return new Bullet(this.position, new Vector2(10, 10), 300);
            
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
