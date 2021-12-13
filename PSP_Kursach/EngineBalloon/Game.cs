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
using Network;
using Network.Models;

namespace EngineBalloon
{
    public class Game : GameWindow
    {
        private Action<string> _drawFps;

        private GraphicController GraphicController { get; set; }

        private List<GameObject> GameObjects { get; set; }

        private Player Main { get; set; }
        private Player Secondary { get; set; }

        private GameOver WinPlayer1 {get; set;}

        private GameOver WinPlayer2 { get; set; }

        private bool GameOver { get; set; }

        public static Client Client { get; set; }

        private List<WindModel> WindModels { get; set; }

        public Game(string url, Action<string> drawFPS = null, int width = 1920, int height = 1080) : base(GameWindowSettings.Default, new NativeWindowSettings
        {
            Size = new Vector2i(width, height),
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
            Client = new Client("http://" + url + ":8080");
            Client.Connect();
        }

        private void SetUpPrize()
        {
            PrizeCreator.MaxPrize = 7;
            PrizeCreator.PrizeCount = 0;
            PrizeCreator.Creators = new PrizeCreator[6]
            {
                new PrizeArmorCreator(GraphicController.GetSprite(2)),
                new PrizeDamageCreator(GraphicController.GetSprite(10)),
                new PrizeFuilCreator(GraphicController.GetSprite(9)),
                new PrizeHealthCreator(GraphicController.GetSprite(4)),
                new PrizeRadiusCreator(GraphicController.GetSprite(7)),
                new PrizeSpeedCreator(GraphicController.GetSprite(8)),
            };
            PrizeCreator.SizeWindow = Size * 3;
        }

        private void SetUpPlayer()
        {
            var bullet = new Bullet(GraphicController.GetSprite(6));
            bullet.ResizeByWindow(Size.X, Size.Y);
            bullet.Scale *= 0.5f;

            Main = new Player(bullet, GraphicController.GetSprite(Client.ClientId == 0 ? 0 : 1), new Vector2(Client.ClientId == 0 ? -0.8f : 0.8f, 0.0f));
            Main.ResizeByWindow(Size.X, Size.Y);
            Main.Scale *= 0.5f;
            GameObjects.Add(Main);

            Secondary = new Player(bullet, GraphicController.GetSprite(Client.ClientId == 0 ? 1 : 0), new Vector2(Client.ClientId == 1 ? -0.8f : 0.8f, 0.0f));
            Secondary.ResizeByWindow(Size.X, Size.Y);
            Secondary.Scale *= 0.5f;
            GameObjects.Add(Secondary);

            WinPlayer1 = new GameOver(GraphicController.GetSprite(11), Vector2.Zero);
            WinPlayer2 = new GameOver(GraphicController.GetSprite(12), Vector2.Zero);
        }

        private void SetUpPhisics()
        {
            GameObjects.Add(new Earth(GraphicController.GetSprite(5), new Vector2(0, -0.9f)));
            PhysicController.SizeWindow = Size;
        }

        private void SetUpWinds()
        {
            for (int i = 0; i < Wind.Winds.Length; i++)
            {
                Wind.Winds[i] = new Wind(GraphicController.GetSprite(3), new Vector2(0, 0.9f - i * 0.2f));
                GameObjects.Add(Wind.Winds[i]);
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.ClearColor(Color4.LightBlue);

            GraphicController = new GraphicController(13, "Graphics/Shaders/", "Graphics/Textures/");
            GameObjects = new List<GameObject>();

            SetUpPrize();
            SetUpPlayer();
            SetUpPhisics();
            SetUpWinds();

            while (Client.Start() != 0);
            Timer.Run();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected async override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            if (Timer.IsFixedUpdate() && !GameOver)
            {
                if(Client.ClientId == 0)
                {
                    if (PrizeCreator.PrizeCount < PrizeCreator.MaxPrize)
                    {
                        GameObjects.Add(PrizeCreator.GetPrize(out var prizeModel));
                        await Client.Set(prizeModel, "/Prizes/");
                    }
                }
                else
                {
                    var modelPrizes = await Client.Get<PrizeModel>("/Prizes/");
                    if(modelPrizes != null) GameObjects.Add(PrizeCreator.GetPrize(modelPrizes));
                }
                PhysicController.UseGravity(GameObjects, Timer.DeltaTime);

                Main.UseControl(KeyboardState, AddBullet);

                PhysicController.Collision(Main, GameObjects);
                PhysicController.Collision(Secondary, GameObjects);

                var modelBullet = await Client.Get<BulletModel>("/Bullet/");
                if (modelBullet != null)
                {
                    AddBullet(Secondary.BaseBullet, modelBullet);
                }

                foreach (var obj in GameObjects)
                {
                    obj.FixedUpdate(Timer.DeltaTime);
                }

                if (Client.ClientId == 0)
                {
                    WindModels = Wind.Update();
                }

                var model = new UpdateModel { WindModels = WindModels, X = Main.Position.X, Y = Main.Position.Y };
                await Client.Set(model, string.Empty);
                model = await Client.Get<UpdateModel>(string.Empty);
                Secondary.X = model.X;
                Secondary.Y = model.Y;

                if (Client.ClientId == 1)
                {
                    if (model != null && model.WindModels != null)
                        Wind.Update(model.WindModels);
                }

                if (Main.HP < 1)
                {
                    GameObjects.Add(Client.ClientId == 0 ? WinPlayer2 : WinPlayer1);
                    GameOver = true;
                }

                if (Secondary.HP < 1)
                {
                    GameObjects.Add(Client.ClientId == 1 ? WinPlayer2 : WinPlayer1);
                    GameOver = true;
                }
            }

            Timer.Update();
        }

        private async void AddBullet(Bullet baseBullet)
        {
            var bullet = (Bullet)baseBullet.Clone();
            var direction =  new Vector2((MousePosition.X * 2 / Size.X) - 1.0f - bullet.Position.X, 
                bullet.Position.Y - ((MousePosition.Y * 2 / Size.Y) - 1.0f));
            direction.Normalize();
            bullet.Direction = direction;
            GameObjects.Add(bullet);
            var bulletModel = new BulletModel()
            {
                X = bullet.X,
                Y = bullet.Y,
                DirectionX = bullet.Direction.X,
                DirectionY = bullet.Direction.Y
            };
            await Client.Set(bulletModel, "/Bullet/");
        }

        private void AddBullet(Bullet baseBullet, BulletModel model)
        {
            var bullet = (Bullet)baseBullet.Clone();
            var direction = new Vector2(model.DirectionX, model.DirectionY);
            direction.Normalize();
            bullet.Position = new Vector2(model.X, model.Y);
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
