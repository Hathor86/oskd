using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNATools.Enums
{
    /// <summary>
    /// Enum for choosing what will be displayed in the debug frame
    /// <see cref="XNATools.Diagnostics.DebugFrame"/>
    /// </summary>    
    [Flags]
    public enum FrameDisplay:int
    {
        /// <summary>
        /// Custom data frame
        /// </summary>
        Custom = 1,
        /// <summary>
        /// FrameRate frame
        /// </summary>
        FPS,

        /// <summary>
        /// All frames
        /// </summary>
        All = Custom | FPS
    }
}
