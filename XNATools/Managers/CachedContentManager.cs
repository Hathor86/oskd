using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using XNATools.Services.Interfaces;
using Microsoft.Xna.Framework;
using XNATools.Services;

namespace XNATools.Managers
{

    /// <summary>
    /// Class for holding CachedContentManager
    /// A CachedContentManager as his name said, store ressources in cache
    /// It can release it automatically if not used since a specified time
    /// </summary>
    public sealed class CachedContentManager<TContenType> : ServiceBase,
        IDictionary<string, TContenType>,
        ICacheManagerService<TContenType>
    {
        #region Fields

        private const int LoopCheck = 10000;

        private readonly string _DefaultFolder = string.Empty;        
        private TimeSpan _ReleaseTime = new TimeSpan(0, 10, 0);

        private ContentManager contentManager;
        private Dictionary<string, TContenType> innerDictionary = new Dictionary<string, TContenType>();
        private Dictionary<string, TimeSpan> timeDictionnary = new Dictionary<string, TimeSpan>();

        private int loop = 0;

        #endregion

        #region cTor(s)

        /// <summary>
        /// New CachedContentManager
        /// </summary>
        /// <param name="game">Game that use the cache</param>
        public CachedContentManager(Game game)
            :base(game)
        {
            contentManager = game.Content;
        }

        /// <summary>
        /// New CachedContentManager
        /// </summary>
        /// <param name="game">Game that use the cache</param>
        /// <param name="baseFolder">Folder where look for content</param>
        public CachedContentManager(Game game, string baseFolder)
            : this(game)
        {
            if (baseFolder.EndsWith(@"\"))
            {
                _DefaultFolder = baseFolder;
            }
            else
            {
                _DefaultFolder = string.Format("{0}\\", baseFolder);
            }            
        }

        /// <summary>
        /// New CachedContentManager
        /// </summary>
        /// <param name="game">Game that use the cache</param>
        /// <param name="baseFolder">Folder where look for content</param>
        /// <param name="autoRelease">Define if content will be released after x elapsed seconds (10minutes by default)</param>
        public CachedContentManager(Game game, string baseFolder, bool autoRelease)
            : this(game, baseFolder)
        {
            if (autoRelease)
            {
                UpdateOrder = int.MaxValue;
            }
            else
            {
                game.Components.Remove(this);
            }
        }

        /// <summary>
        /// New CachedContentManager
        /// </summary>
        /// <param name="game">Game that use the cache</param>
        /// <param name="baseFolder">Folder where look for content</param>
        /// <param name="autoRelease">Define if content will be released after x elapsed seconds (10minutes by default)</param>
        /// <param name="releaseTime">How long content persist in cache</param>
        public CachedContentManager(Game game, string baseFolder, bool autoRelease, TimeSpan releaseTime)
            :this(game,baseFolder,autoRelease)
        {
            _ReleaseTime = releaseTime;
        }

        #endregion

        #region Methods

        /// <summary>
        /// <see cref="Microsoft.Xna.Framework.GameComponent.Update"/>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            loop++;

            foreach (string s in innerDictionary.Keys)
            {               
                timeDictionnary[s] += gameTime.ElapsedGameTime;                
            }

            if (loop >= LoopCheck)
            {
                string[] toRemove = (from string s in timeDictionnary.Keys where timeDictionnary[s] >= _ReleaseTime select s).ToArray<string>();

                for (int i = 0; i < toRemove.Length; i++)
                {
                    innerDictionary.Remove(toRemove[i]);
                    timeDictionnary.Remove(toRemove[i]);
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// <see cref="XNATools.Services.ServiceBase.IsThisServiceExistInManager"/>
        /// </summary>
        /// <returns></returns>
        protected override bool IsThisServiceExistInManager()
        {
            return ServiceManager.Exists<ICacheManagerService<TContenType>>(this);
        }

        /// <summary>
        /// <see cref="XNATools.Services.ServiceBase.RegisterService"/>
        /// </summary>        
        protected override void RegisterService()
        {
            ServiceManager.Add<ICacheManagerService<TContenType>>(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets base folder used
        /// </summary>
        public string BaseFolder
        {
            get
            {
                return _DefaultFolder;
            }
        }

        /// <summary>
        /// Gets cache persitence time
        /// </summary>
        public TimeSpan ReleaseTime
        {
            get
            {
                return _ReleaseTime;
            }
        }

        #endregion

        #region IDictionary<string,TContenType> Membres

        #region Methods

        /// <summary>
        /// Add specified key and values into cache
        /// </summary>
        /// <param name="key">Key to add</param>
        /// <param name="value">Value to add.<remarks>Can be null but useless</remarks></param>
        public void Add(string key, TContenType value)
        {
            innerDictionary.Add(key, value);
            timeDictionnary.Add(key, TimeSpan.Zero);
        }

        /// <summary>
        /// Add specified key and values into cache
        /// </summary>
        /// <param name="key">Key to add</param>
        public void Add(string key)
        {
            Add(key, contentManager.Load<TContenType>(string.Format("{0}{1}", _DefaultFolder, key)));            
        }

        /// <summary>
        /// Define if cache contains specified key
        /// </summary>
        /// <param name="key">Key to find</param>
        /// <returns>True if found otherwise, false</returns>
        public bool ContainsKey(string key)
        {
            return innerDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Remove specified key from cache
        /// </summary>
        /// <param name="key">Key to remove</param>
        /// <returns>True if successfull otherwise, false</returns>
        public bool Remove(string key)
        {
            return innerDictionary.Remove(key);
        }

        /// <summary>
        /// Try to get value associted with the key
        /// </summary>
        /// <param name="key">Key to find</param>
        /// <param name="value">Value to store if found</param>
        /// <returns>True if successfull otherwise, false</returns>
        public bool TryGetValue(string key, out TContenType value)
        {
            return innerDictionary.TryGetValue(key, out value);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets collection containing keys
        /// </summary>
        public ICollection<string> Keys
        {
            get
            {
                return innerDictionary.Keys;
            }
        }

        /// <summary>
        /// Gets collection containing values
        /// </summary>
        public ICollection<TContenType> Values
        {
            get
            {
                return innerDictionary.Values;
            }
        }

        /// <summary>
        /// Gets or sets value
        /// </summary>
        /// <param name="key">Key to set or define</param>
        /// <returns></returns>
        public TContenType this[string key]
        {
            get
            {
                if (innerDictionary.ContainsKey(key))
                {
                    timeDictionnary[key] = TimeSpan.Zero;
                    return innerDictionary[key];
                }
                else
                {
                    innerDictionary.Add(key, contentManager.Load<TContenType>(string.Format("{0}{1}", _DefaultFolder, key)));
                    timeDictionnary.Add(key, TimeSpan.Zero);
                    return innerDictionary[key];
                }
            }
            set
            {
                innerDictionary[key] = value;
                timeDictionnary[key] = TimeSpan.Zero;
            }
        }

        #endregion

        #endregion

        #region ICollection<KeyValuePair<string,TContenType>> Membres

        /// <summary>
        /// Add KeyValuePair to cache
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<string, TContenType> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Remove all Keys and values from cache
        /// </summary>
        public void Clear()
        {
            innerDictionary.Clear();
            timeDictionnary.Clear();
        }

        /// <summary>
        /// Define if cache contains KeyValuePair item
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>True if found otherwise, false</returns>
        public bool Contains(KeyValuePair<string, TContenType> item)
        {
            return innerDictionary.ContainsKey(item.Key);
        }

        /// <summary>
        /// Copy cache iton specified array at specified index
        /// </summary>
        /// <param name="array">Array where to copy</param>
        /// <param name="arrayIndex">Zero-based index where to start copy</param>
        public void CopyTo(KeyValuePair<string, TContenType>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, TContenType>>)innerDictionary).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets number of items in cache
        /// </summary>
        public int Count
        {
            get
            {
                return innerDictionary.Count;
            }
        }

        /// <summary>
        /// Always false
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Remove specified KeyValuePair from cache
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<string, TContenType> item)
        {
            timeDictionnary.Remove(item.Key);
            return innerDictionary.Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,TContenType>> Membres

        /// <summary>
        /// <see cref="System.Collections.IEnumerable.GetEnumerator"/>
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, TContenType>> GetEnumerator()
        {            
            return ((IEnumerable<KeyValuePair<string, TContenType>>)(innerDictionary)).GetEnumerator();
        }

        #endregion

        #region IEnumerable Membres

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.ICollection)innerDictionary).GetEnumerator();
        }

        #endregion

        #region ICacheMangerService<TContenType> Membres

        /// <summary>
        /// Gets specified asset
        /// </summary>
        /// <param name="key">Key, often it's the same as asset name</param>
        /// <returns>A TContentType value</returns>
        public TContenType Get(string key)
        {
            return this[key];
        }

        #endregion
    }
}
