using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace BrokenEvent.Shared
{
  class HintMeasureEventArgs : EventArgs
  {
    private Size size;
    private readonly VisualStyleRenderer renderer;

    public HintMeasureEventArgs(Size size, VisualStyleRenderer renderer)
    {
      this.size = size;
      this.renderer = renderer;
    }

    public VisualStyleRenderer Renderer
    {
      get { return renderer; }
    }

    public Size Size
    {
      get { return size; }
      set { size = value; }
    }
  }
}
