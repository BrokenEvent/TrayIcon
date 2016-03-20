using System;
using System.Drawing;

namespace BrokenEvent.Shared
{
 class TooltipShowArgs : EventArgs
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
}
