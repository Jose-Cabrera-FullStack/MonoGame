using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flat.Graphics
{
    public class Shapes : IDisposable
    {
        private bool isDisposed;
        private Game game;
        private BasicEffect effect;

        private VertexPositionColor[] vertices;
        private int[] indices;

        private int shapeCount;
        private int vertexCount;
        private int indexCount;

        private bool isStarted;

        public Shapes(Game game)
        {
            this.isDisposed = false;
            this.game = game ?? throw new ArgumentException("Game cannot be null");

            this.effect = new BasicEffect(this.game.GraphicsDevice);
            this.effect.TextureEnabled = false;
            this.effect.FogEnabled = false;
            this.effect.LightingEnabled = false;
            this.effect.VertexColorEnabled = true;
            this.effect.World = Matrix.Identity;
            this.effect.View = Matrix.Identity;
            this.effect.Projection = Matrix.Identity;

            const int MaxVertexCount = 1024;
            const int MaxIndexCount = MaxVertexCount * 3;

            this.vertices = new VertexPositionColor[MaxVertexCount];
            this.indices = new int[MaxIndexCount];

            this.shapeCount = 0;
            this.vertexCount = 0;
            this.indexCount = 0;

            this.isStarted = false;
        }

        public void Dispose()
        {
            if (this.isDisposed)
            {
                return;
            }

            this.effect?.Dispose();
            this.isDisposed = true;
        }

        public void Begin()
        {
            if (this.isStarted)
            {
                throw new Exception("Begin cannot be called twice");
            }

            Viewport vp = this.game.GraphicsDevice.Viewport;
            this.effect.Projection = Matrix.CreateOrthographicOffCenter(0, vp.Width, 0, vp.Height, 0, 1);

            this.isStarted = true;

        }
        public void End()
        {

            this.Flush();
            this.isStarted = false;
        }

        public void Flush()
        {

            if (this.shapeCount == 0)
            {
                return;
            }

            this.EnsureStarted();

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.game.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleList,
                    this.vertices,
                    0,
                    this.vertexCount,
                    this.indices,
                    0,
                    this.indexCount / 3
                );
            }

            this.shapeCount = 0;
            this.vertexCount = 0;
            this.indexCount = 0;

        }

        public void EnsureStarted()
        {
            if (!this.isStarted)
            {
                throw new Exception("Begin must be called before End");
            }
        }

        public void EnsureSpace(int shapeVertexCount, int shapeIndexCount)
        {
            if (this.vertexCount > this.vertices.Length)
            {
                throw new Exception("Too many vertices");
            }
            if (this.indexCount >= this.indices.Length)
            {
                throw new Exception("Too many indices");
            }

            if (this.vertexCount + shapeVertexCount > this.vertices.Length ||
                this.indexCount + shapeIndexCount > this.indices.Length)
            {
                this.Flush();
            }
        }

        public void DrawRectangle(float x, float y, float width, float height, Color color)
        {
            this.EnsureStarted();

            const int shapeVertexCount = 4;
            const int shapeIndexCount = 6;

            this.EnsureSpace(shapeVertexCount, shapeIndexCount);

            float left = x;
            float right = x + width;
            float bottom = y;
            float top = y + height;

            Vector2 a = new Vector2(left, top);
            Vector2 b = new Vector2(right, top);
            Vector2 c = new Vector2(right, bottom);
            Vector2 d = new Vector2(left, bottom);


            this.indices[this.indexCount++] = 0 + this.vertexCount;
            this.indices[this.indexCount++] = 1 + this.vertexCount;
            this.indices[this.indexCount++] = 2 + this.vertexCount;
            this.indices[this.indexCount++] = 0 + this.vertexCount;
            this.indices[this.indexCount++] = 2 + this.vertexCount;
            this.indices[this.indexCount++] = 3 + this.vertexCount;

            this.vertices[this.vertexCount++] = new VertexPositionColor(new Vector3(a, 0f), color);
            this.vertices[this.vertexCount++] = new VertexPositionColor(new Vector3(b, 0f), color);
            this.vertices[this.vertexCount++] = new VertexPositionColor(new Vector3(c, 0f), color);
            this.vertices[this.vertexCount++] = new VertexPositionColor(new Vector3(d, 0f), color);

            this.shapeCount++;
        }
    }
}