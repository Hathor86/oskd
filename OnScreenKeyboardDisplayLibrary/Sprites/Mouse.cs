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
            /*buttons[5].Position = position;
            buttons[6].Position = position;*/

            float positionX = this.Position.X + this.Center.X - positionContainer.Texture.Width / 2;
            float positionY = this.Position.Y + this.Center.Y - positionContainer.Texture.Height / 2;
            positionContainer.Position = new Vector2(positionX, positionY + this.Texture.Height / 3);

            positionX = positionContainer.Position.X + positionContainer.Center.X - (positionFollower.Texture.Width / 2);
            positionY = positionContainer.Position.Y + positionContainer.Center.Y - (positionFollower.Texture.Height / 2);
            positionFollower.InitialPosition = new Vector2(positionX, positionY);
        }

        public override void Initialize()
        {
            mouseService = ServiceManager.Get<IMouseService>();

            buttons[0] = new MouseButton(Game, Input.MouseButton.Left);
            buttons[1] = new MouseButton(Game, Input.MouseButton.Right);
            buttons[2] = new MouseButton(Game, Input.MouseButton.Middle);
            buttons[3] = new MouseButton(Game, Input.MouseButton.SideLeft);
            buttons[4] = new MouseButton(Game, Input.MouseButton.SideRight);
            /*buttons[5] = new MouseButton(Game, Input.MouseButton.Middle);//Mousewheel up
            buttons[6] = new MouseButton(Game, Input.MouseButton.Middle);//Mousewheel down*/

            /*Remove mouswheel from game component
            We'll handle it manually*/
            //Game.Components.Remove(buttons[5]);
            //Game.Components.Remove(buttons[6]);

            /*buttons[5].Initialize();
            buttons[6].Initialize();
            buttons[5].Enabled = false;
            buttons[6].Enabled = false;*/

            positionContainer = new MousePositionContainer(Game);
            positionFollower = new MousePositionFollower(Game);

            base.Initialize();

            /*buttons[5].SetSource(new Rectangle(buttons[5].Source.X, buttons[5].Source.Y, buttons[5].Source.Width, 39));
            buttons[6].SetSource(new Rectangle(buttons[6].Source.X, buttons[6].Source.Y + 39, buttons[6].Source.Width, buttons[6].Source.Height - 39));*/

            SetDefault();

            Scale = new Vector2(1.5f, 1.5f);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            positionFollower.Position = Vector2.Add(positionFollower.InitialPosition, -mouseService.Offset);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            //ServiceManager.Get<Microsoft.Xna.Framework.Graphics.SpriteBatch>().Draw(buttons[5].Texture, buttons[5].Position, buttons[5].Source, Color.White,0,Vector2.Zero,3f,Effect,0f);

            /*if (mouseService.MouseWheelOffset < 0)
            {
                buttons[5].Draw(gameTime);
            }

            if (mouseService.MouseWheelOffset > 0)
            {
                buttons[6].Draw(gameTime);
            }*/
        }

        #endregion
    }
}
