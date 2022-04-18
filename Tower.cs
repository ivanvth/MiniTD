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

        public int Range;
        int minDamage;
        int maxDamage;
        public int Damage
        {
            get 
            {
                return (int)((minDamage + maxDamage) / 2);
            }
        }
        DamageType damageType;

        int screenWidth;
        int screenHeight;

        public bool IsActive = false;

        public TimeSpan Cooldown = TimeSpan.FromMilliseconds(2000);
        DateTime lastShot;
        public bool ReadyToShoot { get; private set; }
        public TargetLockType TargetLock;
        ITargetStrategy targetStrategy;

        public Tower(int positionX, int positionY, Texture2D towerImage, int width, int height)
        {
            this.screenWidth = width;
            this.screenHeight = height;
            this.position = new Vector2(positionX, positionY);
            this.towerImage = towerImage;
            damageType = DamageType.Bullet;
            Range = 100;
            minDamage = 90;
            maxDamage = 110;
            TargetLock = TargetLockType.First;
            targetStrategy = TargetStrategies.FirstTargetStrat.GetStaticInstance();
            lastShot = DateTime.Now;
        }

        public void Update(List<Enemy> enemies)
        {
            SetTarget(enemies);
            if (!ReadyToShoot)
            {
                if (DateTime.Now - lastShot >= Cooldown && HasTarget)
                {
                    ReadyToShoot = true;
                }
            }
        }
        public Bullet Shoot(List<Enemy> enemies)
        {
            ReadyToShoot = false;
            lastShot = DateTime.Now;
            Vector2 firePos = new Vector2(this.position.X, this.position.Y - 16);
            return new Bullet(firePos, currentTarget.Position, 5, Range, screenWidth, screenHeight);
        }

        private void SetTarget(List<Enemy> enemies)
        {
            Vector2 targetPoint = new Vector2(this.position.X + 16, this.position.Y + 16);
            
            currentTarget = targetStrategy.GetTarget(enemies, targetPoint, Range);
            HasTarget = currentTarget != null;  
        }

        private void SetTargetStrategy(TargetLockType strat)
        {
            switch (strat)
            {
                case TargetLockType.Nearest:
                    targetStrategy = TargetStrategies.NearestTargetStrat.GetStaticInstance();
                    break;
                case TargetLockType.Furthest:
                    targetStrategy = TargetStrategies.FurthestTargetStrat.GetStaticInstance();
                    break;
                case TargetLockType.HighHP:
                    targetStrategy = TargetStrategies.HighestHealthTargetStrat.GetStaticInstance();
                    break;
                case TargetLockType.LowHP:
                    targetStrategy = TargetStrategies.LowestHealthTargetStrat.GetStaticInstance();
                    break;
                case TargetLockType.First:
                    targetStrategy = TargetStrategies.FirstTargetStrat.GetStaticInstance();
                    break;
                case TargetLockType.Last:
                    targetStrategy = TargetStrategies.LastTargetStrat.GetStaticInstance();
                    break;
            }
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
