using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNATools.Font;
using XNATools.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace XNATools.Diagnostics
{
    /// <summary>
    /// Class for holding FPS frame
    /// </summary>
    public sealed class FrameRateCounter:DrawableGameComponent
    {
        #region Fields

        private const string FPSString = "{0} FPS";

        internal FontInstance Font;
        private int _FrameCount = 0;        
        private int _FrameRate = 0;
        private TimeSpan _ElapsedTime = TimeSpan.Zero;

        private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);

        #endregion

        #region cTor(s)

        /// <summary>
        /// Create a display for FPS
        /// </summary>
        /// <param name="game">The game</param>
        public FrameRateCounter(Game game)
            :base(game)
        {
            Font = new FontInstance(game, "DebugFont", string.Empty);
            Font.Position = new Vector2(0, 0);
            Font.DrawOrder = int.MaxValue;
            game.Components.Add(this);            
        }

        #endregion

        #region Methods

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.GameComponent.Update"/>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _ElapsedTime += gameTime.ElapsedGameTime;

            if (_ElapsedTime >= OneSecond)
            {
                _ElapsedTime -= OneSecond;
                _FrameRate = _FrameCount;
                _FrameCount = 0;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.DrawableGameComponent.Draw"/>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            _FrameCount++;
           Font.Text = string.Format(FPSString, _FrameRate);            
            base.Draw(gameTime);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets FPS frame Position
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return Font.Position;
            }
            set
            {
                Font.Position = value;
            }
        }

        /// <summary>
        /// Gets or sets visibility
        /// </summary>
        new public bool Visible
        {
            get
            {
                return Font.Visible;
            }
            set
            {
                base.Visible = value;
                Font.Visible = value;                
            }
        }

        #endregion
    }
}
