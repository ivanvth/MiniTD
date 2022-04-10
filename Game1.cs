using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace MiniTD
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bulletTexture2D;
        Texture2D squareTexture2D;
        Texture2D squareNorthEastTexture;
        Texture2D squareNorthSouthTexture;
        Texture2D squareNorthWestTexture;
        Texture2D squareEastSouthTexture;
        Texture2D squareEastWestTexture;
        Texture2D squareSouthWestTexture;

        int level = 1;

        Texture2D[] towerTexture2Ds = new Texture2D[1];
        int offSetX = 0;

        List<Bullet> bullets = new List<Bullet>();
        Tower[] towers = new Tower[5];
        Vector2[] towerPositions = new Vector2[5]
        {
            new Vector2(192, 96),
            new Vector2(160, 128),
            new Vector2(192, 128),
            new Vector2(224, 128),
            new Vector2(192, 160)
        };
        WaveSpawner waveSpawner;
        Texture2D enemyTexture;
        List<Enemy> enemies;
        Vector2[] enemyRoute;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.ApplyChanges();

            base.Initialize();
            

            for (int i = 0; i < towers.Length; i++)
            {
                Vector2 position = towerPositions[i];
                towers[i] = new Tower((int)position.X + offSetX, (int)position.Y, towerTexture2Ds[0]);
            }

            enemyRoute = new Vector2[]
            {
                new Vector2(128 + offSetX, -10),
                new Vector2(128 + offSetX, 0),
                new Vector2(128 + offSetX, 10),
                new Vector2(128 + offSetX, 100),
                new Vector2(64 + offSetX, 100)
            };

            waveSpawner = new WaveSpawner(enemyRoute, new Animation(enemyTexture));
            enemies = waveSpawner.SpawnWave(level);

            

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            squareTexture2D = Content.Load<Texture2D>("square_002");
            squareNorthEastTexture = Content.Load<Texture2D>("squareNorthEast_002");
            squareNorthSouthTexture = Content.Load<Texture2D>("squareNorthSouth_002");
            squareNorthWestTexture = Content.Load<Texture2D>("squareNorthWest_002");
            squareEastSouthTexture = Content.Load<Texture2D>("squareEastSouth_002");
            squareEastWestTexture = Content.Load<Texture2D>("squareEastWest_002");
            squareSouthWestTexture = Content.Load<Texture2D>("squareSouthWest_002");

            enemyTexture = Content.Load<Texture2D>("mob1_003");

            towerTexture2Ds[0] = Content.Load<Texture2D>("tower_005");
            bulletTexture2D = Content.Load<Texture2D>("Bullet_001");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Tower tower in towers)
            {
                tower.Update();
                if (tower.ReadyToShoot)
                {
                    bullets.Add(tower.Shoot(enemies));
                }
            }
            List<Bullet> bulletsToDelete = new List<Bullet>();
            foreach (Bullet bullet in bullets)
            {
                if (!bullet.Update())
                {
                    bulletsToDelete.Add(bullet);
                }
            }
            foreach (Bullet bullet in bulletsToDelete)
            {
                bullets.Remove(bullet);
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            DrawPath();
            DrawTowers();
            DrawEnemies();
            DrawBullets();
            _spriteBatch.End();


            base.Draw(gameTime);
        }

        private void DrawEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                _spriteBatch.Draw(enemyTexture, enemy.Position, enemy.GetAnimation(), Color.White);
            }
        }
        private void DrawBullets()
        {
            foreach (Bullet bullet in bullets)
            {
                _spriteBatch.Draw(bulletTexture2D, bullet.Position, Color.White);
            }
        }
        private void DrawTowers()
        {
            for (int i=0; i<towers.Length; i++)
            {
                Vector2 towerPosition = towers[i].GetPosition();
                Texture2D towerImage = towers[i].GetImage();
                _spriteBatch.Draw(squareTexture2D, new Vector2(towerPosition.X + offSetX, towerPosition.Y), Color.White);
                _spriteBatch.Draw(towerImage, new Vector2(towerPosition.X + offSetX, towerPosition.Y-32), Color.White);
            }

        }
        private void DrawPath()
        {
            _spriteBatch.Draw(squareNorthSouthTexture, new Vector2(128 + offSetX, 0), Color.White);
            _spriteBatch.Draw(squareNorthWestTexture, new Vector2(128 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareEastWestTexture, new Vector2(96 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareEastWestTexture, new Vector2(64 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareEastSouthTexture, new Vector2(32 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareNorthSouthTexture, new Vector2(32 + offSetX, 64), Color.White);
            _spriteBatch.Draw(squareNorthSouthTexture, new Vector2(32 + offSetX, 96), Color.White);
            _spriteBatch.Draw(squareNorthSouthTexture, new Vector2(32 + offSetX, 128), Color.White);
            _spriteBatch.Draw(squareNorthSouthTexture, new Vector2(32 + offSetX, 160), Color.White);
            _spriteBatch.Draw(squareNorthSouthTexture, new Vector2(32 + offSetX, 192), Color.White);
            _spriteBatch.Draw(squareNorthEastTexture, new Vector2(32 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareSouthWestTexture, new Vector2(64 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareNorthEastTexture, new Vector2(64 + offSetX, 256), Color.White);
            _spriteBatch.Draw(squareEastWestTexture, new Vector2(96 + offSetX, 256), Color.White);
            _spriteBatch.Draw(squareEastWestTexture, new Vector2(128 + offSetX, 256), Color.White);
            _spriteBatch.Draw(squareNorthWestTexture, new Vector2(160 + offSetX, 256), Color.White);
            _spriteBatch.Draw(squareEastSouthTexture, new Vector2(160 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareEastWestTexture, new Vector2(192 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareEastWestTexture, new Vector2(224 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareSouthWestTexture, new Vector2(256 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareNorthSouthTexture, new Vector2(256 + offSetX, 256), Color.White);
            _spriteBatch.Draw(squareNorthEastTexture, new Vector2(256 + offSetX, 288), Color.White);
            _spriteBatch.Draw(squareEastWestTexture, new Vector2(288 + offSetX, 288), Color.White);
            _spriteBatch.Draw(squareEastWestTexture, new Vector2(320 + offSetX, 288), Color.White);
            _spriteBatch.Draw(squareEastWestTexture, new Vector2(352 + offSetX, 288), Color.White);
            _spriteBatch.Draw(squareNorthWestTexture, new Vector2(384 + offSetX, 288), Color.White);
            _spriteBatch.Draw(squareNorthSouthTexture, new Vector2(384 + offSetX, 256), Color.White);
            _spriteBatch.Draw(squareNorthSouthTexture, new Vector2(384 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareNorthSouthTexture, new Vector2(384 + offSetX, 192), Color.White);
            _spriteBatch.Draw(squareSouthWestTexture, new Vector2(384 + offSetX, 160), Color.White);
            _spriteBatch.Draw(squareNorthEastTexture, new Vector2(352 + offSetX, 160), Color.White);
            _spriteBatch.Draw(squareNorthSouthTexture, new Vector2(352 + offSetX, 128), Color.White);
            _spriteBatch.Draw(squareNorthSouthTexture, new Vector2(352 + offSetX, 96), Color.White);
            _spriteBatch.Draw(squareSouthWestTexture, new Vector2(352 + offSetX, 64), Color.White);
            _spriteBatch.Draw(squareEastWestTexture, new Vector2(320 + offSetX, 64), Color.White);
            _spriteBatch.Draw(squareNorthEastTexture, new Vector2(288 + offSetX, 64), Color.White);
            _spriteBatch.Draw(squareSouthWestTexture, new Vector2(288 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareEastWestTexture, new Vector2(256 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareEastWestTexture, new Vector2(224 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareEastWestTexture, new Vector2(192 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareNorthEastTexture, new Vector2(160 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareNorthSouthTexture, new Vector2(160 + offSetX, 0), Color.White);
        } 
    }
}
