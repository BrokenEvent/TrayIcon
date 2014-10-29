using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BrokenEvent.Shared
{
  abstract class HintNativeWindow : NativeWindow
  {
    #region P/Invoke and structs

    private struct PAINTSTRUCT
    {
      public IntPtr hdc;
      public bool fErase;
      public int rcPaint_left;
      public int rcPaint_top;
      public int rcPaint_right;
      public int rcPaint_bottom;
      public bool fRestore;
      public bool fIncUpdate;
      public int reserved1;
      public int reserved2;
      public int reserved3;
      public int reserved4;
      public int reserved5;
      public int reserved6;
      public int reserved7;
      public int reserved8;
    }

    [DllImport("user32.dll", EntryPoint = "BeginPaint", CharSet = CharSet.Auto)]
    private static extern IntPtr BeginPaint(IntPtr hWnd, [In, Out] ref PAINTSTRUCT lpPaint);

    [DllImport("user32.dll", EntryPoint = "EndPaint", CharSet = CharSet.Auto)]
    private static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);

    #endregion

    private Size size;
    private Color transparencyColor;

    protected HintNativeWindow(IntPtr ownerHandle, Color transparencyColor): this(ownerHandle)
    {
      this.transparencyColor = transparencyColor;
      User32.SetLayeredWindowAttributes(Handle, (uint)transparencyColor.ToArgb(), 0, LayeredWindowCommands.LWA_COLORKEY);
    }

    protected HintNativeWindow(IntPtr ownerHandle)
    {      
      CreateParams cp = new CreateParams();
      cp.Parent = ownerHandle;
      uint style = (uint)(WindowStyles.WS_DISABLED | WindowStyles.WS_POPUP);
      cp.Style = (int)style;
      cp.ClassStyle |= (int)WindowClassStyles.CS_DROPSHADOW;
      cp.ExStyle = (int)(WindowClassExFlags.WS_EX_NOACTIVATE | WindowClassExFlags.WS_EX_NOPARENTNOTIFY | WindowClassExFlags.WS_EX_TOOLWINDOW | WindowClassExFlags.WS_EX_TOPMOST | WindowClassExFlags.WS_EX_TRANSPARENT | WindowClassExFlags.WS_EX_LAYERED);

      CreateHandle(cp);
    }

    public void Show(int x, int y, int width, int height)
    {
      size = new Size(width, height);
      User32.SetWindowPos(
          Handle,
          IntPtr.Zero,
          x,
          y,
          width,
          height,
          SetWindowPosFlags.ShowWindow | SetWindowPosFlags.NoActivate
        );
    }

    public void Show(Point position)
    {      
      Measure(ref position, out size);
      User32.SetWindowPos(
          Handle,
          IntPtr.Zero,
          position.X,
          position.Y,
          size.Width,
          size.Height,
          SetWindowPosFlags.ShowWindow | SetWindowPosFlags.NoActivate
        );
    }

    public void Hide()
    {
      User32.ShowWindow(Handle, ShowWindowCommands.Hide);
    }

    public Color TransparencyColor
    {
      get { return transparencyColor; }
    }

    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case (int)Msgs.WM_PAINT:
          PAINTSTRUCT lpPaint = new PAINTSTRUCT();
          Graphics graphics = Graphics.FromHdcInternal(BeginPaint(Handle, ref lpPaint));

          Paint(graphics, size);

          graphics.Dispose();
          EndPaint(Handle, ref lpPaint);

          break;
      }

      base.WndProc(ref m);
    }

    protected abstract void Paint(Graphics g, Size size);

    protected abstract void Measure(ref Point pos, out Size size);
  }
}
