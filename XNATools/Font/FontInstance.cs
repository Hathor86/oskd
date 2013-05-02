using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNATools.Services.Interfaces;
using XNATools.Services;
using XNATools.Managers;

namespace XNATools.Font
{
    /// <summary>
    /// Class for holding fonts
    /// This class cannot be inherited
    /// </summary>
    public sealed class FontInstance : DrawableGameComponent
    {
        #region Fields

        private string _Text = string.Empty;
        private string _FontName;
        private Vector2 _Position;
        private Color _TextColor = Color.White;
        internal SpriteFont SpriteFont;

        private static SpriteBatch _SpriteBatch;

        #endregion

        #region Constructors

        /// <summary>
        /// New FontInstance
        /// </summary>
        /// <param name="game">For this specified game</param>
        /// <param name="fontName">Font name</param>
        /// <param name="text">Text to display</param>
        /// <param name="fontColor">Text Color</param>
        public FontInstance(Game game, string fontName, string text, Color fontColor)
            : base(game)
        {
            _FontName = fontName;
            _Text = text;
            _TextColor = fontColor;
            if (_SpriteBatch == null)
            {
                _SpriteBatch = ServiceManager.Get<SpriteBatch>();
            }
            game.Components.Add(this);
        }

        /// <summary>
        /// New FontInstance
        /// </summary>
        /// <param name="game">For this specified game</param>
        /// <param name="fontName">Font name</param>
        /// <param name="text">Text to display</param>
        public FontInstance(Game game, string fontName, string text)
            : this(game,fontName,text,Color.White)
        { }

        #endregion

        #region Methods

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.GameComponent.Initialize"/>
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.DrawableGameComponent.LoadContent"/>
        /// </summary>
        protected override void LoadContent()
        {
            if (_FontName != null)
            {
                ICacheManagerService<SpriteFont> service = (ICacheManagerService<SpriteFont>)Game.Services.GetService(typeof(ICacheManagerService<SpriteFont>));
                if (service == null)
                {
                    SpriteFont = Game.Content.Load<SpriteFont>(_FontName);
                }
                else
                {
                    SpriteFont = service.Get(_FontName);
                }
            }
        }

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.DrawableGameComponent.Draw"/>
        /// </summary>
        public override void Draw(GameTime gameTime)
        {            
            _SpriteBatch.DrawString(SpriteFont, _Text, _Position, _TextColor);
            base.Draw(gameTime);
        }

        /// <summary>
        /// Set all properties at once
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="fontName">Font name</param>
        /// <param name="x">X-Position</param>
        /// <param name="y">Y-Position</param>
        public void SetProperties(string text, string fontName, float x, float y)
        {
            SetPosition(x, y);
            _Text = text;
            _FontName = fontName;
        }

        /// <summary>
        /// Change Text position
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public void SetPosition(float x, float y)
        {
            if (_Position == null)
                _Position = new Vector2(x, y);
            else
            {
                _Position.X = x;
                _Position.Y = y;
            }
        }        

        #endregion

        #region Properties

        /// <summary>
        /// Gets or Sets Text
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        /// <summary>
        /// Gets Text width
        /// </summary>      
        public float TextWidth
        {
            get
            {
                return SpriteFont.MeasureString(_Text).X;
            }
        }

        /// <summary>
        /// Gets Text height
        /// </summary>      
        public float TextHeight
        {
            get
            {
                return SpriteFont.MeasureString(_Text).Y;
            }
        }

        /// <summary>
        /// Gets Font
        /// </summary>
        public string FontName
        {
            get
            {
                return _FontName;
            }
        }

        /// <summary>
        /// Gets or Sets Text position
        /// </summary>
        public Vector2 Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        /// <summary>
        /// Gets or Sets Text Color
        /// </summary>
        public Color TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; }
        }

        #endregion
    }
}
