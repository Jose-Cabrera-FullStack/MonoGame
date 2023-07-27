using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flat.Graphics

{
    public sealed class Screen : IDisposable
    {
        private readonly static int MinDim = 64;
        private readonly static int MaxDim = 4096;

        private bool isDisposed;
        private Game game;
        private RenderTarget2D target;
        private bool isSet;

        public int Width => this.target.Width;
        public int Height => this.target.Height;

        public Screen(Game game, int width, int height)
        {
            width = Util.Clamp(width, Screen.MinDim, Screen.MaxDim);
            height = Util.Clamp(height, Screen.MinDim, Screen.MaxDim);

            this.game = game ?? throw new ArgumentNullException(nameof(game));

            this.target = new RenderTarget2D(this.game.GraphicsDevice, width, height);
            this.isSet = false;
        }

        public void Dispose()
        {
            if (this.isDisposed)
            {
                return;
            }
            this.target?.Dispose();
            this.isDisposed = true;
        }

        public void Set()
        {
            if (this.isSet)
            {
                throw new InvalidOperationException("Screen is already set");
            }
            this.game.GraphicsDevice.SetRenderTarget(this.target);
            this.isSet = true;
        }

        public void UnSet()
        {
            if (!this.isSet)
            {
                throw new InvalidOperationException("Screen is not set");
            }
            this.game.GraphicsDevice.SetRenderTarget(null);
            this.isSet = false;
        }

        public void Present(Sprites sprites, bool textureFiltering = true)
        {
            if (sprites is null)
            {
                throw new ArgumentNullException(nameof(sprites));
            }

#if DEBUG
            this.game.GraphicsDevice.Clear(Color.Magenta);
#else
                this.game.GraphicsDevice.Clear(Color.Black);
#endif

            Rectangle destinationRectangle = this.CalculateDestinationRectangle();

            sprites.Begin(textureFiltering);
            sprites.Draw(this.target, null, destinationRectangle, Color.White);
            sprites.End();
        }

        private Rectangle CalculateDestinationRectangle()
        {

            Rectangle backbuggerBounds = this.game.GraphicsDevice.PresentationParameters.Bounds;
            float backbufferAspectRatio = (float)backbuggerBounds.Width / (float)backbuggerBounds.Height;
            float screenAspectRatio = (float)this.Width / (float)this.Height;

            float rx = 0f;
            float ry = 0f;
            float rw = backbuggerBounds.Width;
            float rh = backbuggerBounds.Height;

            if (backbufferAspectRatio > screenAspectRatio)
            {
                rw = rh * screenAspectRatio;
                rx = ((float)backbuggerBounds.Width - rw) / 2f;
            }
            else if (backbufferAspectRatio < screenAspectRatio)
            {
                rh = rw / screenAspectRatio;
                ry = ((float)backbuggerBounds.Height - rh) / 2f;
            }

            Rectangle result = new Rectangle((int)rx, (int)ry, (int)rw, (int)rh);
            return result;
        }
    }
}