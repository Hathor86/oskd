using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace XNATools.Services.Interfaces
{
    /// <summary>
    /// Not used for the moment
    /// </summary>
    public interface IAudioService:IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void Play(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="assetName"></param>
        void RegisterSoundEffect(string name, string assetName);
    }    
}
