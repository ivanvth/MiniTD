using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiniTD
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D square;
        int offSetX = 0;

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

            for (int i=0; i<towers.Length; i++)
            {
                Vector2 position = towerPositions[i];
                towers[i] = new Tower((int)position.X + offSetX, (int)position.Y);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            square = Content.Load<Texture2D>("square_001");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            DrawPath();
            DrawTowers();
            _spriteBatch.End();


            base.Draw(gameTime);
        }

        private void DrawTowers()
        {
            foreach (Vector2 postion in towerPositions)
            {
                _spriteBatch.Draw(square, new Vector2(postion.X + offSetX, postion.Y), Color.Red);
            }

        }
        private void DrawPath()
        {
            _spriteBatch.Draw(square, new Vector2(128 + offSetX, 0), Color.White);
            _spriteBatch.Draw(square, new Vector2(128 + offSetX, 32), Color.White);
            _spriteBatch.Draw(square, new Vector2(96 + offSetX, 32), Color.White);
            _spriteBatch.Draw(square, new Vector2(64 + offSetX, 32), Color.White);
            _spriteBatch.Draw(square, new Vector2(32 + offSetX, 32), Color.White);
            _spriteBatch.Draw(square, new Vector2(32 + offSetX, 64), Color.White);
            _spriteBatch.Draw(square, new Vector2(32 + offSetX, 96), Color.White);
            _spriteBatch.Draw(square, new Vector2(32 + offSetX, 128), Color.White);
            _spriteBatch.Draw(square, new Vector2(32 + offSetX, 160), Color.White);
            _spriteBatch.Draw(square, new Vector2(32 + offSetX, 192), Color.White);
            _spriteBatch.Draw(square, new Vector2(32 + offSetX, 224), Color.White);
            _spriteBatch.Draw(square, new Vector2(64 + offSetX, 224), Color.White);
            _spriteBatch.Draw(square, new Vector2(64 + offSetX, 256), Color.White);
            _spriteBatch.Draw(square, new Vector2(96 + offSetX, 256), Color.White);
            _spriteBatch.Draw(square, new Vector2(128 + offSetX, 256), Color.White);
            _spriteBatch.Draw(square, new Vector2(160 + offSetX, 256), Color.White);
            _spriteBatch.Draw(square, new Vector2(160 + offSetX, 224), Color.White);
            _spriteBatch.Draw(square, new Vector2(192 + offSetX, 224), Color.White);
            _spriteBatch.Draw(square, new Vector2(224 + offSetX, 224), Color.White);
            _spriteBatch.Draw(square, new Vector2(256 + offSetX, 224), Color.White);
            _spriteBatch.Draw(square, new Vector2(256 + offSetX, 256), Color.White);
            _spriteBatch.Draw(square, new Vector2(256 + offSetX, 288), Color.White);
            _spriteBatch.Draw(square, new Vector2(288 + offSetX, 288), Color.White);
            _spriteBatch.Draw(square, new Vector2(320 + offSetX, 288), Color.White);
            _spriteBatch.Draw(square, new Vector2(352 + offSetX, 288), Color.White);
            _spriteBatch.Draw(square, new Vector2(384 + offSetX, 288), Color.White);
            _spriteBatch.Draw(square, new Vector2(384 + offSetX, 256), Color.White);
            _spriteBatch.Draw(square, new Vector2(384 + offSetX, 224), Color.White);
            _spriteBatch.Draw(square, new Vector2(384 + offSetX, 192), Color.White);
            _spriteBatch.Draw(square, new Vector2(384 + offSetX, 160), Color.White);
            _spriteBatch.Draw(square, new Vector2(352 + offSetX, 160), Color.White);
            _spriteBatch.Draw(square, new Vector2(352 + offSetX, 128), Color.White);
            _spriteBatch.Draw(square, new Vector2(352 + offSetX, 96), Color.White);
            _spriteBatch.Draw(square, new Vector2(352 + offSetX, 64), Color.White);
            _spriteBatch.Draw(square, new Vector2(320 + offSetX, 64), Color.White);
            _spriteBatch.Draw(square, new Vector2(288 + offSetX, 64), Color.White);
            _spriteBatch.Draw(square, new Vector2(288 + offSetX, 32), Color.White);
            _spriteBatch.Draw(square, new Vector2(256 + offSetX, 32), Color.White);
            _spriteBatch.Draw(square, new Vector2(224 + offSetX, 32), Color.White);
            _spriteBatch.Draw(square, new Vector2(192 + offSetX, 32), Color.White);
            _spriteBatch.Draw(square, new Vector2(160 + offSetX, 32), Color.White);
            _spriteBatch.Draw(square, new Vector2(160 + offSetX, 0), Color.White);
        } 
    }
}
