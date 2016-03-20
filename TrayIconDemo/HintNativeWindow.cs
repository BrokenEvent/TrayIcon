using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BrokenEvent.Shared
{
  abstract class HintNativeWindow : NativeWindow
  {
    #region P/Invoke and structs

    // ReSharper disable UnusedField.Compiler
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
    // ReSharper restore UnusedField.Compiler

    [DllImport("user32.dll", EntryPoint = "BeginPaint", CharSet = CharSet.Auto)]
    private static extern IntPtr BeginPaint(IntPtr hWnd, [In, Out] ref PAINTSTRUCT lpPaint);

    [DllImport("user32.dll", EntryPoint = "EndPaint", CharSet = CharSet.Auto)]
    private static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);

    #endregion

    private Size size;
    private Color transparencyColor;

    protected HintNativeWindow(IntPtr ownerHandle, Color transparencyColor)
      : this(ownerHandle, WindowClassExFlags.WS_EX_TRANSPARENT | WindowClassExFlags.WS_EX_LAYERED)
    {
      this.transparencyColor = transparencyColor;
      User32.SetLayeredWindowAttributes(Handle, (uint)transparencyColor.ToArgb(), 0, LayeredWindowCommands.LWA_COLORKEY);
    }

    protected HintNativeWindow(IntPtr ownerHandle, WindowClassExFlags classFlags)
      : this(ownerHandle, classFlags | WindowClassExFlags.WS_EX_NOACTIVATE | WindowClassExFlags.WS_EX_NOPARENTNOTIFY | WindowClassExFlags.WS_EX_TOOLWINDOW | WindowClassExFlags.WS_EX_TOPMOST, WindowClassStyles.CS_DROPSHADOW, WindowStyles.WS_POPUP | WindowStyles.WS_CHILD)
    {
    }

    protected HintNativeWindow(IntPtr ownerHandle, WindowClassExFlags classFlags, WindowClassStyles classStyles, WindowStyles styles)
    {
      CreateParams cp = new CreateParams();
      cp.Parent = ownerHandle;
      cp.Style = (int)(WindowStyles.WS_DISABLED | styles);
      cp.ClassStyle |= (int)classStyles;
      cp.ExStyle = (int)classFlags;

      CreateHandle(cp);
    }

    protected HintNativeWindow(IntPtr ownerHandle): this(ownerHandle, 0)
    {
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

    public void Refresh(Point position)
    {
      Measure(ref position, out size);
      User32.SetWindowPos(
          Handle,
          IntPtr.Zero,
          position.X,
          position.Y,
          size.Width,
          size.Height,
          SetWindowPosFlags.NoActivate
        );
      Redraw();
    }

    public void Redraw()
    {
      User32.RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.RDW_INVALIDATE);
    }

    public void Hide()
    {
      User32.ShowWindow(Handle, ShowWindowCommands.Hide);
    }

    public Color TransparencyColor
    {
      get { return transparencyColor; }
    }

    protected virtual bool ShouldPaint
    {
      get { return true; }
    }

    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case (int)Msgs.WM_PAINT:
          if (!ShouldPaint)
            break;

          PAINTSTRUCT lpPaint = new PAINTSTRUCT();
          using (Graphics graphics = Graphics.FromHdcInternal(BeginPaint(Handle, ref lpPaint)))
            Paint(graphics, size);

          EndPaint(Handle, ref lpPaint);

          break;
      }

      base.WndProc(ref m);
    }

    protected abstract void Paint(Graphics g, Size size);

    protected abstract void Measure(ref Point pos, out Size size);
  }
}
