using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using XNATools.Services.Interfaces;
using XNATools.Managers;
using XNATools.Enums;

namespace XNATools.Services
{
    /// <summary>
    /// Class for MouseService
    /// </summary>
    public sealed class MouseService : InputServiceBase, IMouseService
    {
        #region Fields

        private MouseState _State;
        private Vector2 _Position = Vector2.One;
        private Vector2 _Offset = Vector2.Zero;
        private Vector2 _OffsetFromCenter = Vector2.Zero;
        private MouseState centerState;        // Mouse State when the cursor is in the middle of the screen        
        private MouseState lastState;

        #endregion

        #region cTor(s)

        /// <summary>
        /// New MouseService
        /// </summary>
        public MouseService(Game game)
            : base(game)
        { }

        #endregion

        #region Methods

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.GameComponent.Initialize"/>
        /// </summary>
        public override void Initialize()
        {
            Center();
            centerState = Mouse.GetState();
            lastState = Mouse.GetState();
            base.Initialize();
        }

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.GameComponent.Update"/>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            lastState = _State;
            _State = Mouse.GetState();

            _Position.X = _State.X;
            _Position.Y = _State.Y;
            _Offset.X = lastState.X - _State.X;
            _Offset.Y = lastState.Y - _State.Y;
            _OffsetFromCenter.X = centerState.X - _State.X;
            _OffsetFromCenter.Y = centerState.Y - _State.Y;

            base.Update(gameTime);
        }

        /// <summary>
        /// <see cref="XNATools.Services.InputServiceBase.ComputeStateHasChange"/>
        /// </summary>
        /// <returns></returns>
        protected override bool ComputeStateHasChange()
        {
            return _Offset != Vector2.Zero
                || IsButtonJustPressed(MouseButton.Left)
                || IsButtonJustPressed(MouseButton.Right)
                || IsButtonJustPressed(MouseButton.Middle)
                || IsButtonJustPressed(MouseButton.SideLeft)
                || IsButtonJustPressed(MouseButton.SideRight)              
                || IsButtonJustReleased(MouseButton.Left)
                || IsButtonJustReleased(MouseButton.Right)
                || IsButtonJustReleased(MouseButton.Middle)
                || IsButtonJustReleased(MouseButton.SideLeft)
                || IsButtonJustReleased(MouseButton.SideRight)               
                || MouseWheelOffset != 0;
        }

        /// <summary>
        /// <see cref="XNATools.Services.InputServiceBase.IsThisInputServiceExistInManager"/>
        /// </summary>
        /// <returns></returns>
        protected override bool IsThisInputServiceExistInManager()
        {
            return ServiceManager.Exists<IMouseService>(this);
        }

        /// <summary>
        /// <see cref="XNATools.Services.InputServiceBase.RegisterInputService"/>
        /// </summary>        
        protected override void RegisterInputService()
        {
            ServiceManager.Add<IMouseService>(this);
        }

        #endregion

        #region Properties

        #endregion

        #region IMouseService Members

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IMouseService.State"/>
        /// </summary>
        public MouseState State
        {
            get
            {
                return _State;
            }
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IMouseService.Position"/>
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return _Position;
            }
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IMouseService.Offset"/>
        /// </summary>
        public Vector2 Offset
        {
            get
            {
                return _Offset;
            }
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IMouseService.OffsetFromCenter"/>
        /// </summary>
        public Vector2 OffsetFromCenter
        {
            get
            {
                return _OffsetFromCenter;
            }
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IMouseService.MouseWheelOffset"/>
        /// </summary>
        public int MouseWheelOffset
        {
            get
            {
                return lastState.ScrollWheelValue - _State.ScrollWheelValue;
            }
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IMouseService.Center"/>
        /// </summary>
        public void Center()
        {
            Mouse.SetPosition((int)ScreenManager.ScreenCenter.X, (int)ScreenManager.ScreenCenter.Y);
            _Position = ScreenManager.ScreenCenter;
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IMouseService.IsButtonDown"/>
        /// </summary>
        public bool IsButtonDown(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return _State.LeftButton == ButtonState.Pressed;

                case MouseButton.Right:
                    return _State.RightButton == ButtonState.Pressed;

                case MouseButton.Middle:
                    return _State.MiddleButton == ButtonState.Pressed;

                case MouseButton.SideLeft:
                    return _State.XButton1 == ButtonState.Pressed;

                case MouseButton.SideRight:
                    return _State.XButton2 == ButtonState.Pressed;               

                default:
                    throw new NotImplementedException("A new mouse button has been created and has not been handled");
            }
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IMouseService.IsButtonUp"/>
        /// </summary>
        public bool IsButtonUp(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return _State.LeftButton == ButtonState.Released;

                case MouseButton.Right:
                    return _State.RightButton == ButtonState.Released;

                case MouseButton.Middle:
                    return _State.MiddleButton == ButtonState.Released;

                case MouseButton.SideLeft:
                    return _State.XButton1 == ButtonState.Released;

                case MouseButton.SideRight:
                    return _State.XButton2 == ButtonState.Released;

                default:
                    throw new NotImplementedException("A new mouse button has been created and has not been handled");

            }
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IMouseService.IsButtonPressed"/>
        /// </summary>
        public bool IsButtonPressed(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return lastState.LeftButton == ButtonState.Pressed && _State.LeftButton == ButtonState.Pressed;

                case MouseButton.Right:
                    return lastState.RightButton == ButtonState.Pressed && _State.RightButton == ButtonState.Pressed;

                case MouseButton.Middle:
                    return lastState.MiddleButton == ButtonState.Pressed && _State.MiddleButton == ButtonState.Pressed;

                case MouseButton.SideLeft:
                    return lastState.XButton1 == ButtonState.Pressed && _State.XButton1 == ButtonState.Pressed;

                case MouseButton.SideRight:
                    return lastState.XButton2 == ButtonState.Pressed && _State.XButton2 == ButtonState.Pressed;

                default:
                    throw new NotImplementedException("A new mouse button has been created and has not been handled");
            }
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IMouseService.IsButtonReleased"/>
        /// </summary>
        public bool IsButtonReleased(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return lastState.LeftButton == ButtonState.Released && _State.LeftButton == ButtonState.Released;

                case MouseButton.Right:
                    return lastState.RightButton == ButtonState.Released && _State.RightButton == ButtonState.Released;

                case MouseButton.Middle:
                    return lastState.MiddleButton == ButtonState.Released && _State.MiddleButton == ButtonState.Released;

                case MouseButton.SideLeft:
                    return lastState.XButton1 == ButtonState.Released && _State.XButton1 == ButtonState.Released;

                case MouseButton.SideRight:
                    return lastState.XButton2 == ButtonState.Released && _State.XButton2 == ButtonState.Released;

                default:
                    throw new NotImplementedException("A new mouse button has been created and has not been handled");

            }
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IMouseService.IsButtonJustPressed"/>
        /// </summary>
        public bool IsButtonJustPressed(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return lastState.LeftButton == ButtonState.Released && _State.LeftButton == ButtonState.Pressed;

                case MouseButton.Right:
                    return lastState.RightButton == ButtonState.Released && _State.RightButton == ButtonState.Pressed;

                case MouseButton.Middle:
                    return lastState.MiddleButton == ButtonState.Released && _State.MiddleButton == ButtonState.Pressed;

                case MouseButton.SideLeft:
                    return lastState.XButton1 == ButtonState.Released && _State.XButton1 == ButtonState.Pressed;

                case MouseButton.SideRight:
                    return lastState.XButton2 == ButtonState.Released && _State.XButton2 == ButtonState.Pressed;

                default:
                    throw new NotImplementedException("A new mouse button has been created and has not been handled");

            }
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IMouseService.IsButtonJustReleased"/>
        /// </summary>
        public bool IsButtonJustReleased(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return lastState.LeftButton == ButtonState.Pressed && _State.LeftButton == ButtonState.Released;

                case MouseButton.Right:
                    return lastState.RightButton == ButtonState.Pressed && _State.RightButton == ButtonState.Released;

                case MouseButton.Middle:
                    return lastState.MiddleButton == ButtonState.Pressed && _State.MiddleButton == ButtonState.Released;

                case MouseButton.SideLeft:
                    return lastState.XButton1 == ButtonState.Pressed && _State.XButton1 == ButtonState.Released;

                case MouseButton.SideRight:
                    return lastState.XButton2 == ButtonState.Pressed && _State.XButton2 == ButtonState.Released;

                default:
                    throw new NotImplementedException("A new mouse button has been created and has not been handled");

            }
        }

        #endregion
    }
}
