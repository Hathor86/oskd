using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNATools.Engine2D;
using XNATools.Enums;
using System;

namespace XNATools.Engine2D
{
    /// <summary>
    /// Class for holding a background
    /// </summary>
    public class Background:Sprite
    {
        #region Fields

        private bool autoScale = false;

        #endregion

        #region cTor(s)

        /// <summary>
        /// New Background
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="assetName">Image name</param>
        /// <param name="autoScale">Determine if image must be scaled</param>
        public Background(Game game, string assetName, bool autoScale)
            : base(game, assetName, Layer.Background, 0, false)
        {
            this.autoScale = autoScale; 
        }

        /// <summary>
        /// New Background
        /// </summary>
        /// <param name="game">Game</param>
        /// <param name="assetName">Image name</param>
        public Background(Game game, string assetName)
            : this(game, assetName, false)
        {}

        #endregion

        #region Methods

        /// <summary>
        /// <see cref="XNATools.Engine2D.Sprite.Initialize"/>
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            if (autoScale)
            {
                Scale = Helper.ComputeScale(Texture.Width, Texture.Height, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
            }
        }
        
        #endregion
    }
}
