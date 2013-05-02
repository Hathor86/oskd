using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using OnScreenKeyboardDisplayLibrary.Configuration.Reader;
using XNATools.Engine2D;
using XNATools.Enums;

namespace OnScreenKeyboardDisplayLibrary.Sprites
{
    /// <summary>
    /// Class that draw the keyboard
    /// </summary>
    public partial class Keyboard : Sprite
    {
        #region Fields                

        private List<KeyboardKey> _Keys = new List<KeyboardKey>();

        #endregion

        #region cTor(s)

        /// <summary>
        /// Create a new keyboard sprite
        /// </summary>
        /// <param name="game">Game who use the sprite</param>
        public Keyboard(Game game)
            : base(game, "KeyboardBase", Layer.Artwork)
        { }

        #endregion

        #region Methods

        /// <summary>
        /// Set the keyboardlayout based on the config file
        /// </summary>
        public void SetLayout()
        {
            Rectangle rect;
            foreach (KeyboardKey key in _Keys)
            {
                rect = KeyboardLayoutReader.GetKeyConfig(key.HandledKey)[0];

                key.SetSource(rect);

                rect = KeyboardLayoutReader.GetKeyConfig(key.HandledKey)[1];

                key.Position = new Vector2(rect.X * Scale.X, rect.Y * Scale.Y);

                if (key.HandledKey == Keys.F24)
                {
                    key.HandledKey = Keys.Enter;
                }
                key.SetScale(this.Scale);
            }
        }

        public override void Initialize()
        {           
            foreach (Keys key in KeyboardLayoutReader.GetAllKeys())
            {
                KeyboardKey k = new KeyboardKey(Game, key);
                _Keys.Add(k);
            }

            base.Initialize();

            Scale = new Vector2(1.5f,1.5f);
        }

        public override void Draw(GameTime gameTime)
        {}

        #endregion
    }
}
