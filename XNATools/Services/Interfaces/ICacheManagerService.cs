using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNATools.Services.Interfaces
{
    /// <summary>
    /// Interface for all services who wants to be CacheManagerService
    /// </summary>
    /// <typeparam name="TContentType">Type of content to store in cache
    /// <example>
    /// <![CDATA[
    /// ICacheManagerService<Texture2D>
    /// ICacheManagerService<SpriteFont>
    /// ....
    /// ]]>
    /// </example>
    /// </typeparam>    
    public interface ICacheManagerService<TContentType>:IService
    {
        /// <summary>
        /// Add specified asset to the cache
        /// </summary>
        /// <param name="key">Key, often it's the same as asset name</param>
        /// <param name="value">Content to add</param>        
        void Add(string key, TContentType value);
        /// <summary>
        /// Gets specified asset
        /// </summary>
        /// <param name="key">Key, often it's the same as asset name</param>
        /// <returns>A TContentType value</returns>
        TContentType Get(string key);
        /// <summary>
        /// Gets or sets asset from cache
        /// </summary>
        /// <param name="key">Key, often it's the same as asset name</param>
        /// <returns>A TContentType value</returns>
        TContentType this[string key] { get; set; }
        /// <summary>
        /// Remove specified asset from cache
        /// </summary>
        /// <param name="key">Key, often it's the same as asset name</param>
        /// <returns>True if delete successfull; otherwise, false</returns>
        bool Remove(string key);
    }
}
