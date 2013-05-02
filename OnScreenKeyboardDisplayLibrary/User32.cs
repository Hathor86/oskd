using System.Runtime.InteropServices;

namespace OnScreenKeyboardDisplayLibrary
{
    public class User32
    {
        [DllImport("user32.dll")]
        public static extern void SetWindowPos(uint Hwnd, int Level, int X, int Y, int W, int H, uint Flags);
    }
}
