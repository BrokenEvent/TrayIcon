using System;
using System.Runtime.InteropServices;

namespace BrokenEvent.Shared
{
  /// <summary>
  /// Windows User32 DLL declarations
  /// </summary>
  public class User32
  {
    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ShowWindow")]
    public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowPos")]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetLayeredWindowAttributes")]
    public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, LayeredWindowCommands dwFlags);

    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "RedrawWindow")]
    public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ChangeWindowMessageFilterEx")]
    public static extern bool ChangeWindowMessageFilterEx(IntPtr hWnd, int message, MessageFilterActions action, IntPtr pChangeFilterStruct);

    // Note: MessageFilterActions.Reset is not applicable here
    [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ChangeWindowMessageFilter")]
    public static extern bool ChangeWindowMessageFilter(int message, MessageFilterActions action);
  }

  public enum MessageFilterActions
  {
    /// <summary>
    /// Allows the message through the filter. This enables the message to be received by hWnd, regardless of the source of the message, even it comes from a lower privileged process. 
    /// </summary>
    Allow = 1,

    /// <summary>
    /// Blocks the message to be delivered to hWnd if it comes from a lower privileged process, unless the message is allowed process-wide by using the ChangeWindowMessageFilter function or globally. 
    /// </summary>
    Disallow = 2,

    /// <summary>
    /// Resets the window message filter for hWnd to the default. Any message allowed globally or process-wide will get through, but any message not included in those two categories, and which comes from a lower privileged process, will be blocked.
    /// </summary>
    Reset = 0
  }

  [Flags]
  public enum RedrawWindowFlags
  {
    /// <summary>
    /// Causes the window to receive a WM_ERASEBKGND message when the window is repainted. The RDW_INVALIDATE flag must also be specified; otherwise, RDW_ERASE has no effect.
    /// </summary>
    RDW_ERASE = 4,

    /// <summary>
    /// Causes any part of the nonclient area of the window that intersects the update region to receive a WM_NCPAINT message. The RDW_INVALIDATE flag must also be specified; otherwise, RDW_FRAME has no effect. The WM_NCPAINT message is typically not sent during the execution of RedrawWindow unless either RDW_UPDATENOW or RDW_ERASENOW is specified.
    /// </summary>
    RDW_FRAME = 1024,

    /// <summary>
    /// Causes a WM_PAINT message to be posted to the window regardless of whether any portion of the window is invalid.
    /// </summary>
    RDW_INTERNALPAINT = 2,

    /// <summary>
    /// Invalidates lprcUpdate or hrgnUpdate (only one may be non-NULL). If both are NULL, the entire window is invalidated.
    /// </summary>
    RDW_INVALIDATE = 1,

    /// <summary>
    /// Suppresses any pending WM_ERASEBKGND messages.
    /// </summary>
    RDW_NOERASE = 32,

    /// <summary>
    /// Suppresses any pending WM_NCPAINT messages. This flag must be used with RDW_VALIDATE and is typically used with RDW_NOCHILDREN. RDW_NOFRAME should be used with care, as it could cause parts of a window to be painted improperly.
    /// </summary>
    RDW_NOFRAME = 2048,

    /// <summary>
    /// Suppresses any pending internal WM_PAINT messages. This flag does not affect WM_PAINT messages resulting from a non-NULL update area.
    /// </summary>
    RDW_NOINTERNALPAINT = 16,

    /// <summary>
    /// Validates lprcUpdate or hrgnUpdate (only one may be non-NULL). If both are NULL, the entire window is validated. This flag does not affect internal WM_PAINT messages.
    /// </summary>
    RDW_VALIDATE = 8,

    /// <summary>
    /// Causes the affected windows (as specified by the RDW_ALLCHILDREN and RDW_NOCHILDREN flags) to receive WM_NCPAINT and WM_ERASEBKGND messages, if necessary, before the function returns. WM_PAINT messages are received at the ordinary time.
    /// </summary>
    RDW_ERASENOW = 512,

    /// <summary>
    /// Causes the affected windows (as specified by the RDW_ALLCHILDREN and RDW_NOCHILDREN flags) to receive WM_NCPAINT, WM_ERASEBKGND, and WM_PAINT messages, if necessary, before the function returns.
    /// </summary>
    RDW_UPDATENOW = 256,

    /// <summary>
    /// Includes child windows, if any, in the repainting operation.
    /// </summary>
    RDW_ALLCHILDREN = 128,

    /// <summary>
    /// Excludes child windows, if any, from the repainting operation.
    /// </summary>
    RDW_NOCHILDREN = 64,
  }

  [Flags]
  public enum LayeredWindowCommands
  {
    /// <summary>
    /// Use bAlpha to determine the opacity of the layered window.
    /// </summary>
    LWA_ALPHA = 0x00000002,

    /// <summary>
    /// Use crKey as the transparency color.
    /// </summary>
    LWA_COLORKEY = 0x00000001
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

  [Flags]
  public enum WindowClassStyles: uint
  {
    /// <summary>
    /// Aligns the window's client area on a byte boundary (in the x direction). This style affects the width of the window and its horizontal placement on the display.
    /// </summary>
    CS_BYTEALIGNCLIENT = 0x1000,

    /// <summary>
    /// Aligns the window on a byte boundary (in the x direction). This style affects the width of the window and its horizontal placement on the display.
    /// </summary>
    CS_BYTEALIGNWINDOW = 0x2000,

    /// <summary>
    /// Allocates one device context to be shared by all windows in the class. Because window classes are process specific, it is possible for multiple threads of an application to create a window of the same class. It is also possible for the threads to attempt to use the device context simultaneously. When this happens, the system allows only one thread to successfully finish its drawing operation. 
    /// </summary>
    CS_CLASSDC = 0x0040,

    /// <summary>
    /// Sends a double-click message to the window procedure when the user double-clicks the mouse while the cursor is within a window belonging to the class. 
    /// </summary>
    CS_DBLCLKS = 0x0008,

    /// <summary>
    /// Enables the drop shadow effect on a window. The effect is turned on and off through SPI_SETDROPSHADOW. Typically, this is enabled for small, short-lived windows such as menus to emphasize their Z-order relationship to other windows. Windows created from a class with this style must be top-level windows; they may not be child windows.
    /// </summary>
    CS_DROPSHADOW = 0x00020000,

    /// <summary>
    /// Indicates that the window class is an application global class. For more information, see the "Application Global Classes" section of About Window Classes.
    /// </summary>
    CS_GLOBALCLASS = 0x4000,

    /// <summary>
    /// Redraws the entire window if a movement or size adjustment changes the width of the client area.
    /// </summary>
    CS_HREDRAW = 0x0002,

    /// <summary>
    /// Disables Close on the window menu.
    /// </summary>
    CS_NOCLOSE = 0x0200,

    /// <summary>
    /// Allocates a unique device context for each window in the class. 
    /// </summary>
    CS_OWNDC = 0x0020,

    /// <summary>
    /// Sets the clipping rectangle of the child window to that of the parent window so that the child can draw on the parent. A window with the CS_PARENTDC style bit receives a regular device context from the system's cache of device contexts. It does not give the child the parent's device context or device context settings. Specifying CS_PARENTDC enhances an application's performance. 
    /// </summary>
    CS_PARENTDC = 0x0080,

    /// <summary>
    /// Saves, as a bitmap, the portion of the screen image obscured by a window of this class. When the window is removed, the system uses the saved bitmap to restore the screen image, including other windows that were obscured. Therefore, the system does not send WM_PAINT messages to windows that were obscured if the memory used by the bitmap has not been discarded and if other screen actions have not invalidated the stored image. 
    /// This style is useful for small windows (for example, menus or dialog boxes) that are displayed briefly and then removed before other screen activity takes place. This style increases the time required to display the window, because the system must first allocate memory to store the bitmap.
    /// </summary>
    CS_SAVEBITS = 0x0800,

    /// <summary>
    /// Redraws the entire window if a movement or size adjustment changes the height of the client area.
    /// </summary>
    CS_VREDRAW = 0x0001  
  }

  [Flags]
  public enum WindowClassExFlags: uint
  {
    /// <summary>
    /// The window accepts drag-drop files.
    /// </summary>
    WS_EX_ACCEPTFILES = 0x00000010,

    /// <summary>
    /// Forces a top-level window onto the taskbar when the window is visible. 
    /// </summary>
    WS_EX_APPWINDOW = 0x00040000,

    /// <summary>
    /// The window has a border with a sunken edge.
    /// </summary>
    WS_EX_CLIENTEDGE = 0x00000200,

    /// <summary>
    /// Paints all descendants of a window in bottom-to-top painting order using double-buffering. For more information, see Remarks. This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. 
    /// Windows 2000:  This style is not supported.
    /// </summary>
    WS_EX_COMPOSITED = 0x02000000,

    /// <summary>
    /// The title bar of the window includes a question mark. When the user clicks the question mark, the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message. The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command. The Help application displays a pop-up window that typically contains help for the child window.
    /// WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
    /// </summary>
    WS_EX_CONTEXTHELP = 0x00000400,

    /// <summary>
    /// The window itself contains child windows that should take part in dialog box navigation. If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.
    /// </summary>    
    WS_EX_CONTROLPARENT = 0x00010000,

    /// <summary>
    /// The window has a double border; the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.
    /// </summary>
    WS_EX_DLGMODALFRAME = 0x00000001,

    /// <summary>
    /// The window is a layered window. This style cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
    /// Windows 8:  The WS_EX_LAYERED style is supported for top-level windows and child windows. Previous Windows versions support WS_EX_LAYERED only for top-level windows.
    /// </summary>
    WS_EX_LAYERED = 0x00080000,

    /// <summary>
    /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the horizontal origin of the window is on the right edge. Increasing horizontal values advance to the left. 
    /// </summary>
    WS_EX_LAYOUTRTL = 0x00400000,

    /// <summary>
    /// The window has generic left-aligned properties. This is the default.
    /// </summary>
    WS_EX_LEFT = 0x00000000,

    /// <summary>
    /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical scroll bar (if present) is to the left of the client area. For other languages, the style is ignored.
    /// </summary>
    WS_EX_LEFTSCROLLBAR = 0x00004000,

    /// <summary>
    /// The window text is displayed using left-to-right reading-order properties. This is the default.
    /// </summary>
    WS_EX_LTRREADING = 0x00000000,

    /// <summary>
    /// The window is a MDI child window.
    /// </summary>
    WS_EX_MDICHILD = 0x00000040,

    /// <summary>
    /// A top-level window created with this style does not become the foreground window when the user clicks it. The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
    /// To activate the window, use the SetActiveWindow or SetForegroundWindow function.
    /// The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
    /// </summary>
    WS_EX_NOACTIVATE = 0x08000000,

    /// <summary>
    /// The window does not pass its window layout to its child windows.
    /// </summary>
    WS_EX_NOINHERITLAYOUT = 0x00100000,

    /// <summary>
    /// The child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
    /// </summary>
    WS_EX_NOPARENTNOTIFY = 0x00000004,

    /// <summary>
    /// The window does not render to a redirection surface. This is for windows that do not have visible content or that use mechanisms other than surfaces to provide their visual.
    /// </summary>
    WS_EX_NOREDIRECTIONBITMAP = 0x00200000,

    /// <summary>
    /// The window is an overlapped window.
    /// </summary>
    WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE),

    /// <summary>
    /// The window is palette window, which is a modeless dialog box that presents an array of commands. 
    /// </summary>
    WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST),

    /// <summary>
    /// The window has generic "right-aligned" properties. This depends on the window class. This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the style is ignored.
    /// Using the WS_EX_RIGHT style for static or edit controls has the same effect as using the SS_RIGHT or ES_RIGHT style, respectively. Using this style with button controls has the same effect as using BS_RIGHT and BS_RIGHTBUTTON styles. 
    /// </summary>
    WS_EX_RIGHT = 0x00001000,

    /// <summary>
    /// The vertical scroll bar (if present) is to the right of the client area. This is the default.
    /// </summary>
    WS_EX_RIGHTSCROLLBAR = 0x00000000,

    /// <summary>
    /// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text is displayed using right-to-left reading-order properties. For other languages, the style is ignored.
    /// </summary>
    WS_EX_RTLREADING = 0x00002000,

    /// <summary>
    /// The window has a three-dimensional border style intended to be used for items that do not accept user input.
    /// </summary>
    WS_EX_STATICEDGE = 0x00020000,

    /// <summary>
    /// The window is intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font. A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, its icon is not displayed on the title bar. However, you can display the system menu by right-clicking or by typing ALT+SPACE. 
    /// </summary>
    WS_EX_TOOLWINDOW = 0x00000080,

    /// <summary>
    /// The window should be placed above all non-topmost windows and should stay above them, even when the window is deactivated. To add or remove this style, use the SetWindowPos function.
    /// </summary>
    WS_EX_TOPMOST = 0x00000008,

    /// <summary>
    /// The window should not be painted until siblings beneath the window (that were created by the same thread) have been painted. The window appears transparent because the bits of underlying sibling windows have already been painted.
    /// To achieve transparency without these restrictions, use the SetWindowRgn function.
    /// </summary>
    WS_EX_TRANSPARENT = 0x00000020,

    /// <summary>
    /// The window has a border with a raised edge.
    /// </summary>
    WS_EX_WINDOWEDGE = 0x00000100,
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
    WM_THEMECHANGED           = 0x031A,
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
