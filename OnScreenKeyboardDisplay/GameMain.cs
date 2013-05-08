using System;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OnScreenKeyboardDisplayLibrary;
using OnScreenKeyboardDisplayLibrary.Configuration.Reader;
using XNATools.Attributes;
using XNATools.Managers;
using XNATools.Services;
using XNATools.Services.Interfaces;
using Graph = OnScreenKeyboardDisplayLibrary.Sprites;

namespace OnScreenKeyboardDisplay
{
    public class GameMain : Game
    {
        private static readonly string AppPath = Assembly.GetExecutingAssembly().Location.Replace(Path.GetFileName(Assembly.GetExecutingAssembly().Location), string.Empty);

        Graph.Keyboard keyboard;
        Graph.Mouse mouse;

        Color backColor = new Color(14, 14, 14);

        int screenHeight = 200;
        int screenWidth = 800;

        int windowPositionX = 3;
        int windowPositionY = 22;

        bool showWindowBorder = true;
        bool pinWindow = true;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            ServiceManager.Game = this;

            // Application's resolution setup:
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            Content.RootDirectory = "OSKDContent";
        }

        protected override void Initialize()
        { 
            ServiceManager.AddSpriteBatch(new SpriteBatch(GraphicsDevice));
            spriteBatch = ServiceManager.Get<SpriteBatch>();

            GlobalKeyboardService kService = new GlobalKeyboardService(this);
            kService.Hook();

            GlobalMouseService mService = new GlobalMouseService(this);
            mService.Hook();
            //MouseService mService = new MouseService(this);
            IsMouseVisible = true;

            CachedContentManager<Texture2D> textureCache = new CachedContentManager<Texture2D>(this);
            KeyboardLayoutReader.ReadConfigFile(Path.Combine(AppPath, "Configuration"));

            keyboard = new Graph.Keyboard(this);
            mouse = new Graph.Mouse(this);            

            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            AssemblyName thisAssemblyName = thisAssembly.GetName();
            Version thisAssemblyVersion = thisAssemblyName.Version;
            AssemblyCodeNameAttribute assemblyCodeName = (AssemblyCodeNameAttribute)thisAssembly.GetCustomAttributes(typeof(AssemblyCodeNameAttribute), true)[0];

            Window.Title = string.Format("{0} V.{1}.{2} \"{3}\"", thisAssemblyName.Name, thisAssemblyVersion.Major, thisAssemblyVersion.Minor, assemblyCodeName.CodeName);         

            // If window needs to be pinned/unpinned
            if (showWindowBorder)
            {
                if (pinWindow)
                {
                    User32.SetWindowPos((uint)this.Window.Handle, -1, windowPositionX - 3, windowPositionY - 22, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 0);
                }
                else
                {
                    User32.SetWindowPos((uint)this.Window.Handle, 1, windowPositionX - 3, windowPositionY - 22, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 1);
                    User32.SetWindowPos((uint)this.Window.Handle, 0, windowPositionX - 3, windowPositionY - 22, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 1);
                }
            }
            else
            {
                if (pinWindow)
                {
                    User32.SetWindowPos((uint)this.Window.Handle, -1, windowPositionX, windowPositionY, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 1);
                }
                else
                {
                    User32.SetWindowPos((uint)this.Window.Handle, 1, windowPositionX, windowPositionY, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 1);
                    User32.SetWindowPos((uint)this.Window.Handle, 0, windowPositionX, windowPositionY, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 1);
                }
            }

            base.Initialize();

            keyboard.SetLayout();
            KeyboardLayoutReader.Purge();

            mouse.SetPosition(new Vector2(454 * 1.5f, 2));
        }


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backColor);

            spriteBatch.Begin();          

            base.Draw(gameTime);

            spriteBatch.End();
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            GlobalKeyboardService kservice = ServiceManager.Get<IKeyboardService>() as GlobalKeyboardService;
            kservice.Unhook();

            GlobalMouseService mservice = ServiceManager.Get<IMouseService>() as GlobalMouseService;
            mservice.Unhook();

            base.OnExiting(sender, args);
        }
    }
}