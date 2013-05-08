using Microsoft.Xna.Framework;
using XNATools.Engine2D;
using Input = XNATools.Enums;

namespace OnScreenKeyboardDisplayLibrary.Sprites
{
    public partial class Mouse : Sprite
    {
        private class MousePositionContainer : Sprite
        {
            #region Fields
            #endregion

            #region cTor(s)

            public MousePositionContainer(Game game)
                : base(game, "Circle", Input.Layer.Artwork, 1)
            { }

            #endregion

            #region Methods

            public override void Initialize()
            {
                base.Initialize();
                ForceTransformBounds();
            }

            #endregion
        }
    }
}
