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
using EngineBalloon.GameObjects.Bullets;
using EngineBalloon.GameObjects.Prizes.Creators;

namespace EngineBalloon
{
    public class Game : GameWindow
    {
        private Action<string> _drawFps;

        private GraphicController GraphicController { get; set; }

        private List<GameObject> GameObjects { get; set; }

        private Player Main { get; set; }
        private Player Secondary { get; set; }

        public Game(Action<string> drawFPS = null, int width = 800, int height = 600) : base(GameWindowSettings.Default, new NativeWindowSettings
        {
            Size = new Vector2i(width, height),
            WindowBorder = WindowBorder.Hidden,
            WindowState = WindowState.Normal,
            Title = "Balloon War!",
            APIVersion = new Version(3, 3),
            Profile = ContextProfile.Core,
            API = ContextAPI.OpenGL,
            IsFullscreen = true,
            NumberOfSamples = 0
        })
        {
            _drawFps = drawFPS;
        }

        private void SetUpPrize()
        {
            PrizeCreator.MaxPrize = 7;
            PrizeCreator.PrizeCount = 0;
            PrizeCreator.Creators = new PrizeCreator[6]
            {
                new PrizeArmorCreator(GraphicController.GetSprite(2)),
                new PrizeDamageCreator(GraphicController.GetSprite(11)),
                new PrizeFuilCreator(GraphicController.GetSprite(10)),
                new PrizeHealthCreator(GraphicController.GetSprite(5)),
                new PrizeRadiusCreator(GraphicController.GetSprite(8)),
                new PrizeSpeedCreator(GraphicController.GetSprite(9)),
            };
            PrizeCreator.SizeWindow = Size * 3;
        }

        private void SetUpPlayer()
        {
            var bullet = new Bullet(GraphicController.GetSprite(7));
            bullet.ResizeByWindow(Size.X, Size.Y);
            bullet.Scale *= 0.5f;

            Main = new Player(bullet, GraphicController.GetSprite(0), new Vector2(-0.8f, 0.0f));
            Main.ResizeByWindow(Size.X, Size.Y);
            Main.Scale *= 0.5f;
            GameObjects.Add(Main);

            Secondary = new Player(bullet, GraphicController.GetSprite(1), new Vector2(0.8f, 0.0f));
            Secondary.ResizeByWindow(Size.X, Size.Y);
            Secondary.Scale *= 0.5f;
            GameObjects.Add(Secondary);
        }


        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.ClearColor(Color4.LightBlue);

            GraphicController = new GraphicController(12, "Graphics/Shaders/", "Graphics/Textures/");
            GameObjects = new List<GameObject>();

            SetUpPrize();
            SetUpPlayer();
            PhysicController.SizeWindow = Size;

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
                if(PrizeCreator.PrizeCount < PrizeCreator.MaxPrize) 
                    GameObjects.Add(PrizeCreator.GetPrize(out var prizeModel));
                PhysicController.UseGravity(GameObjects, Timer.DeltaTime);

                Main.UseControl(KeyboardState, AddBullet);

                PhysicController.Collision(Main, GameObjects);
                PhysicController.Collision(Secondary, GameObjects);

                foreach (var obj in GameObjects)
                {
                    obj.FixedUpdate(Timer.DeltaTime);
                }
            }

            Timer.Update();
        }

        private void AddBullet(Bullet baseBullet)
        {
            var bullet = (Bullet)baseBullet.Clone();
            var direction =  new Vector2((MousePosition.X * 2 / Size.X) - 1.0f - bullet.Position.X, 
                bullet.Position.Y - ((MousePosition.Y * 2 / Size.Y) - 1.0f));
            direction.Normalize();
            bullet.Direction = direction;
            GameObjects.Add(bullet);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach (var obj in GameObjects)
            {
                obj.Draw();
            }

            _drawFps?.Invoke("Fps: " + Timer.FrameCount);

            SwapBuffers();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}
