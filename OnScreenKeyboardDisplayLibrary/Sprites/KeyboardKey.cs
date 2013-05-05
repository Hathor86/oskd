using System.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNATools.Engine2D;
using XNATools.Enums;
using XNATools.Managers;
using XNATools.Services.Interfaces;

namespace OnScreenKeyboardDisplayLibrary.Sprites
{
    public partial class Keyboard : Sprite
    {
        /// <summary>
        /// Class that manage a keyboard key
        /// </summary>
        private class KeyboardKey : Sprite
        {
            #region Fields

            private const string HighlightTextureDirectory = "Keyboard";
            private static readonly string HighlightColor;
            private static readonly bool UseScrollLock;

            private static string _HighlightTexturePath;
            private static GlobalKeyboardService kService;
            private static ICacheManagerService<Texture2D> textureCacheService;

            private Keys _HandledKey;

            #endregion

            #region cTor(s)

            static KeyboardKey()
            {
                HighlightColor = ConfigurationManager.AppSettings["Color"];
                UseScrollLock = bool.Parse(ConfigurationManager.AppSettings["ScrollLock"]);
            }

            /// <summary>
            /// Create a new Keyboardkey
            /// </summary>
            /// <param name="game">The game</param>
            /// <param name="handledKey">The key that must be handled</param>
            public KeyboardKey(Game game, Keys handledKey)
                : base(game, "KeyboardBase", Layer.Highlight)
            {
                this._HandledKey = handledKey;
                _HighlightTexturePath = string.Format("{0}\\{1}", HighlightTextureDirectory, HighlightColor);
            }

            #endregion

            #region Methods

            /// <summary>
            /// Set the source rectangle into the texture
            /// </summary>
            /// <param name="source">The source rectangle</param>
            public void SetSource(Rectangle source)
            {
                Source = source;
            }

            /// <summary>
            /// Set the texture scale
            /// </summary>
            /// <param name="scale">Desired scale</param>
            public void SetScale(Vector2 scale)
            {
                Scale = scale;
            }

            public override void Initialize()
            {
                if (kService == null)
                {
                    kService = ServiceManager.Get<IKeyboardService>() as GlobalKeyboardService;
                }

                if (textureCacheService == null)
                {
                    textureCacheService = ServiceManager.Get<ICacheManagerService<Texture2D>>();
                }

                base.Initialize();
            }

            protected override void LoadContent()
            {
                base.LoadContent();
            }

            public override void Update(GameTime gameTime)
            {
                if (_HandledKey == Keys.F23)
                {
                    SwitchTexture(kService.IsNumLockOn);
                }
                else if (_HandledKey == Keys.F22)
                {
                    SwitchTexture(kService.IsCapsLockOn);
                }
                else if (_HandledKey == Keys.F21)
                {
                    SwitchTexture(kService.IsScrollLockOn);
                }
                else
                {
                    SwitchTexture(kService.IsKeyDown(_HandledKey));
                }
                base.Update(gameTime);
            }

            public override void Draw(GameTime gameTime)
            {
                //Force using of scroll key to handle
                if (UseScrollLock)
                {
                    if (System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.Scroll))
                    {
                        this.Enabled = true;
                    }
                    else
                    {
                        this.Enabled = false;
                    }
                }

                base.Draw(gameTime);
            }

            private void SwitchTexture(bool isHighlighted)
            {
                if (isHighlighted)
                {
                    Texture = textureCacheService[_HighlightTexturePath];
                }
                else
                {
                    Texture = textureCacheService["KeyboardBase"];
                }
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets or sets the key handled by
            /// this instance
            /// </summary>
            public Keys HandledKey
            {
                get
                {
                    return _HandledKey;
                }
                set
                {
                    _HandledKey = value;
                }
            }

            #endregion
        }
    }
}
