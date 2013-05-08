using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNATools
{
    /// <summary>
    /// Class for Misc helper
    /// </summary>
    public static class Helper
    {
        #region Fields

        /// <summary>
        /// Randomizer
        /// </summary>
        public static readonly Random RandomNumber = new Random(DateTime.Now.Millisecond & (int)new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes").NextValue());

        #endregion

        #region Methods

        /// <summary>
        /// Choose a random value in parameters
        /// </summary>
        /// <typeparam name="T">Type of values to choose</typeparam>
        /// <param name="values">Values to choose</param>
        /// <returns></returns>
        public static T ChooseBetween<T>(params T[] values)
        {
            return values[RandomNumber.Next(0, values.Length)];
        }

        /// <summary>
        /// Compute a Vector2 for scaling
        /// </summary>
        /// <param name="originalSize">Base size</param>
        /// <param name="sizeToObtain">Size to obtain</param>
        /// <returns>A new Vector2 that contain scale</returns>
        public static Vector2 ComputeScale(Vector2 originalSize, Vector2 sizeToObtain)
        {
            return ComputeScale(originalSize.X, originalSize.Y, sizeToObtain.X, sizeToObtain.Y);
        }

        /// <summary>
        /// Compute a Vector2 for scaling
        /// </summary>
        /// <param name="originalX">Base size in X</param>
        /// <param name="originalY">Base size in Y</param>
        /// <param name="toObtainX">Size to obtain in X</param>
        /// <param name="toObtainY">Size to obtain in Y</param>
        /// <returns>A new Vector2 that contain scale</returns>
        public static Vector2 ComputeScale(float originalX, float originalY, float toObtainX, float toObtainY)
        {
            float widthScale = toObtainX / originalX;
            float heightScale = toObtainY / originalY;

            return new Vector2(widthScale, heightScale);
        }

        #endregion
    }
}
