using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using System.ComponentModel;
using EngineBalloon.Graphics;
using System.Runtime.Versioning;

namespace EngineBalloon
{
    [SupportedOSPlatform("windows")]
    public class Game : GameWindow
    {
        private GraphicController GraphicController { get; set; }
        private Sprite Sprite { get; set; }

        public Game() : base(GameWindowSettings.Default, new NativeWindowSettings 
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
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(Color4.LightBlue);

            GraphicController = new GraphicController(1, "Graphics/Shaders/", "Graphics/Textures/");

            Sprite = new Sprite(GraphicController.Shader, GraphicController.Textures[0]);
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
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            Sprite.Draw(Vector2.Zero, Vector2.One * 0.2f, 0f, false);

            SwapBuffers();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}
