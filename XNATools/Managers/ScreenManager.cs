using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNATools.Enums;
using Microsoft.Xna.Framework.Graphics;
using XNATools.Diagnostics;

namespace XNATools.Managers
{
    /// <summary>
    /// Class for holding Scenes and all about screen
    /// </summary>
    public static class ScreenManager
    {
        #region Fields

        private static Dictionary<string, Scene> _SceneList = new Dictionary<string, Scene>();
        private static AspectRatio _Ratio = AspectRatio.SixteenByNine;

        private static GraphicsDeviceManager _GraphicDeviceManager;
        private static Scene _CurrentScene;
        private static int _CurrentWidth = 800;
        private static int _CurrentHeight = 640;
        private static DebugFrame _DebugFrame;

        /// <summary>
        /// Determine if aspect ration must be kept
        /// </summary>
        public static bool KeepAspectRatio = true;

        #endregion

        #region cTor(s)
        #endregion

        #region Methods

        /// <summary>
        /// Add a scene
        /// </summary>
        /// <param name="key">Key of the scene</param>
        /// <param name="s">Scene to add</param>
        public static void AddScene(string key, Scene s)
        {
            if (!_SceneList.ContainsKey(key))
            {
                _SceneList.Add(key, s);
            }
        }

        /// <summary>
        /// Remove a scene
        /// </summary>
        /// <param name="key">Key of the scene</param>
        public static void RemoveScene(string key)
        {
            _SceneList.Remove(key);
        }

        /// <summary>
        /// Show specified scene
        /// </summary>
        /// <param name="key">Key of the scene</param>
        public static void ShowScene(string key)
        {
            if (_SceneList.ContainsKey(key))
            {
                if (_CurrentScene != null)
                    _CurrentScene.Hide();

                _SceneList[key].Show();
                _CurrentScene = _SceneList[key];
            }
            else
                throw new IndexOutOfRangeException(string.Format("Scene {0} doesn't exist", key));
        }

        /// <summary>
        /// Return specified scene
        /// </summary>
        /// <param name="key">Key of the scene</param>
        /// <returns></returns>
        public static Scene GetScene(string key)
        {
            if (_SceneList.ContainsKey(key))
                return _SceneList[key];
            else
                throw new IndexOutOfRangeException(string.Format("Scene {0} doesn't exist", key));

        }

        /// <summary>
        /// Change screen resolution
        /// Aspect Ration will be kept; Height will be change if necesseray
        /// </summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public static void ChangeResolution(int width, int height)
        {
            if (!KeepAspectRatio)
            {
                _GraphicDeviceManager.PreferredBackBufferHeight = height;
                _GraphicDeviceManager.PreferredBackBufferWidth = width;
                _GraphicDeviceManager.ApplyChanges();
                _CurrentWidth = width;
                _CurrentHeight = height;
            }
            else
            {
                switch (_Ratio)
                {
                    case AspectRatio.FourByThree:
                        if (width / (float)height != 4f / 3f)
                        {
                            height = width * 3 / 4;
                        }
                        break;
                    case AspectRatio.SixteenByNine:
                        if (width / (float)height != 16f / 9f)
                        {
                            height = width * 9 / 16;
                        }
                        break;
                    case AspectRatio.SixteenByTen:
                        if (width / (float)height != 16f / 10f)
                        {
                            height = width * 10 / 16;
                        }
                        break;
                }
                _GraphicDeviceManager.PreferredBackBufferWidth = width;
                _GraphicDeviceManager.PreferredBackBufferHeight = height;
                _GraphicDeviceManager.ApplyChanges();
                _CurrentWidth = width;
                _CurrentHeight = height;
            }
        }

        /// <summary>
        /// Switch between full screen and window
        /// </summary>
        public static void ToggleFullScreen()
        {
            _GraphicDeviceManager.ToggleFullScreen();
        }        

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets GraphicsDeviceManager
        /// <remarks>Don't forget to set it before use ScreenManager</remarks>        
        /// </summary>        
        public static GraphicsDeviceManager GraphicsDeviceManager
        {
            get
            {
                return _GraphicDeviceManager;
            }
            set
            {
                if (_GraphicDeviceManager == null)
                    _GraphicDeviceManager = value;
                else
                    throw new Exception("Property \"GraphicsDeviceManager\" can only be set once");
            }
        }

        /// <summary>
        /// Gets the GraphicsDevice related to the GraphicsDeviceManager
        /// </summary>
        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                return _GraphicDeviceManager.GraphicsDevice;
            }
        }

        /// <summary>
        /// Gets or Sets Aspect Ratio. Resolution will be adapted
        /// </summary>
        public static AspectRatio AspectRatio
        {
            get
            {
                return _Ratio;
            }

            set
            {
                if (value != _Ratio)
                {
                    _Ratio = value;
                    ChangeResolution(_CurrentWidth, _CurrentHeight);
                }
            }
        }

        /// <summary>
        /// Gets or sets the debug frame
        /// </summary>
        public static DebugFrame DebugFrame
        {
            get
            {
                return _DebugFrame;
            }
            set
            {
                if (_DebugFrame == null)
                {
                    _DebugFrame = value;
                }
                else
                    throw new Exception("Debug frame already sets");
            }
        }

        /// <summary>
        /// Gets Coordinates of screen center
        /// </summary>
        public static Vector2 ScreenCenter
        {
            get
            {
                return new Vector2(_CurrentWidth / 2, _CurrentHeight / 2);
            }
        }

        #endregion
    }
}
