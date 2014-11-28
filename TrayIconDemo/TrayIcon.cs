using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BrokenEvent.Shared
{
  /// <summary>
  /// Specifies a component that creates an icon in the notification area. This class cannot be inherited.
  /// </summary>
  /// <filterpriority>2</filterpriority>
  [DefaultEvent("MouseDoubleClick")]
  [DefaultProperty("Text")]
  class TrayIcon : Component
  {
    #region P/Invoke and structures

    [DllImport("user32.dll", EntryPoint = "RegisterWindowMessage", CharSet = CharSet.Auto)]
    private static extern int RegisterWindowMessage(string text);

    [DllImport("shell32.dll", CharSet = CharSet.Auto, EntryPoint = "Shell_NotifyIcon")]
    private static extern bool Shell_NotifyIcon(NotifyIconMessages message, NOTIFYICONDATA pnid);

    [DllImport("shell32.dll", CharSet = CharSet.Auto, EntryPoint = "Shell_NotifyIcon")]
    private static extern bool Shell_NotifyIcon(NotifyIconMessages message, NOTIFYICONDATA4 pnid);

    [DllImport("shell32.dll", CharSet = CharSet.Auto, EntryPoint = "Shell_NotifyIconGetRect")]
    private static extern int Shell_NotifyIconGetRect(ref NOTIFYICONIDENTIFIER identifier, ref WinAPI.RECT iconLocation);

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, EntryPoint = "SetForegroundWindow")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private class NOTIFYICONDATA
    {
      public int cbSize = Marshal.SizeOf(typeof (NOTIFYICONDATA));
      public IntPtr hWnd;
      public int uID;
      public NotifyIconFlags uFlags;
      public int uCallbackMessage;
      public IntPtr hIcon;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
      public string szTip;

      public NofityIconStates dwState;
      public int dwStateMask;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
      public string szInfo;

      public int uTimeoutOrVersion;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
      public string szInfoTitle;

      public NotifyIconInfoFlags dwInfoFlags;
      public Guid guidItem;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private class NOTIFYICONDATA4 : NOTIFYICONDATA
    {
      public NOTIFYICONDATA4()
      {
        cbSize = Marshal.SizeOf(typeof(NOTIFYICONDATA4));
      }
      public IntPtr hBalloonIcon;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NOTIFYICONIDENTIFIER
    {
      public uint cbSize;
      public IntPtr hWnd;
      public uint uID;
      public Guid guidItem;
    }

    [Flags]
    private enum NotifyIconFlags
    {
      /// <summary>
      /// The uCallbackMessage member is valid.
      /// </summary>
      Message = 0x00000001,
      /// <summary>
      /// The hIcon member is valid.
      /// </summary>
      Icon = 0x00000002,
      /// <summary>
      /// The szTip member is valid.
      /// </summary>
      Tip = 0x00000004,
      /// <summary>
      /// The dwState and dwStateMask members are valid.
      /// </summary>
      State = 0x00000008,
      /// <summary>
      /// Display a balloon notification. The szInfo, szInfoTitle, dwInfoFlags, and uTimeout members are valid. Note that uTimeout is valid only in Windows 2000 and Windows XP. 
      /// To display the balloon notification, specify <see cref="NotifyIconFlags.Info"/> and provide text in szInfo.
      /// To remove a balloon notification, specify <see cref="NotifyIconFlags.Info"/> and provide an empty string through szInfo.
      /// To add a notification area icon without displaying a notification, do not set the <see cref="NotifyIconFlags.Info"/> flag.
      /// </summary>
      Info = 0x00000010,
      /// <summary>
      /// Windows 7 and later: The guidItem is valid.
      /// Windows Vista and earlier: Reserved.
      /// </summary>
      Guid = 0x00000020,
      /// <summary>
      /// Windows Vista and later. If the balloon notification cannot be displayed immediately, discard it. Use this flag for notifications that represent real-time information which would be meaningless or misleading if displayed at a later time. For example, a message that states "Your telephone is ringing." <see cref="NotifyIconFlags.RealTime"/> is meaningful only when combined with the <see cref="NotifyIconFlags.Info"/> flag.
      /// </summary>
      RealTime = 0x00000040,
      /// <summary>
      /// Windows Vista and later. Use the standard tooltip. Normally, when uVersion is set to NOTIFYICON_VERSION_4, the standard tooltip is suppressed and can be replaced by the application-drawn, pop-up UI. If the application wants to show the standard tooltip with NOTIFYICON_VERSION_4, it can specify <see cref="NotifyIconFlags.ShowTip"/> to indicate the standard tooltip should still be shown.
      /// </summary>
      ShowTip = 0x00000080
    }

    [Flags]
    private enum NofityIconStates
    {
      /// <summary>
      /// The icon is hidden.
      /// </summary>
      Hidden = 0x00000001,
      /// <summary>
      /// The icon resource is shared between multiple icons.
      /// </summary>
      SharedIcon = 0x00000002,
    }

    [Flags]
    private enum NotifyIconInfoFlags
    {
      /// <summary>
      /// No icon.
      /// </summary>
      IconNone = 0x00000000,
      /// <summary>
      /// An information icon.
      /// </summary>
      IconInfo = 0x00000001,
      /// <summary>
      /// A warning icon.
      /// </summary>
      IconWarning = 0x00000002,
      /// <summary>
      /// An error icon.
      /// </summary>
      IconError = 0x00000003,
      /// <summary>
      /// Windows XP SP2 and later. 
      /// Windows XP: Use the icon identified in hIcon as the notification balloon's title icon.
      /// Windows Vista and later: Use the icon identified in hBalloonIcon as the notification balloon's title icon.
      /// </summary>
      IconUser = 0x00000004,
      /// <summary>
      /// Windows XP and later. Do not play the associated sound. Applies only to notifications.
      /// </summary>
      NoSound = 0x00000010,
      /// <summary>
      /// Windows Vista and later. The large version of the icon should be used as the notification icon. This corresponds to the icon with dimensions SM_CXICON x SM_CYICON. If this flag is not set, the icon with dimensions XM_CXSMICON x SM_CYSMICON is used.
      /// This flag can be used with all stock icons.
      /// Applications that use older customized icons (NIIF_USER with hIcon) must provide a new SM_CXICON x SM_CYICON version in the tray icon (hIcon). These icons are scaled down when they are displayed in the System Tray or System Control Area (SCA).
      /// New customized icons (NIIF_USER with hBalloonIcon) must supply an SM_CXICON x SM_CYICON version in the supplied icon (hBalloonIcon).
      /// </summary>
      LargeIcon = 0x00000020,
      /// <summary>
      /// Windows 7 and later. Do not display the balloon notification if the current user is in "quiet time", which is the first hour after a new user logs into his or her account for the first time. During this time, most notifications should not be sent or shown.
      /// This lets a user become accustomed to a new computer system without those distractions. Quiet time also occurs for each user after an operating system upgrade or clean installation. A notification sent with this flag during quiet time is not queued; it is simply dismissed unshown.
      /// The application can resend the notification later if it is still valid at that time.
      /// Because an application cannot predict when it might encounter quiet time, we recommended that this flag always be set on all appropriate notifications by any application that means to honor quiet time.
      /// During quiet time, certain notifications should still be sent because they are expected by the user as feedback in response to a user action, for instance when he or she plugs in a USB device or prints a document.
      /// If the current user is not in quiet time, this flag has no effect.
      /// </summary>
      RespectQuietTime = 0x00000080,
      /// <summary>
      /// Windows XP and later. Reserved.
      /// </summary>
      IconMask = 0x0000000F,
    }

    private enum NotifyIconMessages
    {
      /// <summary>
      /// Adds an icon to the status area. The icon is given an identifier in the NOTIFYICONDATA structure pointed to by lpdata—either through its uID or guidItem member. This identifier is used in subsequent calls to <see cref="Shell_NotifyIcon"/> to perform later actions on the icon.
      /// </summary>
      Add = 0x00000000,
      /// <summary>
      /// Modifies an icon in the status area. NOTIFYICONDATA structure pointed to by lpdata uses the ID originally assigned to the icon when it was added to the notification area (<see cref="Add"/>) to identify the icon to be modified.
      /// </summary>
      Modify = 0x00000001,
      /// <summary>
      /// Deletes an icon from the status area. NOTIFYICONDATA structure pointed to by lpdata uses the ID originally assigned to the icon when it was added to the notification area (<see cref="Add"/>) to identify the icon to be deleted.
      /// </summary>
      Delete = 0x00000002,
      /// <summary>
      /// Shell32.dll version 5.0 and later only. Returns focus to the taskbar notification area. Notification area icons should use this message when they have completed their UI operation. For example, if the icon displays a shortcut menu, but the user presses ESC to cancel it, use <see cref="SetFocus"/> to return focus to the notification area.
      /// </summary>
      SetFocus = 0x00000003,
      /// <summary>
      /// Shell32.dll version 5.0 and later only. Instructs the notification area to behave according to the version number specified in the uVersion member of the structure pointed to by lpdata. The version number specifies which members are recognized.
      /// <see cref="SetVersion"/> must be called every time a notification area icon is added (<see cref="Add"/>). It does not need to be called with <see cref="Modify"/>. The version setting is not persisted once a user logs off.
      /// </summary>
      SetVersion = 0x00000004,
    }

    #endregion

    private string hintText = string.Empty;
    private string longHintText = string.Empty;
    private Icon icon;
    private Form ownerForm;
    private bool enabled;
    private bool iconAdded;
    private int id;
    private bool doubleClick;
    private ContextMenuStrip contextMenu;
    private bool useVersion4;
    private bool trimLongText;
    private bool useLargeIcons = true;
    private Guid guid;
    private bool showDefaultTips = true;
    private HintWindow hintWindow;
    private bool showOwnerUI = false;
    private bool customHintVisible;
    private Point customHintPosition;

    private static readonly object EVENT_MOUSEDOWN = new object();
    private static readonly object EVENT_MOUSEMOVE = new object();
    private static readonly object EVENT_MOUSEUP = new object();
    private static readonly object EVENT_MOUSECLICK = new object();
    private static readonly object EVENT_MOUSEDOUBLECLICK = new object();
    private static readonly object EVENT_BALLOONTIPSHOWN = new object();
    private static readonly object EVENT_BALLOONTIPCLICKED = new object();
    private static readonly object EVENT_BALLOONTIPCLOSED = new object();
    private static readonly object EVENT_TOOLTIPSHOWN = new object();
    private static readonly object EVENT_TOOLTIPCLOSED = new object();
    private static readonly object EVENT_TOOLTIPMEASURE = new object();
    private static readonly object EVENT_TOOLTIPPAINT = new object();
    private static readonly object EVENT_CONTEXTMENUSHOW = new object();
    private static int trayIconId = 0;
    private static int WM_TASKBARCREATED = RegisterWindowMessage("TaskbarCreated");
    private const int CALLBACK_MESSAGE = 2048;

    public TrayIcon()
    {
      id = ++trayIconId;

      useVersion4 = WindowsVersionDetector.RunningOnVista;
    }

    #region Public properties

    [Description("Tray icon text. Should be less than 64 chars")]
    public string HintText
    {
      get { return hintText; }
      set
      {
        if (hintText == value)
          return;

        if (hintText.Length >= 128)
        {
          if (trimLongText)
            value = value.Substring(0, 124) + "...";
          else
            throw new ArgumentOutOfRangeException("value", hintText.Length, "Hint text should be less than 128 chars long");
        }
        hintText = value;
        if (enabled)
          UpdateIcon();
      }
    }

    [Description("Hint text to be shown in hint when running on Vista+")]
    public string LongHintText
    {
      get { return longHintText; }
      set
      {
        if (longHintText == value)
          return;

        longHintText = value;
      }
    }

    [Description("Tray icon")]
    public Icon Icon
    {
      get { return icon; }
      set
      {
        if (icon == value)
          return;
        icon = value;
        if (enabled)
          UpdateIcon();
      }
    }

    [Description("Owner form")]
    public Form OwnerForm
    {
      get { return ownerForm; }
      set
      {
        ownerForm = value;
        if (enabled)
          UpdateIcon();
      }
    }

    [Description("Show custom UI instead of standart Hint")]
    public bool ShowOwnerUI
    {
      get { return showOwnerUI; }
      set { showOwnerUI = value; }
    }

    [Description("If the icon is visible")]
    public bool Enabled
    {
      get { return enabled; }
      set
      {
        if (enabled == value)
          return;
        enabled = value;
        if (value)
          UpdateIcon();
        else
          RemoveIcon();
      }
    }

    [Description("GUID for this icon of this application")]
    public Guid Guid
    {
      get { return guid; }
      set { guid = value; }
    }

    [Description("Context menu shown when user releases right mouse button on the tray icon")]
    public ContextMenuStrip ContextMenu
    {
      get { return contextMenu; }
      set { contextMenu = value; }
    }

    [Description("Trim long text in tray icon params if is too long. Will throw exception otherwise")]
    [DefaultValue(false)]
    public bool TrimLongText
    {
      get { return trimLongText; }
      set { trimLongText = value; }
    }

    [Description("Use large system icons is it is possible (WinVista or later)")]
    [DefaultValue(true)]
    public bool UseLargeIcons
    {
      get { return useLargeIcons; }
      set { useLargeIcons = value; }
    }

    [Description("Show default tips instead of user-defined UI when running on Vista or later")]
    [DefaultValue(true)]
    public bool ShowDefaultTips
    {
      get { return showDefaultTips; }
      set
      {
        showDefaultTips = value;
        if (enabled)
          UpdateIcon();
      }
    }

    /// <summary>
    /// Returns true if Vista+ custom hint is visible (currently within <see cref="TooltipShown"/> and <see cref="TooltipClosed"/> events) and false otherwise
    /// </summary>
    [Browsable(false)]
    [ReadOnly(true)]
    public bool CustomHintVisible
    {
      get { return customHintVisible; }
    }

    #endregion

    #region Public events

    /// <summary>
    /// Occurs when the balloon tip is clicked.
    /// </summary>
    [Description("Occurs when the balloon tip is clicked.")]
    public event EventHandler BalloonTipClicked
    {
      add
      {
        Events.AddHandler(EVENT_BALLOONTIPCLICKED, value);
      }
      remove
      {
        Events.RemoveHandler(EVENT_BALLOONTIPCLICKED, value);
      }
    }

    /// <summary>
    /// Occurs when the context menu is required to show
    /// </summary>
    [Description("Occurs when the context menu is required to show")]
    public event EventHandler ContextMenuShowing
    {
      add
      {
        Events.AddHandler(EVENT_CONTEXTMENUSHOW, value);
      }
      remove
      {
        Events.RemoveHandler(EVENT_CONTEXTMENUSHOW, value);
      }
    }

    /// <summary>
    /// Occurs when the tooltip is being shown.
    /// Works only when running on Vista and <see cref="ShowDefaultTips"/> is false
    /// </summary>
    [Description("Occurs when the tooltip is being shown")]
    public event EventHandler<TooltipShowArgs> TooltipShown
    {
      add
      {
        Events.AddHandler(EVENT_TOOLTIPSHOWN, value);
      }
      remove
      {
        Events.RemoveHandler(EVENT_TOOLTIPSHOWN, value);
      }
    }

    /// <summary>
    /// Occurs when the tooltip is closed.
    /// Works only when running on Vista and <see cref="ShowDefaultTips"/> is false
    /// </summary>
    [Description("Occurs when the tooltip is closed.")]
    public event EventHandler TooltipClosed
    {
      add
      {
        Events.AddHandler(EVENT_TOOLTIPCLOSED, value);
      }
      remove
      {
        Events.RemoveHandler(EVENT_TOOLTIPCLOSED, value);
      }
    }

    /// <summary>
    /// Occurs when the tooltip is being shown and its size has to be measured.
    /// Works only when running on Vista and <see cref="ShowDefaultTips"/> and
    /// <see cref="ShowOwnerUI"/> are false.
    /// </summary>
    [Description("Occurs when the tooltip is being shown and its size has to be measured.")]
    public event EventHandler<HintMeasureEventArgs> TooltipMeasure
    {
      add
      {
        Events.AddHandler(EVENT_TOOLTIPMEASURE, value);
      }
      remove
      {
        Events.RemoveHandler(EVENT_TOOLTIPMEASURE, value);
      }
    }

    /// <summary>
    /// Occurs when the tooltip is being painted.
    /// Works only when running on Vista and <see cref="ShowDefaultTips"/> and
    /// <see cref="ShowOwnerUI"/> are false.
    /// </summary>
    [Description("Occurs when the tooltip is being painted.")]
    public event EventHandler<HintPaintEventArgs> TooltipPaint
    {
      add
      {
        Events.AddHandler(EVENT_TOOLTIPPAINT, value);
      }
      remove
      {
        Events.RemoveHandler(EVENT_TOOLTIPPAINT, value);
      }
    }

    /// <summary>
    /// Occurs when the balloon tip is closed by the user.
    /// </summary>
    [Description("Occurs when the balloon tip is closed by the user.")]
    public event EventHandler BalloonTipClosed
    {
      add
      {
        Events.AddHandler(EVENT_BALLOONTIPCLOSED, value);
      }
      remove
      {
        Events.RemoveHandler(EVENT_BALLOONTIPCLOSED, value);
      }
    }

    /// <summary>
    /// Occurs when the balloon tip is displayed on the screen.
    /// </summary>
    [Description("Occurs when the balloon tip is displayed on the screen.")]
    public event EventHandler BalloonTipShown
    {
      add
      {
        Events.AddHandler(EVENT_BALLOONTIPSHOWN, value);
      }
      remove
      {
        Events.RemoveHandler(EVENT_BALLOONTIPSHOWN, value);
      }
    }

    /// <summary>
    /// Occurs when the user clicks a <see cref="TrayIcon"/> with the mouse.
    /// </summary>
    [Description("Occurs when the user clicks a TrayIcon with the mouse.")]
    public event MouseEventHandler MouseClick
    {
      add
      {
        Events.AddHandler(EVENT_MOUSECLICK, value);
      }
      remove
      {
        Events.RemoveHandler(EVENT_MOUSECLICK, value);
      }
    }

    /// <summary>
    /// Occurs when the user double-clicks the <see cref="TrayIcon"/> with the mouse.
    /// </summary>
    [Description("Occurs when the user double-clicks the TrayIcon with the mouse.")]
    public event MouseEventHandler MouseDoubleClick
    {
      add
      {
        Events.AddHandler(EVENT_MOUSEDOUBLECLICK, value);
      }
      remove
      {
        Events.RemoveHandler(EVENT_MOUSEDOUBLECLICK, value);
      }
    }

    /// <summary>
    /// Occurs when the user presses the mouse button while the pointer is over the icon in the notification area of the taskbar.
    /// </summary>
    [Description("Occurs when the user presses the mouse button while the pointer is over the icon in the notification area of the taskbar.")]
    public event MouseEventHandler MouseDown
    {
      add
      {
        Events.AddHandler(EVENT_MOUSEDOWN, value);
      }
      remove
      {
        Events.RemoveHandler(EVENT_MOUSEDOWN, value);
      }
    }

    /// <summary>
    /// Occurs when the user moves the mouse while the pointer is over the icon in the notification area of the taskbar.
    /// </summary>
    [Description("Occurs when the user moves the mouse while the pointer is over the icon in the notification area of the taskbar.")]
    public event MouseEventHandler MouseMove
    {
      add
      {
        Events.AddHandler(EVENT_MOUSEMOVE, value);
      }
      remove
      {
        Events.RemoveHandler(EVENT_MOUSEMOVE, value);
      }
    }

    /// <summary>
    /// Occurs when the user releases the mouse button while the pointer is over the icon in the notification area of the taskbar.
    /// </summary>
    [Description("Occurs when the user releases the mouse button while the pointer is over the icon in the notification area of the taskbar.")]
    public event MouseEventHandler MouseUp
    {
      add
      {
        Events.AddHandler(EVENT_MOUSEUP, value);
      }
      remove
      {
        Events.RemoveHandler(EVENT_MOUSEUP, value);
      }
    }

    #endregion

    #region Input proxies

    private void OnBalloonTipClicked()
    {
      EventHandler eventHandler = (EventHandler)Events[EVENT_BALLOONTIPCLICKED];
      if (eventHandler == null)
        return;
      eventHandler(this, EventArgs.Empty);
    }

    private void OnToolTipShown(int x, int y)
    {
      if (showDefaultTips)
        return;

      customHintVisible = true;

      if (showOwnerUI)
      {
        EventHandler<TooltipShowArgs> eventHandler = (EventHandler<TooltipShowArgs>)Events[EVENT_TOOLTIPSHOWN];
        if (eventHandler == null)
          return;
        eventHandler(this, new TooltipShowArgs(new Point(x, y)));
      }

      if (ownerForm != null && useVersion4)
        hintWindow = new HintWindow(IntPtr.Zero, this);

      hintWindow.Show(customHintPosition = new Point(x, y));

    }

    private void OnToolTipClosed()
    {
      if (showDefaultTips)
        return;
     
      customHintVisible = false;

      if (showOwnerUI)
      {
        EventHandler eventHandler = (EventHandler)Events[EVENT_TOOLTIPCLOSED];
        if (eventHandler == null)
          return;
        eventHandler(this, EventArgs.Empty);
      }
      else
        hintWindow.Hide();
    }

    private void OnBalloonTipClosed()
    {
      EventHandler eventHandler = (EventHandler)Events[EVENT_BALLOONTIPCLOSED];
      if (eventHandler == null)
        return;
      eventHandler(this, EventArgs.Empty);
    }

    private void OnBalloonTipShown()
    {
      EventHandler eventHandler = (EventHandler)Events[EVENT_BALLOONTIPSHOWN];
      if (eventHandler == null)
        return;
      eventHandler(this, EventArgs.Empty);
    }

    private void OnMouseClick(MouseEventArgs mea)
    {
      MouseEventHandler mouseEventHandler = (MouseEventHandler)Events[EVENT_MOUSECLICK];
      if (mouseEventHandler == null)
        return;
      mouseEventHandler(this, mea);
    }

    private void OnMouseDoubleClick(MouseEventArgs mea)
    {
      MouseEventHandler mouseEventHandler = (MouseEventHandler)Events[EVENT_MOUSEDOUBLECLICK];
      if (mouseEventHandler == null)
        return;
      mouseEventHandler(this, mea);
    }

    private void OnMouseDown(MouseEventArgs e)
    {
      MouseEventHandler mouseEventHandler = (MouseEventHandler)Events[EVENT_MOUSEDOWN];
      if (mouseEventHandler == null)
        return;
      mouseEventHandler(this, e);
    }

    private void OnMouseMove(MouseEventArgs e)
    {
      MouseEventHandler mouseEventHandler = (MouseEventHandler)Events[EVENT_MOUSEMOVE];
      if (mouseEventHandler == null)
        return;
      mouseEventHandler(this, e);
    }

    private void OnMouseUp(MouseEventArgs e)
    {
      MouseEventHandler mouseEventHandler = (MouseEventHandler)Events[EVENT_MOUSEUP];
      if (mouseEventHandler == null)
        return;
      mouseEventHandler(this, e);
    }

    private void WmMouseDown(ref Message m, MouseButtons button, int clicks)
    {
      if (clicks == 2)
      {
        OnMouseDoubleClick(new MouseEventArgs(button, 2, 0, 0, 0));
        doubleClick = true;
      }
      OnMouseDown(new MouseEventArgs(button, clicks, 0, 0, 0));
    }

    private void WmMouseMove(ref Message m)
    {
      OnMouseMove(new MouseEventArgs(Control.MouseButtons, 0, 0, 0, 0));
    }

    private void WmMouseUp(ref Message m, MouseButtons button)
    {
      OnMouseUp(new MouseEventArgs(button, 0, 0, 0, 0));
      if (!doubleClick)
      {
        OnMouseClick(new MouseEventArgs(button, 0, 0, 0, 0));
      }
      doubleClick = false;
    }

    private void WmTaskbarCreated()
    {
      iconAdded = false;
      if (enabled)
        UpdateIcon();
    }

    private void OnTooltipMeasure(ref Size size, VisualStyleRenderer renderer)
    {
      EventHandler<HintMeasureEventArgs> handler = (EventHandler<HintMeasureEventArgs>)Events[EVENT_TOOLTIPMEASURE];
      if (handler != null)
      {
        HintMeasureEventArgs args = new HintMeasureEventArgs(size, renderer);
        handler(this, args);
        size = args.Size;
      }
    }

    private bool OnTooltipPaint(Graphics g, Size size, VisualStyleRenderer renderer)
    {
      EventHandler<HintPaintEventArgs> handler = (EventHandler<HintPaintEventArgs>)Events[EVENT_TOOLTIPPAINT];
      if (handler != null)
      {
        handler(this, new HintPaintEventArgs(g, size, renderer));
        return true;
      }

      return false;
    }

    #endregion

    #region Vista Hint

    private class HintWindow : HintNativeWindow
    {
      private readonly TrayIcon owner;
      private VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.Standard.Normal);
      private const int PADDING = 4;

      public HintWindow(IntPtr ownerHandle, TrayIcon owner)
        : base(ownerHandle)
      {
        this.owner = owner;
      }

      protected override void Paint(Graphics g, Size size)
      {
        using (Brush brush = new SolidBrush(TransparencyColor))
          g.FillRectangle(brush, 0, 0, size.Width, size.Height);

        if (owner.OnTooltipPaint(g, size, renderer))
          return;

        string text = string.IsNullOrEmpty(owner.longHintText) ? owner.hintText : owner.longHintText;

        renderer.DrawBackground(g, new Rectangle(Point.Empty, size));
        renderer.DrawText(
            g,
            new Rectangle(
                PADDING,
                PADDING,
                size.Width - PADDING,
                size.Height - PADDING
              ),
            text,
            false,
            TextFormatFlags.TextBoxControl
          );
      }

      protected override void Measure(ref Point pos, out Size size)
      {
        string text = string.IsNullOrEmpty(owner.longHintText) ? owner.hintText : owner.longHintText;

        using (Graphics g = Graphics.FromHwnd(Handle))
          size = renderer.GetTextExtent(g, text, TextFormatFlags.TextBoxControl).Size;

        owner.OnTooltipMeasure(ref size, renderer);
        
        size = new Size(size.Width + PADDING * 2, size.Height + PADDING * 2);

        pos.Y -= size.Height;
        Rectangle workingArea = Screen.GetWorkingArea(pos);
        if (pos.X < workingArea.X)
          pos.X = workingArea.X;
        if (pos.Y < workingArea.Y)
          pos.Y = workingArea.Y;

        if (pos.X + size.Width > workingArea.Right)
          pos.X = workingArea.Right - size.Width;
        if (pos.Y + size.Height > workingArea.Bottom)
          pos.Y = workingArea.Bottom - size.Height;
      }
    }

    #endregion

    protected override void Dispose(bool disposing)
    {
      if (ownerForm != null)
        RemoveIcon();
      if (hintWindow != null)
      {
        hintWindow.DestroyHandle();
        hintWindow = null;
      }
      ownerForm = null;
      base.Dispose(disposing);
    }

    #region Public methods

    /// <summary>
    /// This method should be called from the owner form's WndProc message.
    /// The simplest way to do this is
    ///<code>protected override void WndProc(ref Message m)
    ///{
    ///  if (myTrayIcon.WndProc(ref m))
    ///     return;
    ///   base.WndProc(ref m);
    ///}</code>
    /// Yes, not the best solution. But it seems to be impossible to handle windows messages somehow different.
    /// </summary>
    /// <param name="msg">Message received</param>
    /// <returns>True if message was handled by TrayIcon (and no further processing required) and false otherwise.</returns>
    public bool WndProc(ref Message msg)
    {
      if (msg.Msg == (int)Msgs.WM_DESTROY)
      {
        Dispose(true);
        return false;
      }

      if (msg.Msg != CALLBACK_MESSAGE)
        return false;

      if (msg.LParam == (IntPtr)WM_TASKBARCREATED)
      {
        WmTaskbarCreated();
        return true;
      }

      switch ((int)msg.LParam & 0xFFFF) // for WinVista and later lParam is restricted to 16 bits
      {
        case (int)Msgs.WM_COMMAND: // 273, commands, ignored
          return true;
        case (int)Msgs.WM_INITMENUPOPUP: // 279, init popup menu, ignored
          return true;
        case (int)Msgs.WM_MOUSEMOVE:
          WmMouseMove(ref msg);
          return true;
        case (int)Msgs.WM_LBUTTONDOWN:
          WmMouseDown(ref msg, MouseButtons.Left, 1);
          return true;
        case (int)Msgs.WM_LBUTTONUP:
          WmMouseUp(ref msg, MouseButtons.Left);
          return true;
        case (int)Msgs.WM_LBUTTONDBLCLK:
          WmMouseDown(ref msg, MouseButtons.Left, 2);
          return true;
        case (int)Msgs.WM_RBUTTONDOWN:
          WmMouseDown(ref msg, MouseButtons.Right, 1);
          return true;
        case (int)Msgs.WM_RBUTTONUP:
            ShowContextMenu();
          WmMouseUp(ref msg, MouseButtons.Right);
          return true;
        case (int)Msgs.WM_RBUTTONDBLCLK:
          WmMouseDown(ref msg, MouseButtons.Right, 2);
          return true;
        case (int)Msgs.WM_MBUTTONDOWN:
          WmMouseDown(ref msg, MouseButtons.Middle, 1);
          return true;
        case (int)Msgs.WM_MBUTTONUP:
          WmMouseUp(ref msg, MouseButtons.Middle);
          return true;
        case (int)Msgs.WM_MBUTTONDBLCLK:
          WmMouseDown(ref msg, MouseButtons.Middle, 2);
          return true;
        case (int)NotifyIconParamMessages.NIN_BALLOONSHOW:
          OnBalloonTipShown();
          return true;
        case (int)NotifyIconParamMessages.NIN_BALLOONHIDE:
          OnBalloonTipClosed();
          return true;
        case (int)NotifyIconParamMessages.NIN_BALLOONTIMEOUT:
          OnBalloonTipClosed();
          return true;
        case (int)NotifyIconParamMessages.NIN_BALLOONUSERCLICK:
          OnBalloonTipClicked();
          return true;
        case (int)NotifyIconParamMessages.NIN_POPUPOPEN:
          OnToolTipShown(((int)msg.WParam) & 0xFFFF, (((int)msg.WParam) >> 16) & 0xFFFF);
          return true;
        case (int)NotifyIconParamMessages.NIN_POPUPCLOSE:
          OnToolTipClosed();
          return true;
      }

      return false;
    }

    /// <summary>
    /// Show balloon notification
    /// </summary>
    /// <param name="caption">Caption of the balloon. Limited to 64 chars</param>
    /// <param name="text">Text of the balloon. Limited to 256 chars</param>
    /// <param name="balloonIcon">The type of an icon to show</param>
    /// <param name="noSound">Set to true to suppress default system sound</param>
    /// <param name="realTime">Set to true if the notification is relevant only now. If Windows cannot show
    /// the notification now - it will be discarded</param>
    /// <exception cref="ArgumentOutOfRangeException">Shown if <see cref="TrimLongText"/> is false and <paramref name="caption"/> or <paramref name="text"/> overflows the limits</exception>
    public void ShowBalloonTip(string caption, string text, NotifyIconIcons balloonIcon, bool noSound = false, bool realTime = false)
    {
      if (ownerForm == null)
        return;

      if (caption.Length >= 64)
      {
        if (trimLongText)
          caption = caption.Substring(0, 63);
        else
          throw new ArgumentOutOfRangeException("caption", caption.Length, "Caption should be less than 64 chars");
      }
      if (text.Length >= 256)
      {
        if (trimLongText)
          text = text.Substring(0, 255);
        else
          throw new ArgumentOutOfRangeException("text", text.Length, "Text should be less than 64 chars");
      }

      if (!useVersion4)
      {
        ShowBalloonTipLegacy(caption, text, balloonIcon, noSound, realTime);
        return;
      }

      NOTIFYICONDATA4 data = new NOTIFYICONDATA4();
      InitNotifyIconData(ref data);
      data.szInfoTitle = caption;
      data.szInfo = text;
      data.uFlags = NotifyIconFlags.Info;
      if (realTime)
        data.uFlags |= NotifyIconFlags.RealTime;
      switch (balloonIcon)
      {
        case NotifyIconIcons.None:
          data.dwInfoFlags = NotifyIconInfoFlags.IconNone;
          break;
        case NotifyIconIcons.Error:
          data.dwInfoFlags = NotifyIconInfoFlags.IconError;
          break;
        case NotifyIconIcons.Info:
          data.dwInfoFlags = NotifyIconInfoFlags.IconInfo;
          break;
        case NotifyIconIcons.Warning:
          data.dwInfoFlags = NotifyIconInfoFlags.IconWarning;
          break;
        case NotifyIconIcons.User:
          data.dwInfoFlags = NotifyIconInfoFlags.IconUser;
          break;
      }

      data.dwInfoFlags |= NotifyIconInfoFlags.RespectQuietTime;
      if (useLargeIcons)
        data.dwInfoFlags |= NotifyIconInfoFlags.LargeIcon;
      if (noSound)
        data.dwInfoFlags |= NotifyIconInfoFlags.NoSound;

      if (!Shell_NotifyIcon(NotifyIconMessages.Modify, data))
        throw new Win32Exception("Shell_NotifyIcon failed");
    }

    /// <summary>
    /// Show balloon notification
    /// </summary>
    /// <param name="caption">Caption of the balloon. Limited to 64 chars</param>
    /// <param name="text">Text of the balloon. Limited to 256 chars</param>
    /// <param name="balloonIcon">Icon to show in balloon. Recommended to be at least 32x32</param>
    /// <param name="backupIcon">Icon type when running on WindowsXP which is unable to use custom icons here</param>
    /// <param name="noSound">Set to true to suppress default system sound</param>
    /// <param name="realTime">Set to true if the notification is relevant only now. If Windows cannot show
    /// the notification now - it will be discarded</param>
    /// <exception cref="ArgumentOutOfRangeException">Shown if <see cref="TrimLongText"/> is false and <paramref name="caption"/> or <paramref name="text"/> overflows the limits</exception>
    public void ShowBalloonTip(string caption, string text, Icon balloonIcon, NotifyIconIcons backupIcon, bool noSound = false, bool realTime = false)
    {
      if (ownerForm == null)
        return;

      if (caption.Length >= 64)
      {
        if (trimLongText)
          caption = caption.Substring(0, 61) + "...";
        else
          throw new ArgumentOutOfRangeException("caption", caption.Length, "Caption should be less than 64 chars");
      }
      if (text.Length >= 256)
      {
        if (trimLongText)
          text = text.Substring(0, 253) + "...";
        else
          throw new ArgumentOutOfRangeException("text", text.Length, "Text should be less than 64 chars");
      }

      if (!useVersion4)
      {
        ShowBalloonTipLegacy(caption, text, backupIcon, noSound, realTime);
        return;
      }

      NOTIFYICONDATA4 data = new NOTIFYICONDATA4();
      InitNotifyIconData(ref data);
      data.szInfoTitle = caption;
      data.szInfo = text;
      data.uFlags = NotifyIconFlags.Info;
      if (realTime)
        data.uFlags |= NotifyIconFlags.RealTime;
      data.dwInfoFlags = NotifyIconInfoFlags.IconUser;
      data.hBalloonIcon = balloonIcon.Handle;
 
      data.dwInfoFlags |= NotifyIconInfoFlags.RespectQuietTime;
      if (useLargeIcons)
        data.dwInfoFlags |= NotifyIconInfoFlags.LargeIcon;
      if (noSound)
        data.dwInfoFlags |= NotifyIconInfoFlags.NoSound;

      if (!Shell_NotifyIcon(NotifyIconMessages.Modify, data))
        throw new Win32Exception("Shell_NotifyIcon failed");
    }
  
    /// <summary>
    /// Gets position of the tray icon on the screen
    /// </summary>
    /// <returns>Position of the tray icon on the screen</returns>
    /// <remarks>Works only on Win7 and later. Will return <see cref="Cursor.Position"/> when running on earlier OS</remarks>
    public Point GetIconPosition()
    {
      if (!WindowsVersionDetector.RunningOnWin7)
        return Cursor.Position;

      NOTIFYICONIDENTIFIER ident = new NOTIFYICONIDENTIFIER();
      ident.cbSize = (uint)Marshal.SizeOf(typeof (NOTIFYICONIDENTIFIER));
      ident.hWnd = ownerForm.Handle;
      ident.uID = (uint)id;
      //ident.guidItem = guid; // don't know why, but setting GUID produces "The parameter is incorrect" HRESULT

      WinAPI.RECT rect = new WinAPI.RECT();
      int result = Shell_NotifyIconGetRect(ref ident, ref rect);
      if (result != 0)
        throw new Win32Exception(result);

      return new Point(rect.left, rect.top);
    }    

    /// <summary>
    /// Sets the focus to taskbar
    /// Recommended to call when context menu canceled
    /// </summary>
    public void SetFocus()
    {
      NOTIFYICONDATA data = new NOTIFYICONDATA();
      InitNotifyIconData(ref data);
      Shell_NotifyIcon(NotifyIconMessages.SetFocus, data);      
    }

    /// <summary>
    /// If the custom hint is visible (<see cref="CustomHintVisible"/> is true) refreshes it, making full cycle of Measure-Paint
    /// </summary>
    public void RefreshCustomHint()
    {
      if (!customHintVisible || !useVersion4 || ownerForm == null)
        return;

      hintWindow.Refresh(customHintPosition);
    }

    #endregion

    private void ShowBalloonTipLegacy(string caption, string text, NotifyIconIcons balloonIcon, bool noSound = false, bool realTime = false)
    {
      NOTIFYICONDATA data = new NOTIFYICONDATA();
      InitNotifyIconData(ref data);
      data.szInfoTitle = caption;
      data.szInfo = text;
      data.uFlags = NotifyIconFlags.Info;
      if (realTime)
        data.uFlags |= NotifyIconFlags.RealTime;
      switch (balloonIcon)
      {
        case NotifyIconIcons.None:
          data.dwInfoFlags = NotifyIconInfoFlags.IconNone;
          break;
        case NotifyIconIcons.Error:
          data.dwInfoFlags = NotifyIconInfoFlags.IconError;
          break;
        case NotifyIconIcons.Info:
          data.dwInfoFlags = NotifyIconInfoFlags.IconInfo;
          break;
        case NotifyIconIcons.Warning:
          data.dwInfoFlags = NotifyIconInfoFlags.IconWarning;
          break;
        case NotifyIconIcons.User:
          data.dwInfoFlags = NotifyIconInfoFlags.IconUser;
          break;
      }

      data.dwInfoFlags |= NotifyIconInfoFlags.RespectQuietTime;
      if (useLargeIcons)
        data.dwInfoFlags |= NotifyIconInfoFlags.LargeIcon;
      if (noSound)
        data.dwInfoFlags |= NotifyIconInfoFlags.NoSound;

      if (!Shell_NotifyIcon(NotifyIconMessages.Modify, data))
        throw new Win32Exception("Shell_NotifyIcon failed");
    }   

    private void InitNotifyIconData(ref NOTIFYICONDATA data)
    {
      data.hWnd = ownerForm.Handle;
      data.uID = id;
      data.guidItem = guid;
    }

    private void InitNotifyIconData(ref NOTIFYICONDATA4 data)
    {
      data.hWnd = ownerForm.Handle;
      data.uID = id;
      data.guidItem = guid;
    }

    private void UpdateIcon()
    {
      if (ownerForm == null)
        return;
      if (DesignMode)
        return;

      if (!useVersion4)
      {
        UpdateIconLegacy();
        return;
      }

      NOTIFYICONDATA4 data = new NOTIFYICONDATA4();
      InitNotifyIconData(ref data);

      if (!iconAdded)
      {
        data.uTimeoutOrVersion = 4;

        if (!Shell_NotifyIcon(NotifyIconMessages.Add, data))
          throw new Win32Exception("Shell_NotifyIcon failed");

        if (!Shell_NotifyIcon(NotifyIconMessages.SetVersion, data))
          throw new Win32Exception("Shell_NotifyIcon failed");
      }

      data.hIcon = icon == null ? IntPtr.Zero : icon.Handle;
      data.szTip = hintText;
      data.uCallbackMessage = CALLBACK_MESSAGE;
      data.uFlags = NotifyIconFlags.Icon | NotifyIconFlags.Message | NotifyIconFlags.Tip;
      if (showDefaultTips)
        data.uFlags |= NotifyIconFlags.ShowTip;

      if (!Shell_NotifyIcon(NotifyIconMessages.Modify, data))
        throw new Win32Exception("Shell_NotifyIcon failed");

      iconAdded = true;
    }

    private void UpdateIconLegacy()
    {
      NOTIFYICONDATA data = new NOTIFYICONDATA();
      InitNotifyIconData(ref data);

      if (!iconAdded)
      {
        if (!Shell_NotifyIcon(NotifyIconMessages.Add, data))
          throw new Win32Exception("Shell_NotifyIcon failed");
      }

      data.hIcon = icon == null ? IntPtr.Zero : icon.Handle;
      data.szTip = hintText;
      data.uCallbackMessage = CALLBACK_MESSAGE;
      data.uFlags = NotifyIconFlags.Icon | NotifyIconFlags.Message | NotifyIconFlags.Tip | NotifyIconFlags.Guid;

      Shell_NotifyIcon(NotifyIconMessages.Modify, data);

      iconAdded = true;
    }

    private void RemoveIcon()
    {
      if (ownerForm == null)
        return;
      if (!iconAdded)
        return;

      if (!useVersion4)
      {
        RemoveIconLegacy();
        return;
      }

      NOTIFYICONDATA4 data = new NOTIFYICONDATA4();
      InitNotifyIconData(ref data);

      if (!Shell_NotifyIcon(NotifyIconMessages.Delete, data))
        throw new Win32Exception("Shell_NotifyIcon failed");
      iconAdded = false;
    }

    private void RemoveIconLegacy()
    {
      if (ownerForm == null)
        return;
      if (!iconAdded)
        return;
      NOTIFYICONDATA data = new NOTIFYICONDATA();
      InitNotifyIconData(ref data);

      if (!Shell_NotifyIcon(NotifyIconMessages.Delete, data))
        throw new Win32Exception("Shell_NotifyIcon failed");
      iconAdded = false;
    }

    private void ShowContextMenu()
    {
      if (contextMenu != null)
      {
        SetForegroundWindow(contextMenu.Handle);
        contextMenu.Show(GetIconPosition());
        return;
      }

      EventHandler eventHandler = (EventHandler)Events[EVENT_CONTEXTMENUSHOW];
      if (eventHandler == null)
        return;
      eventHandler(this, EventArgs.Empty);
    }
  }

#if SHARED_PUBLIC_API
  public
#else
  internal
#endif
  class TooltipShowArgs: EventArgs
  {
    private Point position;

    public TooltipShowArgs(Point position)
    {
      this.position = position;
    }

    public Point Position
    {
      get { return position; }
    }
  }

  internal enum NotifyIconParamMessages
  {
    NIN_SELECT = 0x0400,
    NIN_KEYSELECT = 0x401,
    NIN_BALLOONSHOW = 0x0402,
    NIN_BALLOONHIDE = 0x0403,
    NIN_BALLOONTIMEOUT = 0x0404,
    NIN_BALLOONUSERCLICK = 0x0405,
    NIN_POPUPOPEN = 0x0406,
    NIN_POPUPCLOSE = 0x0407,
  }

#if SHARED_PUBLIC_API
  public
#else
  internal
#endif
  enum NotifyIconIcons
  {
    None,
    Info,
    Warning,
    Error,
    User
  }
}