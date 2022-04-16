using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD
{
    class GUIHandlerRight
    {
        SpriteFont font;
        string bullets;
        string enemies;
        string level;
        string gold;

        string activeTowerNumber;
        string activeTowerDamage;
        string activeTowerCooldown;
        string activeTowerRange;
        string activeTowerLockType;

        Vector2[] positions = new Vector2[]
        {
            new Vector2(650, 20),
            new Vector2(650, 30),
            new Vector2(650, 40),
            new Vector2(650, 50),
            new Vector2(650, 60),
            new Vector2(650, 70),
            new Vector2(650, 80),
            new Vector2(650, 90),
            new Vector2(650, 100),
            new Vector2(650, 110),
            new Vector2(650, 120),
            new Vector2(650, 130),
            new Vector2(650, 140),
        };
        public GUIHandlerRight(SpriteFont font)
        {
            this.font = font;
        }

        public void Update(Tower tower, int towerNumber, int levelInt, int bulletInt, int enemyInt, Player player)
        {
            level = $"Level: {levelInt.ToString()}";
            bullets = $"Bullets: {bulletInt.ToString()}";
            enemies = $"Enemies: {enemyInt.ToString()}";
            gold = $"Gold: {player.Gold.ToString()}";

            activeTowerNumber = $"Tower #{towerNumber.ToString()}";
            activeTowerDamage = $"Damage: {tower.Damage.ToString()}";
            activeTowerCooldown = $"Cooldown: {(tower.Cooldown.TotalMilliseconds/1000):F2} secs";
            activeTowerRange = $"Range: {tower.Range.ToString()}";
            activeTowerLockType = $"Target: {tower.TargetLock.ToString()}";
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, level, new Vector2(10, 10), Color.Red);
            spriteBatch.DrawString(font, bullets, new Vector2(10, 20), Color.Red);
            spriteBatch.DrawString(font, enemies, new Vector2(10, 30), Color.Red);
            spriteBatch.DrawString(font, gold, new Vector2(10, 40), Color.Red);

            spriteBatch.DrawString(font, activeTowerNumber, positions[0], Color.Black);
            spriteBatch.DrawString(font, activeTowerDamage, positions[2], Color.Black);
            spriteBatch.DrawString(font, activeTowerCooldown, positions[3], Color.Black);
            spriteBatch.DrawString(font, activeTowerRange, positions[4], Color.Black);
            spriteBatch.DrawString(font, activeTowerLockType, positions[5], Color.Black);
        }
    }
}
