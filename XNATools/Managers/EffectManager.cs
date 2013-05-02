using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using XNATools.Services.Interfaces;
using XNATools.Services;

namespace XNATools.Managers
{
    /// <summary>
    /// Not yet implemented
    /// </summary>
    public static class EffectManager
    {
        /*static Dictionary<string, Effect> effects = new Dictionary<string, Effect>();
        static Game game;
        public static Game Game
        {
            set { game = value; }
        }

        public static void Add(string key, Effect value)
        {
            effects.Add(key, value);
        }

        public static void AddTexture(string key, string assetName)
        {
            ICameraService camera = ServiceManager.Get<ICameraService>();

            BasicEffect effect = new BasicEffect(game.GraphicsDevice, null);
            effect.Projection = camera.Projection;
            effect.View = camera.View;
            effect.LightingEnabled = false;
            effect.VertexColorEnabled = true;
            effect.TextureEnabled = true;
            effect.Texture = game.Content.Load<Texture2D>(assetName);

            effects.Add(key, effect);
        }

        public static Effect Get(string key)
        {
            return effects[key];
        }*/
    }
}
