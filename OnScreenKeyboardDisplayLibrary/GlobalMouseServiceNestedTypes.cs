using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace OnScreenKeyboardDisplayLibrary
{    
    public sealed partial class GlobalMouseService
    {
        /* Not used
        private enum MouseMessages : int
        {
            MouseMove = 0x0200,
            RightButtonUp = 0x0201,
            LeftButtonUp = 0x0202,
            RightButtonDown = 0x0204,
            LeftButtonDown = 0x0205,
            MiddleButtonDown = 0x0207,
            MiddleButtonUp = 0x0208,
            MouseWheel = 0x020A,
            SideLeftButtonDown = 0x020B,
            SideLeftButtonDown = 0x020C

            /*Double click - Not used*/
            /*WM_LBUTTONDBLCLK = 0x203,
            WM_RBUTTONDBLCLK = 0x206,
            WM_MBUTTONDBLCLK  = 0x209,
        }*/

        [StructLayout(LayoutKind.Sequential)]
        private class POINT
        {
            /// <summary>
            /// X Coordinate
            /// </summary>
            public int x;
            /// <summary>
            /// Y Coordinate
            /// </summary>
            public int y;
        }

        /*Not used
         * [StructLayout(LayoutKind.Sequential)]
        private class MouseHookStruct
        {
            /// <summary>
            /// POINT struct (coordinate)
            /// </summary>
            public POINT pt;
            /// <summary>
            /// Window handle
            /// </summary>
            public int hwnd;
            /// <summary>
            /// Specifies the hit-test value. For a list of hit-test values, see the description of the WM_NCHITTEST message. 
            /// </summary>
            public int wHitTestCode;
            /// <summary>
            /// Specifies extra information associated with the message. 
            /// </summary>
            public int dwExtraInfo;
        }*/

        [StructLayout(LayoutKind.Sequential)]
        private class MouseLLHookStruct
        {
            /// <summary>
            /// Structure POINT.
            /// </summary>
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

    }
}
