using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System;
using OpenTK.Mathematics;
using EngineBalloon.Graphics;
using EngineBalloon.GameObjects;
using System.Collections.Generic;
using EngineBalloon.Physics;

namespace EngineBalloon
{
    public class Game : GameWindow
    {
        private Action<string> _drawFps;


        private GraphicController GraphicController { get; set; }


        private List<GameObject> GameObjects { get; set; }


        private Player Main { get; set; }
        private Player Secondary { get; set; }

        public Game(Action<string> drawFPS = null) : base(GameWindowSettings.Default, new NativeWindowSettings
        {
            Size = new Vector2i(800, 600),
            WindowBorder = WindowBorder.Hidden,
            WindowState = WindowState.Normal,
            Title = "Balloon War!",
            APIVersion = new Version(3, 3),
            Profile = ContextProfile.Core,
            API = ContextAPI.OpenGL,
            NumberOfSamples = 0
        })
        {
            _drawFps = drawFPS;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(Color4.LightBlue);

            GraphicController = new GraphicController(1, "Graphics/Shaders/", "Graphics/Textures/");

            GameObjects = new List<GameObject>();

            Main = new Player(new Sprite(GraphicController.Shader, GraphicController.Textures[0]), new Vector2(-0.8f, 0.0f));
            Main.ResizeByWindow(Size.X, Size.Y);
            GameObjects.Add(Main);

            Secondary = new Player(new Sprite(GraphicController.Shader, GraphicController.Textures[0]), new Vector2(0.8f, 0.0f));
            Secondary.ResizeByWindow(Size.X, Size.Y);
            GameObjects.Add(Secondary);

            Timer.Run();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            if (Timer.IsFixedUpdate())
            {
                Main.Move(KeyboardState, Timer.DeltaTime);

                PhysicController.UseGravity(GameObjects, Timer.DeltaTime);
            }

            Timer.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            Main.Draw();
            Secondary.Draw();

            _drawFps?.Invoke("Fps: " + Timer.FrameCount);

            SwapBuffers();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}
