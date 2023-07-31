using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Flat.Graphics;
using Flat.Input;

namespace MonoGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private Sprites sprites;
        private Texture2D texture;
        private Screen screen;
        private float x = 32f;
        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.SynchronizeWithVerticalRetrace = true;

            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.IsFixedTimeStep = true;
        }

        protected override void Initialize()
        {
            this.sprites = new Sprites(this);
            this.screen = new Screen(this, 640, 480);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.texture = this.Content.Load<Texture2D>("Duck");
        }

        protected override void Update(GameTime gameTime)
        {
            FlatKeyboard keyboard = FlatKeyboard.Instance;
            keyboard.Update();

            if (keyboard.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (keyboard.IsKeyDown(Keys.Right))
            {
                this.x += 1f;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.screen.Set();
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            Viewport viewport = this.GraphicsDevice.Viewport;

            this.sprites.Begin(false);
            this.sprites.Draw(texture, null, new Rectangle((int)x, 32, 521, 256), Color.White);
            this.sprites.End();

            this.screen.UnSet();
            this.screen.Present(this.sprites);

            base.Draw(gameTime);
        }
    }
}
