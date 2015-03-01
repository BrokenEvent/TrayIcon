using System;
using System.Drawing;
using System.Windows.Forms;
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

#if SHARED_PUBLIC_API
  public
#else
  internal
#endif
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

    public void DrawHintBackground()
    {
      if (renderer == null) // in case of style is currently not supported (themes disabled?)
        using (Brush brush = new SolidBrush(SystemColors.Info))
          graphics.FillRectangle(brush, new Rectangle(Point.Empty, size));
      else
        renderer.DrawBackground(graphics, new Rectangle(Point.Empty, size));
    }

    public void DrawHintText(string text)
    {
      const int PADDING = 4;
      renderer.DrawText(
          graphics,
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
  }
}
