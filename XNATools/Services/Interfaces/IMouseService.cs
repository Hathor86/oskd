using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using XNATools.Enums;

namespace XNATools.Services.Interfaces
{
    /// <summary>
    /// Interface for all service who wants to be MouseService
    /// </summary>
    public interface IMouseService : IInputService
    {
        /// <summary>
        /// Gets mouse state
        /// </summary>
        MouseState State { get; }   
        /// <summary>
        /// Gets mouse position
        /// </summary>
        Vector2 Position { get; }  
        /// <summary>
        /// Gets offset since last update
        /// </summary>
        Vector2 Offset {get;}
        /// <summary>
        /// Gets offset from center
        /// </summary>
        Vector2 OffsetFromCenter { get; }
        /// <summary>
        /// Gets mousewheel offset
        /// </summary>
        int MouseWheelOffset { get; }

        /// <summary>
        /// Sets mouse position to screen center
        /// </summary>
        void Center();
        /// <summary>
        /// Determine if specified button is currently down
        /// </summary>
        /// <param name="button">Button to test</param>
        /// <returns>True if specified button is down; otherwise, false</returns>
        bool IsButtonDown(MouseButton button);
        /// <summary>
        /// Determine if specified button is currently up
        /// </summary>
        /// <param name="button">Button to test</param>
        /// <returns>True if specified button is up; otherwise, false</returns>
        bool IsButtonUp(MouseButton button);
        /// <summary>
        /// Determine if specified button is pressed
        /// i.e. button was down last update and is still down in current update
        /// </summary>
        /// <param name="button">Button to test</param>
        /// <returns>True if specified button is pressed; otherwise, false</returns>
        bool IsButtonPressed(MouseButton button);
        /// <summary>
        /// Determine if specified button is released
        /// i.e. button was up last update and is still up in current update
        /// </summary>
        /// <param name="button">Button to test</param>
        /// <returns>True if specified button is released; otherwise, false</returns>
        bool IsButtonReleased(MouseButton button);
        /// <summary>
        /// Determine if specified button is just pressed
        /// i.e. button has been down between last update and current update
        /// </summary>
        /// <param name="button">Button to test</param>
        /// <returns>True if specified button is just pressed; otherwise, false</returns>
        bool IsButtonJustPressed(MouseButton button);
        /// <summary>
        /// Determine if specified button is just released
        /// i.e. button has been up between last update and current update
        /// </summary>
        /// <param name="button">Button to test</param>
        /// <returns>True if specified button is just released; otherwise, false</returns>
        bool IsButtonJustReleased(MouseButton button);
    }
}
