using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNATools.Enums;
using XNATools.Font;
using Microsoft.Xna.Framework.Graphics;

namespace XNATools.Diagnostics
{
    /// <summary>
    /// Class for holding debug data
    /// </summary>
    public sealed class DebugFrame : DrawableGameComponent
    {
        #region Fields

        private FrameDisplay _Display = FrameDisplay.All;
        private Vector2 _Position = Vector2.Zero;
        private FrameRateCounter _FPS;
        private Dictionary<string, FontInstance> _DebugData = new Dictionary<string, FontInstance>();
        private Vector2 nextFreePosition = Vector2.Zero;

        #endregion

        #region cTor(s)

        /// <summary>
        /// New debug frame
        /// </summary>
        /// <param name="game">Game to hold this frame</param>
        /// <param name="position">Frame Position</param>
        /// <param name="display">What you to be displayed</param>
        /// <param name="fontColor">Text color</param>
        public DebugFrame(Game game, Vector2 position, FrameDisplay display, Color fontColor)
            : base(game)
        {
            _FPS = new FrameRateCounter(game);
            _FPS.Position = position;
            _FPS.Font.TextColor = fontColor;
            _Display = display;
            DrawOrder = int.MaxValue;
            game.Components.Add(this);
        }

        /// <summary>
        /// New debug frame
        /// </summary>
        /// <param name="game">Game to hold this frame</param>
        public DebugFrame(Game game)
            : this(game, Vector2.Zero, FrameDisplay.All, Color.White)
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
        /// <see cref="Microsoft.Xna.Framework.DrawableGameComponent.Draw"/>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            _FPS.Visible = (_Display & FrameDisplay.FPS) == FrameDisplay.FPS;
            foreach (FontInstance font in _DebugData.Values)
            {
                font.Visible = (_Display & FrameDisplay.Custom) == FrameDisplay.Custom;
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// Add an object to the list
        /// </summary>
        /// <param name="objectName">Text to add</param>
        public void AddDebugObject(string objectName)
        {
            _DebugData.Add(objectName, new FontInstance(Game, "DebugFont", string.Empty,_FPS.Font.TextColor));
            _DebugData[objectName].Initialize();
            _DebugData[objectName].DrawOrder = int.MaxValue;
            nextFreePosition.Y += _DebugData[objectName].SpriteFont.MeasureString("DATA").Y;
            _DebugData[objectName].Position = nextFreePosition;
        }

        /// <summary>
        /// Add data to specified object
        /// </summary>
        /// <param name="key">Object name</param>
        /// <param name="data">DEbug data</param>
        public void AddDebugObjectData(string key, string data)
        {
            if (_DebugData.ContainsKey(key))
            {
                _DebugData[key].Text = _DebugData[key].Text.Replace(Environment.NewLine, ", ");
                _DebugData[key].Text = string.Format("{0}{1}{2}", _DebugData[key].Text, data, Environment.NewLine);
            }
            else
                throw new IndexOutOfRangeException(string.Format("Debug Object {0} not found", key));
        }

        /// <summary>
        /// Replace debug data in the specified object
        /// </summary>
        /// <param name="key">Object name</param>
        /// <param name="data">Debug data</param>
        public void SetDebugObjectData(string key, string data)
        {
            if (_DebugData.ContainsKey(key))
            {
                _DebugData[key].Text = string.Format("{0}{1}", data, Environment.NewLine);
            }
            else
                throw new IndexOutOfRangeException(string.Format("Debug Object {0} not found", key));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets debug data to specified object
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                if (_DebugData.ContainsKey(key))
                    return _DebugData[key].Text;
                else
                    throw new IndexOutOfRangeException(string.Format("Debug Object {0} not found", key));
            }
            set
            {
                SetDebugObjectData(key, value);
            }
        }

        #endregion
    }
}
