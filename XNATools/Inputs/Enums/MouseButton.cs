using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNATools.Enums
{
    /// <summary>
    /// Enum for mouse's button
    /// </summary>
    public enum MouseButton : int
    {
        /// <summary>
        /// No button
        /// </summary>
        None = -1,
        /// <summary>
        /// Left button
        /// </summary>
        Left = 0,
        /// <summary>
        /// Right button
        /// </summary>
        Right,
        /// <summary>
        /// Middle button
        /// (Mouse wheel)
        /// </summary>
        Middle,
        /// <summary>
        ///Side left button
        /// </summary>
        SideLeft,
        /// <summary>
        /// Side right button
        /// </summary>
        SideRight       
    }
}
