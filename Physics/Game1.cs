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

        float x1 = 0.5f;
        float y1 = 0.002f;
        float x2 = 0.001f;
        float y2 = 0.003f;

        Stopwatch watch = new Stopwatch();

        watch.Start();
        for (int i = 0; i < 1000000; i++)
        {
            x1 = x1 + x2;
            y1 = y1 + y2;
        }


        Console.WriteLine($"{x1}, {y1}");
        Console.WriteLine("Time: " + watch.ElapsedMilliseconds);

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

        this.shapes.Begin(this.camera);
        this.shapes.DrawCircle(0, 0, 32, 32, Color.White);
        this.shapes.End();

        this.screen.Unset();
        this.screen.Present(this.sprites);

        base.Draw(gameTime);
    }
}
