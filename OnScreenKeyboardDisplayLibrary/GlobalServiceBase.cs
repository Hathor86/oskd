using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.Services;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OnScreenKeyboardDisplayLibrary
{
    public abstract class GlobalServiceBase : InputServiceBase
    {
        #region Fields

        protected delegate IntPtr LowLevelProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        protected static extern IntPtr CallNextHookEx(IntPtr hookID, int nCode, IntPtr wParam, IntPtr lParam);       

        private LowLevelProc _Proc;
        
        protected IntPtr HookID = IntPtr.Zero;

        #endregion

        #region cTor(s)

        public GlobalServiceBase(Game game)
            : base(game)
        {
            _Proc = HookCallback;
        }

        #endregion

        #region Methods

        public abstract void Hook();
        protected abstract IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam);

        public void Unhook()
        {
            UnhookWindowsHookEx(HookID);
        }

        protected static IntPtr SetHook(LowLevelProc proc, int WH_LL)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        #endregion

        #region Properties

        protected LowLevelProc Proc
        {
            get
            {
                return _Proc;
            }
        }

        #endregion
    }
}
