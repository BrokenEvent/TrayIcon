using System;
using System.Runtime.InteropServices;

namespace BrokenEvent.Shared
{
	public delegate int WindowProcDelegate(IntPtr hw, IntPtr uMsg, IntPtr wParam, IntPtr lParam);

	/// <summary>
	/// Windows User32 DLL declarations
	/// </summary>
	public class User32
	{
    [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SetClipboardViewer")]
		public static extern IntPtr SetClipboardViewer(IntPtr hWnd);

    [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "ChangeClipboardChain")]
		public static extern bool ChangeClipboardChain(
			  IntPtr hWndRemove,  // handle to window to remove
			  IntPtr hWndNewNext  // handle to next window
			);

    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
    public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
    public static extern int SendMessage(IntPtr hwnd, Msgs wMsg, int wParam, int lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ShowWindow")]
    public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowPos")]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

	  [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "UpdateWindow")]
	  public static extern bool UpdateWindow(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetDesktopWindow")]
    public static extern IntPtr GetDesktopWindow();
	}

  public enum ShowWindowCommands
  {
    /// <summary>
    /// Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when minimizing windows from a different thread.
    /// </summary>
    ForceMinimize = 11,
    /// <summary>
    /// Hides the window and activates another window.
    /// </summary>
    Hide = 0,
    /// <summary>
    /// Maximizes the specified window.
    /// </summary>
    Maximize = 3,
    /// <summary>
    /// Minimizes the specified window and activates the next top-level window in the Z order.
    /// </summary>
    Minimize = 6,
    /// <summary>
    /// Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.
    /// </summary>
    Restore = 9,
    /// <summary>
    /// Activates the window and displays it in its current size and position. 
    /// </summary>
    Show = 5,
    /// <summary>
    /// Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application. 
    /// </summary>
    ShowDefault = 10,
    /// <summary>
    /// Activates the window and displays it as a maximized window.
    /// </summary>
    ShowMaximized = 3,
    /// <summary>
    /// Activates the window and displays it as a minimized window.
    /// </summary>
    ShowMinimized = 2,
    /// <summary>
    /// Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.
    /// </summary>
    ShowMinimizedNoActivate = 7,
    /// <summary>
    /// Displays the window in its current size and position. This value is similar to SW_SHOW, except that the window is not activated.
    /// </summary>
    ShowNA = 8,
    /// <summary>
    /// Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except that the window is not activated.
    /// </summary>
    ShowNoActivate = 4,
    /// <summary>
    /// Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
    /// </summary>
    ShowNormal = 1,

  }

  [Flags]
  public enum SetWindowPosFlags
  {
    /// <summary>
    /// If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request. 
    /// </summary>
    Async = 0x4000,
    /// <summary>
    /// Prevents generation of the WM_SYNCPAINT message. 
    /// </summary>
    DeferErase = 0x2000,
    /// <summary>
    /// Draws a frame (defined in the window's class description) around the window.
    /// </summary>
    DrawFrame = 0x0020,
    /// <summary>
    /// Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
    /// </summary>
    FrameChanged = 0x0020,
    /// <summary>
    /// Hides the window.
    /// </summary>
    HideWindow = 0x0080,
    /// <summary>
    /// Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
    /// </summary>
    NoActivate = 0x0010,
    /// <summary>
    /// Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
    /// </summary>
    NoCopyBits = 0x0100,
    /// <summary>
    /// Retains the current position (ignores X and Y parameters).
    /// </summary>
    NoMove = 0x0002,
    /// <summary>
    /// Does not change the owner window's position in the Z order.
    /// </summary>
    NoOwnerZOrder = 0x0200,
    /// <summary>
    /// Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
    /// </summary>
    NoRedraw = 0x0008,
    /// <summary>
    /// Same as the SWP_NOOWNERZORDER flag.
    /// </summary>
    NoReposition = 0x0200,
    /// <summary>
    /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
    /// </summary>
    NoSendChanging = 0x0400,
    /// <summary>
    /// Retains the current size (ignores the cx and cy parameters).
    /// </summary>
    NoSize = 0x0001,
    /// <summary>
    /// Retains the current Z order (ignores the hWndInsertAfter parameter).
    /// </summary>
    NoZOrder = 0x0004,
    /// <summary>
    /// Displays the window
    /// </summary>
    ShowWindow = 0x0040	
  }

  [Flags]
  public enum WindowStyles: uint
  {
    /// <summary>
    /// The window has a thin-line border.
    /// </summary>
    WS_BORDER = 0x00800000,
    /// <summary>
    /// The window has a title bar (includes the WS_BORDER style).
    /// </summary>
    WS_CAPTION = 0x00C00000,
    /// <summary>
    /// The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.
    /// </summary>
    WS_CHILD = 0x40000000,
    /// <summary>
    /// Same as the WS_CHILD style.
    /// </summary>
    WS_CHILDWINDOW = 0x40000000,
    /// <summary>
    /// Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.
    /// </summary>
    WS_CLIPCHILDREN = 0x02000000,
    /// <summary>
    /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated. If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
    /// </summary>
    WS_CLIPSIBLINGS = 0x04000000,
    /// <summary>
    /// The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.
    /// </summary>
    WS_DISABLED = 0x08000000,
    /// <summary>
    /// The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.
    /// </summary>
    WS_DLGFRAME = 0x00400000,
    /// <summary>
    /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style. The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
    /// </summary>
    WS_GROUP = 0x00020000,
    /// <summary>
    /// The window has a horizontal scroll bar.
    /// </summary>
    WS_HSCROLL = 0x00100000,
    /// <summary>
    /// The window is initially minimized. Same as the WS_MINIMIZE style.
    /// </summary>
    WS_ICONIC = 0x20000000,
    /// <summary>
    /// The window is initially maximized.
    /// </summary>
    WS_MAXIMIZE = 0x0100000,
    /// <summary>
    /// The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified. 
    /// </summary>
    WS_MAXIMIZEBOX = 0x00010000,
    /// <summary>
    /// The window is initially minimized. Same as the WS_ICONIC style.
    /// </summary>
    WS_MINIMIZE = 0x20000000,
    /// <summary>
    /// The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified. 
    /// </summary>
    WS_MINIMIZEBOX = 0x00020000,
    /// <summary>
    /// The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_TILED style.
    /// </summary>
    WS_OVERLAPPED = 0x00000000,
    /// <summary>
    /// The window is an overlapped window. Same as the WS_TILEDWINDOW style. 
    /// </summary>
    WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),
    /// <summary>
    /// The windows is a pop-up window. This style cannot be used with the WS_CHILD style.
    /// </summary>
    WS_POPUP = 0x80000000,
    /// <summary>
    /// The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.
    /// </summary>
    WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU),
    /// <summary>
    /// The window has a sizing border. Same as the WS_THICKFRAME style.
    /// </summary>
    WS_SIZEBOX = 0x00040000,
    /// <summary>
    /// The window has a window menu on its title bar. The WS_CAPTION style must also be specified.
    /// </summary>
    WS_SYSMENU = 0x00080000,
    /// <summary>
    /// The window is a control that can receive the keyboard focus when the user presses the TAB key. Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.
    /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function. For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
    /// </summary>
    WS_TABSTOP = 0x00010000,
    /// <summary>
    /// The window has a sizing border. Same as the WS_SIZEBOX style.
    /// </summary>
    WS_THICKFRAME = 0x00040000,
    /// <summary>
    /// The window is an overlapped window. An overlapped window has a title bar and a border. Same as the WS_OVERLAPPED style. 
    /// </summary>
    WS_TILED = 0x00000000,
    /// <summary>
    /// The window is an overlapped window. Same as the WS_OVERLAPPEDWINDOW style. 
    /// </summary>
    WS_TILEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),
    /// <summary>
    /// The window is initially visible.
    /// This style can be turned on and off by using the ShowWindow or SetWindowPos function.
    /// </summary>
    WS_VISIBLE = 0x10000000,
    /// <summary>
    /// The window has a vertical scroll bar.
    /// </summary>
    WS_VSCROLL = 0x00200000
  }

	/// <summary>
	/// Windows Event Messages sent to the WindowProc
	/// </summary>
	public enum Msgs
	{
		WM_NULL                   = 0x0000,
		WM_CREATE                 = 0x0001,
		WM_DESTROY                = 0x0002,
		WM_MOVE                   = 0x0003,
		WM_SIZE                   = 0x0005,
		WM_ACTIVATE               = 0x0006,
		WM_SETFOCUS               = 0x0007,
		WM_KILLFOCUS              = 0x0008,
		WM_ENABLE                 = 0x000A,
		WM_SETREDRAW              = 0x000B,
		WM_SETTEXT                = 0x000C,
		WM_GETTEXT                = 0x000D,
		WM_GETTEXTLENGTH          = 0x000E,
		WM_PAINT                  = 0x000F,
		WM_CLOSE                  = 0x0010,
		WM_QUERYENDSESSION        = 0x0011,
		WM_QUIT                   = 0x0012,
		WM_QUERYOPEN              = 0x0013,
		WM_ERASEBKGND             = 0x0014,
		WM_SYSCOLORCHANGE         = 0x0015,
		WM_ENDSESSION             = 0x0016,
		WM_SHOWWINDOW             = 0x0018,
		WM_WININICHANGE           = 0x001A,
		WM_SETTINGCHANGE          = 0x001A,
		WM_DEVMODECHANGE          = 0x001B,
		WM_ACTIVATEAPP            = 0x001C,
		WM_FONTCHANGE             = 0x001D,
		WM_TIMECHANGE             = 0x001E,
		WM_CANCELMODE             = 0x001F,
		WM_SETCURSOR              = 0x0020,
		WM_MOUSEACTIVATE          = 0x0021,
		WM_CHILDACTIVATE          = 0x0022,
		WM_QUEUESYNC              = 0x0023,
		WM_GETMINMAXINFO          = 0x0024,
		WM_PAINTICON              = 0x0026,
		WM_ICONERASEBKGND         = 0x0027,
		WM_NEXTDLGCTL             = 0x0028,
		WM_SPOOLERSTATUS          = 0x002A,
		WM_DRAWITEM               = 0x002B,
		WM_MEASUREITEM            = 0x002C,
		WM_DELETEITEM             = 0x002D,
		WM_VKEYTOITEM             = 0x002E,
		WM_CHARTOITEM             = 0x002F,
		WM_SETFONT                = 0x0030,
		WM_GETFONT                = 0x0031,
		WM_SETHOTKEY              = 0x0032,
		WM_GETHOTKEY              = 0x0033,
		WM_QUERYDRAGICON          = 0x0037,
		WM_COMPAREITEM            = 0x0039,
		WM_GETOBJECT              = 0x003D,
		WM_COMPACTING             = 0x0041,
		WM_COMMNOTIFY             = 0x0044 ,
		WM_WINDOWPOSCHANGING      = 0x0046,
		WM_WINDOWPOSCHANGED       = 0x0047,
		WM_POWER                  = 0x0048,
		WM_COPYDATA               = 0x004A,
		WM_CANCELJOURNAL          = 0x004B,
		WM_NOTIFY                 = 0x004E,
		WM_INPUTLANGCHANGEREQUEST = 0x0050,
		WM_INPUTLANGCHANGE        = 0x0051,
		WM_TCARD                  = 0x0052,
		WM_HELP                   = 0x0053,
		WM_USERCHANGED            = 0x0054,
		WM_NOTIFYFORMAT           = 0x0055,
		WM_CONTEXTMENU            = 0x007B,
		WM_STYLECHANGING          = 0x007C,
		WM_STYLECHANGED           = 0x007D,
		WM_DISPLAYCHANGE          = 0x007E,
		WM_GETICON                = 0x007F,
		WM_SETICON                = 0x0080,
		WM_NCCREATE               = 0x0081,
		WM_NCDESTROY              = 0x0082,
		WM_NCCALCSIZE             = 0x0083,
		WM_NCHITTEST              = 0x0084,
		WM_NCPAINT                = 0x0085,
		WM_NCACTIVATE             = 0x0086,
		WM_GETDLGCODE             = 0x0087,
		WM_SYNCPAINT              = 0x0088,
		WM_NCMOUSEMOVE            = 0x00A0,
		WM_NCLBUTTONDOWN          = 0x00A1,
		WM_NCLBUTTONUP            = 0x00A2,
		WM_NCLBUTTONDBLCLK        = 0x00A3,
		WM_NCRBUTTONDOWN          = 0x00A4,
		WM_NCRBUTTONUP            = 0x00A5,
		WM_NCRBUTTONDBLCLK        = 0x00A6,
		WM_NCMBUTTONDOWN          = 0x00A7,
		WM_NCMBUTTONUP            = 0x00A8,
		WM_NCMBUTTONDBLCLK        = 0x00A9,
		WM_NCXBUTTONDOWN          = 0x00AB,
		WM_NCXBUTTONUP            = 0x00AC,
		WM_KEYDOWN                = 0x0100,
		WM_KEYUP                  = 0x0101,
		WM_CHAR                   = 0x0102,
		WM_DEADCHAR               = 0x0103,
		WM_SYSKEYDOWN             = 0x0104,
		WM_SYSKEYUP               = 0x0105,
		WM_SYSCHAR                = 0x0106,
		WM_SYSDEADCHAR            = 0x0107,
		WM_KEYLAST                = 0x0108,
		WM_IME_STARTCOMPOSITION   = 0x010D,
		WM_IME_ENDCOMPOSITION     = 0x010E,
		WM_IME_COMPOSITION        = 0x010F,
		WM_IME_KEYLAST            = 0x010F,
		WM_INITDIALOG             = 0x0110,
		WM_COMMAND                = 0x0111,
		WM_SYSCOMMAND             = 0x0112,
		WM_TIMER                  = 0x0113,
		WM_HSCROLL                = 0x0114,
		WM_VSCROLL                = 0x0115,
		WM_INITMENU               = 0x0116,
		WM_INITMENUPOPUP          = 0x0117,
		WM_MENUSELECT             = 0x011F,
		WM_MENUCHAR               = 0x0120,
		WM_ENTERIDLE              = 0x0121,
		WM_MENURBUTTONUP          = 0x0122,
		WM_MENUDRAG               = 0x0123,
		WM_MENUGETOBJECT          = 0x0124,
		WM_UNINITMENUPOPUP        = 0x0125,
		WM_MENUCOMMAND            = 0x0126,
		WM_CTLCOLORMSGBOX         = 0x0132,
		WM_CTLCOLOREDIT           = 0x0133,
		WM_CTLCOLORLISTBOX        = 0x0134,
		WM_CTLCOLORBTN            = 0x0135,
		WM_CTLCOLORDLG            = 0x0136,
		WM_CTLCOLORSCROLLBAR      = 0x0137,
		WM_CTLCOLORSTATIC         = 0x0138,
		WM_MOUSEMOVE              = 0x0200,
		WM_LBUTTONDOWN            = 0x0201,
		WM_LBUTTONUP              = 0x0202,
		WM_LBUTTONDBLCLK          = 0x0203,
		WM_RBUTTONDOWN            = 0x0204,
		WM_RBUTTONUP              = 0x0205,
		WM_RBUTTONDBLCLK          = 0x0206,
		WM_MBUTTONDOWN            = 0x0207,
		WM_MBUTTONUP              = 0x0208,
		WM_MBUTTONDBLCLK          = 0x0209,
		WM_MOUSEWHEEL             = 0x020A,
		WM_XBUTTONDOWN            = 0x020B,
		WM_XBUTTONUP              = 0x020C,
		WM_XBUTTONDBLCLK          = 0x020D,
		WM_PARENTNOTIFY           = 0x0210,
		WM_ENTERMENULOOP          = 0x0211,
		WM_EXITMENULOOP           = 0x0212,
		WM_NEXTMENU               = 0x0213,
		WM_SIZING                 = 0x0214,
		WM_CAPTURECHANGED         = 0x0215,
		WM_MOVING                 = 0x0216,
		WM_DEVICECHANGE           = 0x0219,
		WM_MDICREATE              = 0x0220,
		WM_MDIDESTROY             = 0x0221,
		WM_MDIACTIVATE            = 0x0222,
		WM_MDIRESTORE             = 0x0223,
		WM_MDINEXT                = 0x0224,
		WM_MDIMAXIMIZE            = 0x0225,
		WM_MDITILE                = 0x0226,
		WM_MDICASCADE             = 0x0227,
		WM_MDIICONARRANGE         = 0x0228,
		WM_MDIGETACTIVE           = 0x0229,
		WM_MDISETMENU             = 0x0230,
		WM_ENTERSIZEMOVE          = 0x0231,
		WM_EXITSIZEMOVE           = 0x0232,
		WM_DROPFILES              = 0x0233,
		WM_MDIREFRESHMENU         = 0x0234,
		WM_IME_SETCONTEXT         = 0x0281,
		WM_IME_NOTIFY             = 0x0282,
		WM_IME_CONTROL            = 0x0283,
		WM_IME_COMPOSITIONFULL    = 0x0284,
		WM_IME_SELECT             = 0x0285,
		WM_IME_CHAR               = 0x0286,
		WM_IME_REQUEST            = 0x0288,
		WM_IME_KEYDOWN            = 0x0290,
		WM_IME_KEYUP              = 0x0291,
		WM_MOUSEHOVER             = 0x02A1,
		WM_MOUSELEAVE             = 0x02A3,
		WM_CUT                    = 0x0300,
		WM_COPY                   = 0x0301,
		WM_PASTE                  = 0x0302,
		WM_CLEAR                  = 0x0303,
		WM_UNDO                   = 0x0304,
		WM_RENDERFORMAT           = 0x0305,
		WM_RENDERALLFORMATS       = 0x0306,
		WM_DESTROYCLIPBOARD       = 0x0307,
		WM_DRAWCLIPBOARD          = 0x0308,
		WM_PAINTCLIPBOARD         = 0x0309,
		WM_VSCROLLCLIPBOARD       = 0x030A,
		WM_SIZECLIPBOARD          = 0x030B,
		WM_ASKCBFORMATNAME        = 0x030C,
		WM_CHANGECBCHAIN          = 0x030D,
		WM_HSCROLLCLIPBOARD       = 0x030E,
		WM_QUERYNEWPALETTE        = 0x030F,
		WM_PALETTEISCHANGING      = 0x0310,
		WM_PALETTECHANGED         = 0x0311,
		WM_HOTKEY                 = 0x0312,
		WM_PRINT                  = 0x0317,
		WM_PRINTCLIENT            = 0x0318,
		WM_HANDHELDFIRST          = 0x0358,
		WM_HANDHELDLAST           = 0x035F,
		WM_AFXFIRST               = 0x0360,
		WM_AFXLAST                = 0x037F,
		WM_PENWINFIRST            = 0x0380,
		WM_PENWINLAST             = 0x038F,
		WM_APP                    = 0x8000,
		WM_USER                   = 0x0400,
	}
}
