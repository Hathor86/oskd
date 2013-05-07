using Microsoft.Xna.Framework;
using XNATools.Engine2D;
using XNATools.Managers;
using XNATools.Services.Interfaces;
using Input = XNATools.Enums;
using System.Configuration;

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
        private MousePositionTracker positionFollower;

        private Vector2 followerPositionHelper;
        private int containerBound;

        #endregion

        #region cTor(s)

        /// <summary>
        /// Create a new mouse
        /// </summary>
        /// <param name="game">The game using this</param>
        public Mouse(Game game)
            : base(game, "MouseBase", Input.Layer.Artwork)
        {
            buttons = new MouseButton[7];
            followerPositionHelper = Vector2.Zero;
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
            buttons[5].Position = position;
            buttons[6].Position = new Vector2(buttons[5].Position.X, buttons[5].Position.Y + buttons[5].Bounds.Bottom);

            float positionX = this.Position.X + this.Center.X - positionContainer.Texture.Width / 2;
            float positionY = this.Position.Y + this.Center.Y - positionContainer.Texture.Height / 2;
            positionContainer.Position = new Vector2(positionX, positionY + this.Texture.Height / 3);

            positionX = positionContainer.Position.X + positionContainer.Center.X - (positionFollower.Texture.Width / 2f) - 1;
            positionY = positionContainer.Position.Y + positionContainer.Center.Y - (positionFollower.Texture.Height / 2f) - 1;
            positionFollower.InitialPosition = new Vector2(positionX, positionY);
        }

        private Color GetColor()
        {
            switch (ConfigurationManager.AppSettings["Color"])
            {
                case "Blue":
                    return Color.LightBlue;

                case "Green":
                    return Color.Green;

                case "Orange":
                    return Color.Orange;

                case "Pink":
                    return Color.Pink;

                case "Purple":
                    return Color.Purple;

                case "Red":
                    return Color.Red;

                case "Teal":
                    return Color.Teal;

                case "Yellow":
                    return Color.Yellow;

                default:
                    return Color.White;

            }
        }

        public override void Initialize()
        {
            mouseService = ServiceManager.Get<IMouseService>();

            buttons[0] = new MouseButton(Game, Input.MouseButton.Left);
            buttons[1] = new MouseButton(Game, Input.MouseButton.Right);
            buttons[2] = new MouseButton(Game, Input.MouseButton.Middle);
            buttons[3] = new MouseButton(Game, Input.MouseButton.SideLeft);
            buttons[4] = new MouseButton(Game, Input.MouseButton.SideRight);
            buttons[5] = new MouseButton(Game, Input.MouseButton.Middle);//Mousewheel up
            buttons[6] = new MouseButton(Game, Input.MouseButton.Middle);//Mousewheel down

            /*Remove mouswheel from game component
            We'll handle it manually*/
            Game.Components.Remove(buttons[5]);
            Game.Components.Remove(buttons[6]);

            buttons[5].Initialize();
            buttons[6].Initialize();

            positionContainer = new MousePositionContainer(Game);
            positionFollower = new MousePositionTracker(Game);
            positionContainer.Color = GetColor();
            positionFollower.Color = GetColor();

            base.Initialize();

            buttons[5].SetSource(new Rectangle(buttons[5].Source.X, buttons[5].Source.Y, buttons[5].Source.Width, 39));
            buttons[6].SetSource(new Rectangle(buttons[6].Source.X, buttons[6].Source.Y + 39, buttons[6].Source.Width, buttons[6].Source.Height - 39));

            Scale = new Vector2(1.5f, 1.5f);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            containerBound = (positionContainer.Bounds.Width / 2) - positionFollower.Bounds.Width;
            followerPositionHelper.X = MathHelper.Clamp(-mouseService.Offset.X, -containerBound, containerBound);
            followerPositionHelper.Y = MathHelper.Clamp(-mouseService.Offset.Y, -containerBound, containerBound);
            positionFollower.Position = Vector2.SmoothStep(positionFollower.Position, Vector2.Add(positionFollower.InitialPosition, followerPositionHelper), 0.5f);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (mouseService.MouseWheelOffset < 0)
            {
                buttons[5].Draw(gameTime);
            }

            if (mouseService.MouseWheelOffset > 0)
            {
                buttons[6].Draw(gameTime);
            }
        }

        #endregion
    }
}
