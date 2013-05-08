using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNATools.Services.Interfaces;
using XNATools.Managers;

namespace XNATools.Services
{
    /// <summary>
    /// Base class for all services
    /// </summary>
    public abstract class ServiceBase : GameComponent, IService
    {
        #region Fields
        #endregion

        #region cTor(s)

        /// <summary>
        /// Create a Service
        /// </summary>
        /// <param name="game">Game who hold the service</param>
        /// <param name="addInComponent">Define if service must be added to Game.Component</param>
        public ServiceBase(Game game, bool addInComponent)
            :base(game)
        {
            if (addInComponent)
            {
                game.Components.Add(this);
            }
            RegisterService();
        }
        /// <summary>
        /// Create a Service
        /// </summary>
        /// <param name="game">Game who hold the service</param>
        public ServiceBase(Game game)
            :this(game,true)
        {}

        #endregion

        #region Methods

        /// <summary>
        /// Check if this service has been registered into the ServiceManager
        /// </summary>
        /// <returns>True if registered; otherwise, false</returns>
        protected abstract bool IsThisServiceExistInManager();
        /// <summary>
        /// Register service to the manager
        /// </summary>
        protected abstract void RegisterService();

        #endregion

        #region Properties
        #endregion

        #region IService Members

        /// <summary>
        /// <see cref="XNATools.Services.Interfaces.IService.ExistInManager"/>
        /// </summary>
        public bool ExistInManager
        {
            get 
            {
                return IsThisServiceExistInManager();
            }
        }

        #endregion
    }
}
