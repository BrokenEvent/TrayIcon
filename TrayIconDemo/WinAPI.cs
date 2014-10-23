using System.Drawing;
using System.Runtime.InteropServices;

namespace BrokenEvent.Shared
{
  class WinAPI
  {
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
      public int left;
      public int top;
      public int right;
      public int bottom;

      public RECT(int left, int top, int right, int bottom)
      {
        this.left = left;
        this.top = top;
        this.right = right;
        this.bottom = bottom;
      }

      public RECT(Rectangle rect)
      {
        left = rect.Left;
        top = rect.Top;
        right = rect.Right;
        bottom = rect.Bottom;
      }

      public Rectangle ToRectangle()
      {
        return new Rectangle(left, top, right - left, bottom - top);
      }
    }
  }
}
