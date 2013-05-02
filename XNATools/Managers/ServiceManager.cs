using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNATools.Services.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace XNATools.Managers
{
    /// <summary>
    /// Class for using Game.Service much easier
    /// </summary>
    public static class ServiceManager
    {
        #region Fields
        /// <summary>
        /// Game who hold service
        /// <remarks>Don't forget to set it !!</remarks>
        /// </summary>
        public static Game Game;

        #endregion

        #region cTor(s)
        #endregion

        #region Methods

        /// <summary>
        /// Get Specified service type
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <returns>The service</returns>
        public static T Get<T>()
        {
            return (T)Game.Services.GetService(typeof(T));
        }

        /// <summary>
        /// Add specified service
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <param name="service">The service</param>
        public static void Add<T>(IService service)
        {
            Game.Services.AddService(typeof(T), service);
        }

        /// <summary>
        /// Add Specified spritebatch to service
        /// </summary>
        /// <param name="spriteBatch">The spritebatch</param>
        public static void AddSpriteBatch(SpriteBatch spriteBatch)
        {
            Game.Services.AddService(typeof(SpriteBatch), spriteBatch);
        }       

        /// <summary>
        /// Remove service
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        public static void Remove<T>()
        {
            Game.Services.RemoveService(typeof(T));
        }

        /// <summary>
        /// Determine if specified exists
        /// </summary>
        /// <typeparam name="T">Service type</typeparam>
        /// <param name="service">The service</param>
        /// <returns>True if service is</returns>
        public static bool Exists<T>(IService service)
        {
            return Game.Services.GetService(typeof(T)) != null;
        }

        #endregion

        #region Properties
        #endregion
    }
}
