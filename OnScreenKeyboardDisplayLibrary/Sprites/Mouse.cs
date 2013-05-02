using Microsoft.Xna.Framework;
using XNATools.Engine2D;
using XNATools.Managers;
using XNATools.Services.Interfaces;
using Input = XNATools.Enums;

namespace OnScreenKeyboardDisplayLibrary.Sprites
{
    /// <summary>
    /// Class that draw the mouse
    /// </summary>
    public partial class Mouse : Sprite
    {
        #region Fields

        private IMouseService mouseService;
        private MouseButton[] _buttons;

        #endregion

        #region cTor(s)

        /// <summary>
        /// Create a new mouse
        /// </summary>
        /// <param name="game">The game using this</param>
        public Mouse(Game game)
            : base(game, "MouseBase", Input.Layer.Artwork)
        {
            _buttons = new MouseButton[5];
        }

        #endregion

        #region Methods

        /// <summary>
        /// Change sprite's position
        /// </summary>
        /// <param name="position">Position you want to draw</param>
        public void SetPosition(Vector2 position)
        {
            Position = position;

            _buttons[0].Position = position;
            _buttons[1].Position = position;
            _buttons[2].Position = position;
            _buttons[3].Position = position;
            _buttons[4].Position = position;            
        }        

        public override void Initialize()
        {
            mouseService = ServiceManager.Get<IMouseService>();

            _buttons[0] = new MouseButton(Game, Input.MouseButton.Left);
            _buttons[1] = new MouseButton(Game, Input.MouseButton.Right);
            _buttons[2] = new MouseButton(Game, Input.MouseButton.Middle);
            _buttons[3] = new MouseButton(Game, Input.MouseButton.SideLeft);
            _buttons[4] = new MouseButton(Game, Input.MouseButton.SideRight);

            base.Initialize();

            SetDefault();

            Scale = new Vector2(1.5f, 1.5f);           
        }

        #endregion
    }
}
