using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNATools.Services.Interfaces;
using XNATools.Managers;

namespace XNATools.Services
{
    /// <summary>
    /// Description for KeyboardService
    /// </summary>
    public sealed class KeyboardService : InputServiceBase, IKeyboardService
    {
        #region Fields

        private KeyboardState _State;
        private KeyboardState lastKBState;

        #endregion

        #region cTor(s)

        /// <summary>
        /// New KeyboardService
        /// </summary>
        /// <param name="game">Game who hold service</param>
        public KeyboardService(Game game)
            :base(game)
        {}

        #endregion

        #region Methods

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.GameComponent.Update"/>
        /// </summary>
        /// <param name="gameTime">Provide a snapshot for gametime</param>
        public override void Update(GameTime gameTime)
        {            
            lastKBState = _State;
            _State = Keyboard.GetState();                     
            base.Update(gameTime);
        }
        
        /// <summary>
        /// <see cref="XNATools.Services.InputServiceBase.ComputeStateHasChange"/>
        /// </summary>
        /// <returns></returns>
        protected override bool ComputeStateHasChange()
        {
            return lastKBState.GetPressedKeys().Count() != _State.GetPressedKeys().Count();
        }

        /// <summary>
        /// <see cref="XNATools.Services.InputServiceBase.IsThisInputServiceExistInManager"/>
        /// </summary>
        /// <returns></returns>
        protected override bool IsThisInputServiceExistInManager()
        {
            return ServiceManager.Exists<IKeyboardService>(this);
        }

        /// <summary>
        /// <see cref="XNATools.Services.InputServiceBase.RegisterInputService"/>
        /// </summary>        
        protected override void RegisterInputService()
        {
            ServiceManager.Add<IKeyboardService>(this);
        }

        #endregion

        #region Properties

        #endregion

        #region IKeyBoardService Membres
        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IKeyboardService.State"/>
        /// </summary>
        public KeyboardState State
        {
            get
            {
                return _State;
            }
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IKeyboardService.PressedKeys"/>
        /// </summary>
        public Keys[] PressedKeys
        {
            get
            {
                return _State.GetPressedKeys();
            }
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IKeyboardService.IsKeyDown"/>
        /// </summary>
        public bool IsKeyDown(Keys key)
        {
            return _State.IsKeyDown(key);
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IKeyboardService.IsKeyUp"/>
        /// </summary>
        public bool IsKeyUp(Keys key)
        {
            return _State.IsKeyUp(key);
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IKeyboardService.KeyPressed"/>
        /// </summary>
        public bool KeyPressed(Keys key)
        {
            return lastKBState.IsKeyDown(key) && _State.IsKeyDown(key);
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IKeyboardService.KeyReleased"/>
        /// </summary>
        public bool KeyReleased(Keys key)
        {
            return lastKBState.IsKeyUp(key) && _State.IsKeyUp(key);
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IKeyboardService.KeyIsJustPressed"/>
        /// </summary>
        public bool KeyIsJustPressed(Keys key)
        {
            return lastKBState.IsKeyUp(key) && _State.IsKeyDown(key);
        }

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IKeyboardService.KeyIsJustReleased"/>
        /// </summary>
        public bool KeyIsJustReleased(Keys key)
        {
            return lastKBState.IsKeyDown(key) && _State.IsKeyUp(key);
        }

        #endregion        
    }
}
