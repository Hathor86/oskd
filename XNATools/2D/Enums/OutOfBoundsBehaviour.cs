using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNATools.Enums
{
    /// <summary>
    /// Enum that define behaviour of IsOutOfBounds method 
    /// See Sprite.IsOutOfBounds
    /// </summary>
    public enum OutOfBoundsBehaviour:int
    {
        /// <summary>
        /// No behaviour, simple test
        /// </summary>
        None = 0,
        /// <summary>
        /// Texture's bounds exits completly from client's bounds
        /// </summary>
        TextureIsHidden,
        /// <summary>
        /// Texture's bounds hits client's bounds
        /// </summary>
        TextureHitsBounds
    }
}
