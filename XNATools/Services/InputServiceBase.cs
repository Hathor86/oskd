using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.Services.Interfaces;
using Microsoft.Xna.Framework;

namespace XNATools.Services
{
    /// <summary>
    /// Base class for InputServices
    /// </summary>
    public abstract class InputServiceBase:ServiceBase,IInputService
    {
        #region Fields
        #endregion

        #region cTor(s)

        /// <summary>
        /// New InputServiceBase
        /// <remarks>InputServices will be automatically registered into ServiceManager and Game.Components</remarks>
        /// </summary>        
        /// <param name="game">Game who hold the service</param>
        public InputServiceBase(Game game)
            : base(game) { }

        #endregion

        #region Methods
        /// <summary>
        /// <see cref="XNATools.Services.ServiceBase.IsThisServiceExistInManager"/>
        /// </summary>
        /// <returns></returns>
        protected override bool IsThisServiceExistInManager()
        {
            return IsThisInputServiceExistInManager();
        }
        /// <summary>
        /// <see cref="XNATools.Services.ServiceBase.RegisterService"/>
        /// </summary>
        /// <returns></returns>
        protected override void RegisterService()
        {
            RegisterInputService();
        }

        /// <summary>
        /// Method to determine if InputState has change
        /// </summary>
        /// <returns>True if state has changed; otherwise, false</returns>
        protected abstract bool ComputeStateHasChange();

        /// <summary>
        /// Method to determine if InputService is egistered into ServiceManager
        /// </summary>
        /// <returns>True if InputService has been registered; otherwise, false</returns>
        protected abstract bool IsThisInputServiceExistInManager();

        /// <summary>
        /// Method called to register InputService into ServiceManager
        /// </summary>
        protected abstract void RegisterInputService();

        #endregion

        #region Properties
        #endregion

        #region IInputService Members
        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IInputService.StateHasChanged"/>
        /// </summary>
        public bool StateHasChanged
        {
            get 
            {
                return ComputeStateHasChange();
            }
        }

        #endregion
    }
}
