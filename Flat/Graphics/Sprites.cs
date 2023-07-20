using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Flat.Graphics
{
    public sealed class Sprites : IDisposable
    {
        private bool isDisposed;
        private Game game;
        private SpriteBatch sprites;
        private BasicEffect effect;

        public Sprites(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            this.game = game;
            this.isDisposed = false;
            this.sprites = new SpriteBatch(this.game.GraphicsDevice);

            this.effect = new BasicEffect(this.game.GraphicsDevice);
            this.effect.FogEnabled = false;
            this.effect.TextureEnabled = true;
            this.effect.LightingEnabled = false;
            this.effect.VertexColorEnabled = true;
            this.effect.World = Matrix.Identity;
            this.effect.Projection = Matrix.Identity;
            this.effect.View = Matrix.Identity;
        }

        public void Dispose()
        {
            if (this.isDisposed)
            {
                return;
            }

            this.sprites?.Dispose();
            this.isDisposed = true;

        }

        public void Begin()
        {
            Viewport vp = this.game.GraphicsDevice.Viewport;
            this.effect.Projection = Matrix.CreateOrthographicOffCenter(0, vp.Width, 0, vp.Height, 0, 1);

            this.sprites.Begin();
        }

        public void End()
        {
            this.sprites.End();
        }

        public void Draw(Texture2D texture, Vector2 position, Color color)
        {
            this.sprites.Draw(texture, position, color);
        }
    }
}