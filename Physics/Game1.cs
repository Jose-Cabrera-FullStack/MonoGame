using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Flat;
using Flat.Graphics;
using Flat.Input;
using System.Diagnostics;
using FlatPhysics;


namespace Physics;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private Screen screen;
    private Sprites sprites;
    private Shapes shapes;
    private Camera camera;

    private FlatVector vectorA = new FlatVector(3f, 4f);

    public Game1()
    {
        this.graphics = new GraphicsDeviceManager(this);
        this.graphics.SynchronizeWithVerticalRetrace = true;

        this.Content.RootDirectory = "Content";
        this.IsMouseVisible = true;
        this.IsFixedTimeStep = true;

        const double UpdatesPerSecond = 60d;
        this.TargetElapsedTime = TimeSpan.FromTicks((long)Math.Round((double)TimeSpan.TicksPerSecond / UpdatesPerSecond));
    }

    protected override void Initialize()
    {


        FlatUtil.SetRelativeBackBufferSize(this.graphics, 0.85f);

        this.screen = new Screen(this, 1280, 720);
        this.sprites = new Sprites(this);
        this.shapes = new Shapes(this);
        this.camera = new Camera(this.screen);
        this.camera.Zoom = 5;

        base.Initialize();
    }

    protected override void LoadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
        FlatKeyboard keyboard = FlatKeyboard.Instance;
        FlatMouse mouse = FlatMouse.Instance;

        keyboard.Update();
        mouse.Update();

        if (keyboard.IsKeyDown(Keys.Escape))
        {
            this.Exit();
        }

        if (keyboard.IsKeyDown(Keys.A))
        {
            this.camera.IncZoom();
        }

        if (keyboard.IsKeyDown(Keys.Z))
        {
            this.camera.DecZoom();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        this.screen.Set();
        GraphicsDevice.Clear(new Color(50, 60, 70));

        FlatVector normalized = FlatPhysics.FlatMath.Normalize(this.vectorA);

        this.shapes.Begin(this.camera);
        this.shapes.DrawLine(Vector2.Zero, FlatConverter.ToVector2(this.vectorA), Color.White);
        this.shapes.DrawLine(Vector2.Zero, FlatConverter.ToVector2(normalized), Color.Red);
        this.shapes.End();

        this.screen.Unset();
        this.screen.Present(this.sprites);

        base.Draw(gameTime);
    }
}
