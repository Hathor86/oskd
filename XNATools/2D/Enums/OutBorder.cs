using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNATools.Enums
{
    /// <summary>
    /// Enum for out border.
    /// Border refers to client bounds.
    /// </summary>
    public enum OutBorder:int
    {
        /// <summary>
        /// No border
        /// </summary>
        None = 0,
        /// <summary>
        /// Left border
        /// </summary>
        Left,
        /// <summary>
        /// Right border
        /// </summary>
        Right,
        /// <summary>
        /// Up border
        /// </summary>
        Up,
        /// <summary>
        /// Down border
        /// </summary>
        Down
    }
}
