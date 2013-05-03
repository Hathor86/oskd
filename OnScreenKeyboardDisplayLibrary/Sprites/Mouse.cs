using Microsoft.Xna.Framework;
using XNATools.Engine2D;
using XNATools.Managers;
using XNATools.Services.Interfaces;
using Input = XNATools.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace OnScreenKeyboardDisplayLibrary.Sprites
{
    /// <summary>
    /// Class that draw the mouse
    /// </summary>
    public partial class Mouse : Sprite
    {
        #region Fields

        private IMouseService mouseService;
        private MouseButton[] buttons;
        private MousePositionContainer positionContainer;
        private MousePositionFollower positionFollower;

        #endregion

        #region cTor(s)

        /// <summary>
        /// Create a new mouse
        /// </summary>
        /// <param name="game">The game using this</param>
        public Mouse(Game game)
            : base(game, "MouseBase", Input.Layer.Artwork)
        {
            buttons = new MouseButton[5];
        }

        #endregion

        #region Methods

        /// <summary>
        /// Change sprite's position
        /// </summary>
        /// <param name="position">Position you want to draw</param>
        public void SetPosition(Vector2 position)
        {
            this.Position = position;

            buttons[0].Position = position;
            buttons[1].Position = position;
            buttons[2].Position = position;
            buttons[3].Position = position;
            buttons[4].Position = position;

            float positionX = this.Position.X + this.Center.X - positionContainer.Texture.Width / 2;
            float positionY = this.Position.Y + this.Center.Y - positionContainer.Texture.Height / 2;
            positionContainer.Position = new Vector2(positionX, positionY + this.Texture.Height / 3);

            positionX = positionContainer.Position.X + positionContainer.Center.X - positionFollower.Texture.Width / 2;
            positionY = positionContainer.Position.Y + positionContainer.Center.Y - positionFollower.Texture.Height / 2;
            positionFollower.Position = new Vector2(positionX, positionY);
        }

        public override void Initialize()
        {
            mouseService = ServiceManager.Get<IMouseService>();

            buttons[0] = new MouseButton(Game, Input.MouseButton.Left);
            buttons[1] = new MouseButton(Game, Input.MouseButton.Right);
            buttons[2] = new MouseButton(Game, Input.MouseButton.Middle);
            buttons[3] = new MouseButton(Game, Input.MouseButton.SideLeft);
            buttons[4] = new MouseButton(Game, Input.MouseButton.SideRight);

            positionContainer = new MousePositionContainer(Game);
            positionFollower = new MousePositionFollower(Game);

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
            positionFollower.Position = Vector2.Add(positionFollower.Position, Vector2.UnitX);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            //ServiceManager.Get<Microsoft.Xna.Framework.Graphics.SpriteBatch>().DrawString(Game.Content.Load<Microsoft.Xna.Framework.Graphics.SpriteFont>("Text"), mouseService.Offset.ToString(), Vector2.Zero, Color.White);            
        }

        #endregion
    }
}
