using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace XNATools.Sounds
{
    /// <summary>
    /// Not yet implemented
    /// </summary>
    public sealed class AudioGameComponent : GameComponent
    {
        private SoundEffect effect;
        private string assetName;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="name"></param>
        public AudioGameComponent(Game game, string name)
            : base(game)
        {
            assetName = name;

            // for the time being, immediately load the asset
            effect = game.Content.Load<SoundEffect>(assetName);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Play()
        {
            if (effect == null)
                effect = Game.Content.Load<SoundEffect>(assetName);

            effect.Play();
        }
    }    
}
