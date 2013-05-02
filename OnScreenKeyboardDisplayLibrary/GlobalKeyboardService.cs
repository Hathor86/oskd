using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XNATools.Managers;
using XNATools.Services;
using XNATools.Services.Interfaces;
using WinForm = System.Windows.Forms;

namespace OnScreenKeyboardDisplayLibrary
{
    public sealed class GlobalKeyboardService : InputServiceBase, IKeyboardService
    {
        #region Fields

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        private bool _IsNumLockOn = false;
        private bool _IsCapsLockOn = false;
        private bool _IsScrollLockOn = false;

        private HashSet<Keys> _PressedKeys = new HashSet<Keys>();

        #endregion

        #region cTor(s)

        public GlobalKeyboardService(Game game)
            : base(game)
        {
            _proc = HookCallback;
        }

        #endregion

        #region Methods

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;
                _PressedKeys.Add((Keys)vkCode);
            }

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                _PressedKeys.Remove((Keys)vkCode);
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public void Hook()
        {
            _hookID = SetHook(_proc);
        }

        public void Unhook()
        {
            UnhookWindowsHookEx(_hookID);
        }

        public override void Update(GameTime gameTime)
        {
            _IsNumLockOn = WinForm.Control.IsKeyLocked(WinForm.Keys.NumLock);
            _IsCapsLockOn = WinForm.Control.IsKeyLocked(WinForm.Keys.CapsLock);
            _IsScrollLockOn = WinForm.Control.IsKeyLocked(WinForm.Keys.Scroll);
            base.Update(gameTime);
        }

        #region InputServiceBase Members

        protected override bool ComputeStateHasChange()
        {
            return true;
        }

        protected override bool IsThisInputServiceExistInManager()
        {
            return ServiceManager.Exists<IKeyboardService>(this);
        }

        protected override void RegisterInputService()
        {
            ServiceManager.Add<IKeyboardService>(this);
        }

        #endregion

        #region IKeyboardService Members

        /*As we get out of the update loop, only the methods IsKey... has sense.
         Other methods call these to avoid exception throw*/

        public bool IsKeyDown(Keys key)
        {
            return _PressedKeys.Contains(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return !IsKeyDown(key);
        }

        public bool KeyPressed(Keys key)
        {
            return IsKeyDown(key);
        }

        public bool KeyReleased(Keys key)
        {
            return IsKeyUp(key);
        }

        public bool KeyIsJustPressed(Keys key)
        {
            return IsKeyDown(key);
        }

        public bool KeyIsJustReleased(Keys key)
        {
            return IsKeyUp(key);
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets whether Num lock is activated or not
        /// </summary>
        public bool IsNumLockOn
        {
            get
            {
                return _IsNumLockOn;
            }
        }

        /// <summary>
        /// Gets whether Caps lock is activated or not
        /// </summary>
        public bool IsCapsLockOn
        {
            get
            {
                return _IsCapsLockOn;
            }
        }

        /// <summary>
        /// Gets whether Scroll lock is activated or not
        /// </summary>
        public bool IsScrollLockOn
        {
            get
            {
                return _IsScrollLockOn;
            }
        }

        #region IKeyboardService Members

        public KeyboardState State
        {
            get
            {
                return new KeyboardState(_PressedKeys.ToArray<Keys>());
            }
        }

        public Keys[] PressedKeys
        {
            get
            {
                return _PressedKeys.ToArray<Keys>();
            }
        }

        #endregion

        #endregion
    }
}
