using System;
using System.Configuration;
using Microsoft.Xna.Framework;
using XNATools.Engine2D;
using XNATools.Managers;
using XNATools.Services.Interfaces;
using Input = XNATools.Enums;

namespace OnScreenKeyboardDisplayLibrary.Sprites
{
    public partial class Mouse : Sprite
    {
        /// <summary>
        /// Class that draw the highlight button
        /// </summary>
        private class MouseButton : Sprite
        {
            #region Fields

            private const string TextureDirectory = "Mouse";

            private static readonly string HighlightColor;
            
            private static IMouseService mService;            

            private Input.MouseButton _HandledButton;

            #endregion

            #region cTor(s)

            static MouseButton()
            {              
                HighlightColor = ConfigurationManager.AppSettings["Color"];            
            }

            /// <summary>
            /// Create a new Button
            /// </summary>
            /// <param name="game">Game using this</param>
            /// <param name="buttonHandled">Button handled by this sprite</param>
            public MouseButton(Game game, Input.MouseButton buttonHandled)
                : base(game, string.Empty, Input.Layer.Highlight)
            {
                _HandledButton = buttonHandled;
            }

            #endregion

            #region Methods

            public override void Initialize()
            {
                if (mService == null)
                {
                    mService = ServiceManager.Get<IMouseService>();
                }

                string assetName = string.Empty;

                switch (_HandledButton)
                {
                    case Input.MouseButton.Left:
                        assetName = "LMB";
                        break;

                    case Input.MouseButton.Middle:
                        assetName = "MMB";
                        break;

                    case Input.MouseButton.Right:
                        assetName = "RMB";
                        break;
                    case Input.MouseButton.SideLeft:
                        assetName = "X1";
                        break;

                    case Input.MouseButton.SideRight:
                        assetName = "X2";
                        break;

                    default:
                        throw new NotImplementedException("A new mouse button has been created and has not been handled");
                }

                AssetName = string.Format("{0}\\{1}\\{2}", TextureDirectory, HighlightColor, assetName);

                base.Initialize();

                SetDefault();

                Scale = new Vector2(1.5f, 1.5f);
            }

            protected override void LoadContent()
            {
                base.LoadContent();
            }

            public override void Update(GameTime gameTime)
            {
                if (mService.IsButtonDown(_HandledButton))
                {
                    Visible = true;
                }
                else
                {
                    Visible = false;
                }

                base.Update(gameTime);
            }

            #endregion

            #region Properties
            #endregion
        }
    }
}
