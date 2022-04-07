using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace MiniTD
{
    public class Game1 : Game
    {
        bool equal = true;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bulletTexture2D;
        Texture2D squareTexture2D;
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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.ApplyChanges();

            base.Initialize();

            for (int i = 0; i < towers.Length; i++)
            {
                Vector2 position = towerPositions[i];
                towers[i] = new Tower((int)position.X + offSetX, (int)position.Y, towerTexture2Ds[0]);
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            squareTexture2D = Content.Load<Texture2D>("square_001");
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
                    bullets.Add(tower.Shoot());
                }
            }


            foreach (Bullet bullet in bullets)
            {
                bullet.UpdatePosition();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            if (equal)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
            
            } else
            {
                GraphicsDevice.Clear(Color.Red);
            }
            

            _spriteBatch.Begin();
            DrawPath();
            DrawTowers();
            DrawBullets();
            _spriteBatch.End();


            base.Draw(gameTime);
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
            _spriteBatch.Draw(squareTexture2D, new Vector2(128 + offSetX, 0), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(128 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(96 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(64 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(32 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(32 + offSetX, 64), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(32 + offSetX, 96), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(32 + offSetX, 128), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(32 + offSetX, 160), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(32 + offSetX, 192), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(32 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(64 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(64 + offSetX, 256), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(96 + offSetX, 256), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(128 + offSetX, 256), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(160 + offSetX, 256), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(160 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(192 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(224 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(256 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(256 + offSetX, 256), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(256 + offSetX, 288), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(288 + offSetX, 288), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(320 + offSetX, 288), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(352 + offSetX, 288), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(384 + offSetX, 288), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(384 + offSetX, 256), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(384 + offSetX, 224), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(384 + offSetX, 192), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(384 + offSetX, 160), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(352 + offSetX, 160), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(352 + offSetX, 128), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(352 + offSetX, 96), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(352 + offSetX, 64), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(320 + offSetX, 64), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(288 + offSetX, 64), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(288 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(256 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(224 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(192 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(160 + offSetX, 32), Color.White);
            _spriteBatch.Draw(squareTexture2D, new Vector2(160 + offSetX, 0), Color.White);
        } 
    }
}
