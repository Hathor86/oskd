using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using XNATools.Managers;


namespace XNATools
{
    /// <summary>
    /// This is a collection of game components
    /// </summary>
    public class Scene : DrawableGameComponent, ICollection<GameComponent>
    {
        #region Fields

        private List<GameComponent> _Innerlist = new List<GameComponent>();

        #endregion

        #region cTor(s)

        /// <summary>
        /// New GameScene
        /// </summary>
        /// <param name="game">The game who contains the scene</param>
        /// <param name="sceneKey">Scene's name</param>
        public Scene(Game game, string sceneKey)
            : base(game)
        {            
            Visible = false;
            Enabled = false;
            ScreenManager.AddScene(sceneKey, this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Show the scene
        /// </summary>
        public virtual void Show()
        {
            Visible = true;
            Enabled = true;
        }

        /// <summary>
        /// Hide the scene
        /// </summary>
        public virtual void Hide()
        {
            Visible = false;
            Enabled = false;
        }


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _Innerlist.Count; i++)
            {
                if (_Innerlist[i].Enabled)
                    _Innerlist[i].Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.DrawableGameComponent.Draw"/>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < _Innerlist.Count; i++)
            {
                GameComponent gc = _Innerlist[i];
                if (gc is DrawableGameComponent && ((DrawableGameComponent)gc).Visible)
                    ((DrawableGameComponent)gc).Draw(gameTime);
            }
            
            base.Draw(gameTime);
        }

        #endregion

        #region ICollection<GameComponent> Membres

        /// <summary>
        /// Add specified item to scene
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Add(GameComponent item)
        {
            _Innerlist.Add(item);
        }

        /// <summary>
        /// Remove all components
        /// </summary>
        public void Clear()
        {
            _Innerlist.Clear();
        }

        /// <summary>
        /// Determine whether specified item exists in scene
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>True if specified item exists; otherwise, false</returns>
        public bool Contains(GameComponent item)
        {
            return _Innerlist.Contains(item);
        }

        /// <summary>
        /// Copy current scene to specified array
        /// </summary>
        /// <param name="array">Array to fill in</param>
        /// <param name="arrayIndex">start index</param>
        public void CopyTo(GameComponent[] array, int arrayIndex)
        {
            _Innerlist.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of current items in scene
        /// </summary>
        public int Count
        {
            get { return _Innerlist.Count; }
        }

        /// <summary>
        /// Returns always false.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove specified item from scene
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>True if item successfully removed; atherwise, false</returns>
        public bool Remove(GameComponent item)
        {
            return _Innerlist.Remove(item);
        }

        #endregion

        #region IEnumerable<GameComponent> Membres

        /// <summary>
        /// <see cref="System.Collections.IEnumerable.GetEnumerator"/>
        /// </summary>
        /// <returns></returns>
        public IEnumerator<GameComponent> GetEnumerator()
        {            
            return _Innerlist.GetEnumerator();
        }

        #endregion

        #region IEnumerable Membres

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Innerlist.GetEnumerator() as System.Collections.IEnumerator;
        }

        #endregion
    }
}