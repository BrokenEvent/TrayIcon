using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace BrokenEvent.Shared
{
  class HintMeasureEventArgs : EventArgs
  {
    private Size size;

    public HintMeasureEventArgs(Size size)
    {
      this.size = size;
    }

    public Size Size
    {
      get { return size; }
      set { size = value; }
    }
  }

  class HintPaintEventArgs : EventArgs
  {
    private readonly Size size;
    private readonly Graphics graphics;
    private readonly VisualStyleRenderer renderer;

    public HintPaintEventArgs(Graphics graphics, Size size, VisualStyleRenderer renderer)
    {
      this.size = size;
      this.renderer = renderer;
      this.graphics = graphics;
    }

    public Size Size
    {
      get { return size; }
    }

    public Graphics Graphics
    {
      get { return graphics; }
    }

    public VisualStyleRenderer Renderer
    {
      get { return renderer; }
    }
  }
}
