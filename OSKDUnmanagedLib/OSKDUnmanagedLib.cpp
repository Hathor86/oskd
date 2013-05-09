#pragma comment( lib, "user32.lib" )

#include "Stdafx.h"
#include "OSKDUnmanagedLib.h"

//int mouseWheelValue = 0;

HHOOK WINAPI SetMouseHook(HINSTANCE threadID)
{	 
	 return SetWindowsHookEx (WH_MOUSE_LL, MouseCallBack,  threadID, NULL);	 
}

LRESULT CALLBACK MouseCallBack(int nCode, WPARAM wParam, LPARAM lParam)
{
	 if (nCode >= 0 && wParam == MouseWheel)
	 {
	 }

	 return CallNextHookEx(NULL, nCode, wParam, lParam);
}