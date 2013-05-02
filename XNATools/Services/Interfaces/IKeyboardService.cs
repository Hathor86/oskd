using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace XNATools.Services.Interfaces
{
    /// <summary>
    /// Interface for all service who whants to be KeyboardService
    /// </summary>
    public interface IKeyboardService : IInputService
    {
        /// <summary>
        /// Determine if specified key is currently down state
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>True if specified key is down; otherwise, false</returns>
        bool IsKeyDown(Keys key);
        /// <summary>
        /// Determine if specified key is currently up state
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>True if specified key is up; otherwise, false</returns>
        bool IsKeyUp(Keys key);

        /// <summary>
        /// Determine if specified key is pressed
        /// i.e. button was down last update and is still down in current update
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>True if specifiedkey is pressed; otherwise, false</returns>
        bool KeyPressed(Keys key);
        /// <summary>
        /// Determine if specified key is released
        /// i.e. button was up last update and is still up in current update
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>True if specifiedkey is released; otherwise, false</returns>
        bool KeyReleased(Keys key);

        /// <summary>
        /// Determine if specified key is just pressed
        /// i.e key has been pressed between current and last update
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>True if specifiedkey is just pressed; otherwise, false</returns>
        bool KeyIsJustPressed(Keys key);
        /// <summary>
        /// Determine if specified key is released
        /// i.e key has been released between current and last update
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>True if specifiedkey is just released; otherwise, false</returns>
        bool KeyIsJustReleased(Keys key);

        /// <summary>
        /// Gets current's keyboard state
        /// </summary>
        KeyboardState State { get; }
        /// <summary>
        /// Gets current's keyboard pressed keys
        /// </summary>
        Keys[] PressedKeys { get; }
    }
}
