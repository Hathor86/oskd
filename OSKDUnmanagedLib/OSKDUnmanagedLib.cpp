#include "Stdafx.h"
#include "OSKDUnmanagedLib.h"

#pragma data_seg(".SHRDATA")
HANDLE hWnd = NULL;
#pragma data_seg()
//#pragma comment(linker, "/section::.SHRDATA,rws")

#pragma comment( lib, "user32.lib" )

HINSTANCE hInstance;
UINT HWM_MOUSEHOOK;
HHOOK hook;

static LRESULT CALLBACK MouseCallBack(int nCode, WPARAM wParam, LPARAM lParam);

void SetWindowHandle(HWND w)
{
	hWnd = w;
}

//int mouseWheelValue = 0;

__declspec(dllexport) BOOL WINAPI SetMouseHook(HWND hWnd)
{	 
	 hook = SetWindowsHookEx(WH_GETMESSAGE,(HOOKPROC)MouseCallBack, hInstance, 0);
	 if(hook != NULL)
     { /* success */
      //hWndServer = hWnd;
      return TRUE;
     } /* success */
   return FALSE;
}

_declspec(dllexport) BOOL ClearMouseHook(HWND hWnd)
   {
    /*if(hWnd != hWndServer)
       return FALSE;*/
    BOOL unhooked = UnhookWindowsHookEx(hook);
    /*if(unhooked)
       hWndServer = NULL;*/
    return unhooked;
   }

LRESULT CALLBACK MouseCallBack(int nCode, WPARAM wParam, LPARAM lParam)
{
	LPMSG msg = (LPMSG)lParam;

	if (nCode >= 0 && msg->message == WM_MOUSEHWHEEL)
	 {
	 }

	 return CallNextHookEx(NULL, nCode, wParam, lParam);
}