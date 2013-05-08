using Microsoft.Xna.Framework;
using XNATools.Engine2D;
using Input = XNATools.Enums;

namespace OnScreenKeyboardDisplayLibrary.Sprites
{
    public partial class Mouse : Sprite
    {
        private class MousePositionTracker : Sprite
        {
            #region Fields

            public Vector2 InitialPosition;

            #endregion

            #region cTor(s)

            public MousePositionTracker(Game game)
                : base(game, "Dot", Input.Layer.Artwork, 1)
            { }

            #endregion

            #region Methods

            public override void Initialize()
            {
                base.Initialize();               

                Scale = new Vector2(1.5f, 1.5f);
            }

            #endregion
        }
    }
}
