using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MiniTD
{
    public class Game1 : Game
    {
        //public EventHandler eventHandler;

        GUIHandlerRight gui;

        Player player = new Player();

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bulletTexture2D;
        Texture2D squareTexture2D;
        Texture2D activeSquareTexture2D;
        Texture2D squareNorthEastTexture;
        Texture2D squareNorthSouthTexture;
        Texture2D squareNorthWestTexture;
        Texture2D squareEastSouthTexture;
        Texture2D squareEastWestTexture;
        Texture2D squareSouthWestTexture;

        SpriteFont defaultFont;

        bool upKeyWasUp = true;
        bool rightKeyWasUp = true;
        bool downKeyWasUp = true;
        bool leftKeyWasUp = true;
        bool spaceKeyWasUp = true;

        Texture2D[] towerTexture2Ds = new Texture2D[1];
        int offSetX = 144;

        List<Bullet> bullets = new List<Bullet>();
        Tower[] towers = new Tower[5];
        Vector2[] towerPositions = new Vector2[5]
        {
            new Vector2(192+144, 96),
            new Vector2(160+144, 128),
            new Vector2(192+144, 128),
            new Vector2(224+144, 128),
            new Vector2(192+144, 160)
        };
        int activeTower = 2;

        WaveSpawner waveSpawner;
        Texture2D enemyTexture;
        List<Enemy> enemies = new List<Enemy>();
        Vector2[] enemyRoute;

        List<Vector2> towerRangeVecs = new List<Vector2>();

        int monsterRouteOffsetX = 8;
        int monsterRouteOffsetY = 8;

        ActiveUIType activeUI = ActiveUIType.Main;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            base.Initialize();
            
            
            for (int i = 0; i < towers.Length; i++)
            {
                Vector2 position = towerPositions[i];
                towers[i] = new Tower(
                    (int)position.X, 
                    (int)position.Y, 
                    towerTexture2Ds[0], 
                    _graphics.PreferredBackBufferWidth, 
                    _graphics.PreferredBackBufferHeight);
            }
            towers[activeTower].IsActive = true;

            enemyRoute = new Vector2[]
            {
                new Vector2(280, 2),
                new Vector2(280, 38),
                new Vector2(184, 38),
                new Vector2(184, 230),
                new Vector2(216, 230),
                new Vector2(216, 262),
                new Vector2(312, 262),
                new Vector2(312, 230),
                new Vector2(408, 230),
                new Vector2(408, 294),
                new Vector2(536, 294),
                new Vector2(536, 166),
                new Vector2(504, 166),
                new Vector2(504, 70),
                new Vector2(440, 70),
                new Vector2(440, 38),
                new Vector2(312, 38),
                new Vector2(312, 2)
            };

            waveSpawner = new WaveSpawner(enemyRoute, 
                                          new Animation(enemyTexture), 
                                          _graphics.PreferredBackBufferWidth, 
                                          _graphics.PreferredBackBufferHeight);

            enemies.AddRange(waveSpawner.GetEnemies());

            gui = new GUIHandlerRight(defaultFont);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            squareTexture2D = Content.Load<Texture2D>("square_002");
            activeSquareTexture2D = Content.Load<Texture2D>("redsquare_001");
            squareNorthEastTexture = Content.Load<Texture2D>("squareNorthEast_002");
            squareNorthSouthTexture = Content.Load<Texture2D>("squareNorthSouth_002");
            squareNorthWestTexture = Content.Load<Texture2D>("squareNorthWest_002");
            squareEastSouthTexture = Content.Load<Texture2D>("squareEastSouth_002");
            squareEastWestTexture = Content.Load<Texture2D>("squareEastWest_002");
            squareSouthWestTexture = Content.Load<Texture2D>("squareSouthWest_002");

            enemyTexture = Content.Load<Texture2D>("mob1_003");

            towerTexture2Ds[0] = Content.Load<Texture2D>("tower_005");
            bulletTexture2D = Content.Load<Texture2D>("Bullet_001");

            defaultFont = Content.Load<SpriteFont>("DefaultFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //GameEventArgs eventArgs = new GameEventArgs(GameEventType.InputEvent);

            if (activeUI == ActiveUIType.Main)
            {
                GameKeyboardController();
            }
            else if (activeUI == ActiveUIType.Tower)
            {
                TowerKeyboardController();
            }

            waveSpawner.Update(gameTime);
            enemies.AddRange(waveSpawner.GetEnemies());

            for (int i=0; i<towers.Length; i++)
            {
                towers[i].IsActive = (i == activeTower);
                towers[i].Update(enemies);
                if (towers[i].ReadyToShoot)
                {
                    bullets.Add(towers[i].Shoot(enemies));
                }
            }

            List<Enemy> enemiesToDelete = new List<Enemy>();
            List<Bullet> bulletsToDelete = new List<Bullet>();
            foreach (Bullet bullet in bullets)
            {
                bullet.Update();
                if (bullet.ReadyForDeletion)
                {
                    bulletsToDelete.Add(bullet);
                }
                foreach (Enemy enemy in enemies)
                {
                    Vector2 enemyCenter = new Vector2(enemy.Position.X+8, enemy.Position.Y+8);
                    if (!bullet.ReadyForDeletion && Vector2.Distance(enemyCenter, bullet.Position) < 8f)
                    {
                        Debug.WriteLine(enemy.ID + " hit!");
                        enemiesToDelete.Add(enemy);
                        bulletsToDelete.Add(bullet);
                        bullet.ReadyForDeletion = true;
                    }
                }
            }

            foreach (Bullet bullet in bulletsToDelete)
            {
                bullets.Remove(bullet);
            }
            foreach (Enemy enemy in enemiesToDelete)
            {
                Debug.WriteLine(enemy.ID + " deleted");
                player.AddBounty(enemy);
                enemies.Remove(enemy);
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            gui.Update(towers[activeTower], 
                       activeTower+1, 
                       waveSpawner.Level, 
                       bullets.Count, 
                       enemies.Count, 
                       player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            DrawPath();
            DrawTowerRanges();
            DrawTowers();
            DrawEnemies();
            DrawBullets();
            gui.Draw(_spriteBatch);
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

        private void DrawTowerRanges()
        {
            foreach (Tower tower in towers)
            {
                if (towers[activeTower] == tower)
                {
                    _spriteBatch.Draw(
                        CreateCircle(tower.Range * 2),
                        new Vector2(tower.GetPosition().X - tower.Range + 16, tower.GetPosition().Y - tower.Range + 16),
                        Color.Black * 0.1f);
                }
            }
        }
        private void DrawTowers()
        {
            for (int i=0; i<towers.Length; i++)
            {
                Vector2 towerPosition = towers[i].GetPosition();
                Texture2D towerImage = towers[i].GetImage();
                

                if (towers[i].IsActive)
                {
                    _spriteBatch.Draw(activeSquareTexture2D, new Vector2(towerPosition.X, towerPosition.Y), Color.White);
                } else
                {
                    _spriteBatch.Draw(squareTexture2D, new Vector2(towerPosition.X, towerPosition.Y), Color.White);
                }

                
                
                _spriteBatch.Draw(towerImage, new Vector2(towerPosition.X, towerPosition.Y-32), Color.White);
                

            }

        }

        private Texture2D CreateCircle(int diameter)
        {
            Texture2D texture = new Texture2D(_graphics.GraphicsDevice, diameter, diameter);
            Color[] colorData = new Color[diameter * diameter];

            float radius = diameter / 2f;
            float radiusSquared = radius * radius;

            for (int x = 0; x < diameter; x++)
            {
                for (int y = 0; y < diameter; y++)
                {
                    int index = x * diameter + y;
                    Vector2 pos = new Vector2(x - radius, y - radius);
                    if (pos.LengthSquared() <= radiusSquared)
                    {
                    
                        colorData[index] = Color.White;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }


        private void TowerKeyboardController()
        {

        }
        private void GameKeyboardController()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && spaceKeyWasUp)
            {
                spaceKeyWasUp = false;
                SelectTower(Keys.Space);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && leftKeyWasUp)
            {
                leftKeyWasUp = false;
                SelectTower(Keys.Left);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && rightKeyWasUp)
            {
                rightKeyWasUp = false;
                SelectTower(Keys.Right);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && upKeyWasUp)
            {
                upKeyWasUp = false;
                SelectTower(Keys.Up);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && downKeyWasUp)
            {
                downKeyWasUp = false;
                SelectTower(Keys.Down);
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                spaceKeyWasUp = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Left))
            {
                leftKeyWasUp = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Right))
            {
                rightKeyWasUp = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                upKeyWasUp = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Down))
            {
                downKeyWasUp = true;
            }
        }
        private void SelectTower(Keys key)
        {
            switch (activeTower)
            {
                case 0:
                    switch (key)
                    {
                        case Keys.Up:
                            activeTower = 4;
                            break;
                        case Keys.Right:
                            activeTower = 3;
                            break;
                        case Keys.Down:
                            activeTower = 2;
                            break;
                        case Keys.Left:
                            activeTower = 1;
                            break;
                    }
                    break;
                case 1:
                    switch (key)
                    {
                        case Keys.Up:
                            activeTower = 0;
                            break;
                        case Keys.Right:
                            activeTower = 2;
                            break;
                        case Keys.Down:
                            activeTower = 4;
                            break;
                        case Keys.Left:
                            activeTower = 3;
                            break;
                    }
                    break;
                case 2:
                    switch (key)
                    {
                        case Keys.Up:
                            activeTower = 0;
                            break;
                        case Keys.Right:
                            activeTower = 3;
                            break;
                        case Keys.Down:
                            activeTower = 4;
                            break;
                        case Keys.Left:
                            activeTower = 1;
                            break;
                    }
                    break;
                case 3:
                    switch (key)
                    {
                        case Keys.Up:
                            activeTower = 0;
                            break;
                        case Keys.Right:
                            activeTower = 1;
                            break;
                        case Keys.Down:
                            activeTower = 4;
                            break;
                        case Keys.Left:
                            activeTower = 2;
                            break;
                    }
                    break;
                case 4:
                    switch (key)
                    {
                        case Keys.Up:
                            activeTower = 2;
                            break;
                        case Keys.Right:
                            activeTower = 3;
                            break;
                        case Keys.Down:
                            activeTower = 0;
                            break;
                        case Keys.Left:
                            activeTower = 1;
                            break;
                    }
                    break;
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
