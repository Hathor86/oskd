using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNATools
{
    /// <summary>
    /// Class for extension method for vectors class
    /// </summary>
    public static class VectorExtension
    {
        #region Vector2

        /// <summary>
        /// Update the vector with specified offset
        /// <remarks>X and Y are added to current</remarks>
        /// </summary>
        /// <param name="current">Vector2 to Update</param>
        /// <param name="offsetX">Offset in x</param>
        /// <param name="offsetY">Offest in y</param>
        /// <returns>Update vector</returns>
        /*public static Vector2 Update(this Vector2 current, float offsetX, float offsetY)
        {
            current.X += offsetX;
            current.Y += offsetY;
            return current;
        }
        /// <summary>
        /// Update the vector with specified offset
        /// <remarks>X and Y are added to current</remarks>
        /// </summary> 
        /// <param name="current">Vector2 to Update</param>
        /// <param name="offset">Offset</param>
        /// <returns>Updated vector</returns>
        public static Vector2 Update(this Vector2 current, Vector2 offset)
        {
            current.X += offset.X;
            current.Y += offset.Y;
            return current;
        }*/
       
        #endregion
    }
}
