using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNATools.Services.Interfaces;
using XNATools.Sounds;
using XNATools.Managers;

namespace XNATools.Services
{
    /// <summary>
    /// Not yet implemented
    /// </summary>
    public sealed class AudioService : GameComponent//, IAudioService
    {
        private Dictionary<string, AudioGameComponent> _EffectLibrary;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        [Obsolete]
        public AudioService(Game game)
            : base(game)
        {            
            //ServiceManager.Add<IAudioService>(this);
            game.Components.Add(this);

            _EffectLibrary = new Dictionary<string, AudioGameComponent>();
        }

        #region IAudioService Membres

        /// <summary>
        /// Register a sound effect that will be played by In-Game asset name
        /// </summary>
        /// <param name="name">ex: "EXPLOSION"</param>
        /// <param name="assetName">ex: "explosion_car_very_loud"</param>
        public void RegisterSoundEffect(string name, string assetName)
        {
            _EffectLibrary[name] = new AudioGameComponent(Game, assetName);
        }

        /// <summary>
        /// Play a sound by In-Game asset name (ex: Play("EXPLOSION");)
        /// </summary>
        /// <param name="name">In-Game asset name</param>
        public void Play(string name)
        {
            if (_EffectLibrary.ContainsKey(name))
                _EffectLibrary[name].Play();
            else
                throw new IndexOutOfRangeException(string.Format("Sound : {0} not registered", name));
        }

        #endregion
    }
}
