using System;

namespace XNATools.Enums
{
    /// <summary>
    /// Enums that help to choose draw order better
    /// Border, Artwork and Overlay allows 1 to 9 drawn order in layer; Highlight allow int.MaxValue
    /// </summary>
    public enum Layer:int
    {
        /// <summary>
        /// This is the first thing drawned
        /// Value is 0
        /// </summary>
        Background = 0,
        /// <summary>
        /// This is the seconds thing drawned, just after background
        /// Value is 1
        /// </summary>
        Border = 1,
        /// <summary>
        /// This is the third thing drawned
        /// Value is 10.
        /// </summary>
        Artwork = 10,
        /// <summary>
        /// This is the fourth thing drawned
        /// Value is 20.
        /// </summary>
        Overlay = 20,
        /// <summary>
        /// This is the last thing drawned
        /// Value is 30.
        /// </summary>
        Highlight = 30
    }
}
