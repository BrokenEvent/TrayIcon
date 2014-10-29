using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BrokenEvent.Shared
{
  class CustomHint: Component
  {
    private Form ownerForn;
    private HintWindow hintWindow;
    private bool visible;
    private string text;
    private Padding innerPadding = new Padding(2);
    private VisualStyleRenderer renderer;

    public CustomHint()
    {
      if (VisualStyleRenderer.IsElementDefined(VisualStyleElement.ToolTip.Standard.Normal))
        renderer = new VisualStyleRenderer(VisualStyleElement.ToolTip.Standard.Normal);
    }

    private static readonly object EVENT_ONMEASURE = new object();
    private static readonly object EVENT_ONPAINT = new object();

    [Description("Occurs when the hint is being shown and its size has to be measured.")]
    public event EventHandler<HintMeasureEventArgs> OnMeasure
    {
      add { Events.AddHandler(EVENT_ONMEASURE, value); }
      remove { Events.RemoveHandler(EVENT_ONMEASURE, value); }
    }

    [Description("Occurs when the hint is being painted.")]
    public event EventHandler<HintPaintEventArgs> OnPaint
    {
      add { Events.AddHandler(EVENT_ONPAINT, value); }
      remove { Events.RemoveHandler(EVENT_ONPAINT, value); }
    }

    [Description("Owner form")]
    public Form OwnerForn
    {
      get { return ownerForn; }
      set { ownerForn = value; }
    }

    [ReadOnly(true)]
    [Browsable(false)]
    public bool Visible
    {
      get { return visible; }
    }

    /// <summary>
    /// Show hint at given position.
    /// Equal to call <code>Show(pos, HintPosition.BelowRight);</code>
    /// </summary>
    /// <param name="pos">Position to show in screen coordinates</param>
    public void Show(Point pos)
    {
      Show(pos, HintPosition.BelowRight);
    }

    /// <summary>
    /// Show hint with given relation to given position
    /// </summary>
    /// <param name="pos">Position to show in screen coordinates</param>
    /// <param name="hintPosition">Relation to given point</param>
    public void Show(Point pos, HintPosition hintPosition)
    {
      if (ownerForn == null)
        return;

      if (hintWindow == null)
        hintWindow = new HintWindow(IntPtr.Zero, this);

      Size size;
      Measure(ref pos, out size, hintPosition);
      hintWindow.Show(pos.X, pos.Y, size.Width, size.Height);
      visible = true;
    }

    [Description("Text of the hint")]
    public string Text
    {
      get { return text; }
      set { text = value; }
    }

    [Description("Padding between hint shape and the text inside")]
    public Padding InnerPadding
    {
      get { return innerPadding; }
      set { innerPadding = value; }
    }

    /// <summary>
    /// Hide hint
    /// </summary>
    public void Hide()
    {
      if (ownerForn == null || hintWindow == null)
        return;

      hintWindow.Hide();
      visible = false;
    }

    protected virtual void Paint(Graphics g, Size size)
    {
      using (Brush brush = new SolidBrush(Color.Lime))
        g.FillRectangle(brush, 0, 0, size.Width, size.Height);

      EventHandler<HintPaintEventArgs> handler = (EventHandler<HintPaintEventArgs>)Events[EVENT_ONPAINT];
      if (handler != null)
      {
        handler(this, new HintPaintEventArgs(g, size, renderer));
        return;
      }

      if (renderer == null) // fallback for WinXP and others who didn't know this style
      {
        using (Brush brush = new SolidBrush(SystemColors.Info))
          g.FillRectangle(brush, new Rectangle(Point.Empty, size));
        using (Brush brush = new SolidBrush(Color.DarkBlue))
          g.DrawString(
              text,
              SystemFonts.DefaultFont,
              brush,
              new RectangleF(
                  innerPadding.Left,
                  innerPadding.Top,
                  size.Width - innerPadding.Right,
                  size.Height - innerPadding.Bottom
                ),
              StringFormat.GenericDefault
            );
        return;
      }

      renderer.DrawBackground(g, new Rectangle(Point.Empty, size));
      renderer.DrawText(
          g,
          new Rectangle(
              innerPadding.Left,
              innerPadding.Top,
              size.Width - innerPadding.Right,
              size.Height - innerPadding.Bottom
            ),
          text,
          false,
          TextFormatFlags.TextBoxControl
        );
    }

    private static Point MakeHintPosition(Point showPos, Size size, HintPosition hintPosition)
    {
      switch (hintPosition)
      {
        case HintPosition.AboveLeft:
          return new Point(showPos.X - size.Width, showPos.Y - size.Height);
        case HintPosition.AboveRight:
          return new Point(showPos.X, showPos.Y - size.Height);
        case HintPosition.BelowLeft:
          return new Point(showPos.X - size.Width, showPos.Y);
        case HintPosition.BelowRight:
          return showPos;
        default:
          return Point.Empty;
      }
    }

    private void Measure(ref Point showPos, out Size size, HintPosition hintPosition)
    {
      size = Measure();
      size.Width += innerPadding.Left + innerPadding.Right;
      size.Height += innerPadding.Top + innerPadding.Bottom;

      Rectangle workingArea = GetHintOwnerRect(showPos);

      showPos = MakeHintPosition(showPos, size, hintPosition);

      if (showPos.X < workingArea.X)
        showPos.X = workingArea.X;
      if (showPos.Y < workingArea.Y)
        showPos.Y = workingArea.Y;

      if (showPos.X + size.Width > workingArea.Right)
        showPos.X = workingArea.Right - size.Width;
      if (showPos.Y + size.Height > workingArea.Bottom)
        showPos.Y = workingArea.Bottom - size.Height;
    }

    protected virtual Rectangle GetHintOwnerRect(Point targetPos)
    {
      return Screen.GetWorkingArea(targetPos);
    }

    protected virtual Size Measure()
    {
      Size result;
      if (renderer != null)
        using (Graphics g = Graphics.FromHwnd(hintWindow.Handle))
          result = renderer.GetTextExtent(g, text, TextFormatFlags.TextBoxControl).Size;
      else // fallback for WinXP and others who didn't know this style
        result = TextRenderer.MeasureText(text, SystemFonts.DefaultFont, Size.Empty, TextFormatFlags.TextBoxControl);

      EventHandler<HintMeasureEventArgs> handler = (EventHandler<HintMeasureEventArgs>)Events[EVENT_ONMEASURE];
      if (handler != null)
      {
        HintMeasureEventArgs args = new HintMeasureEventArgs(result);
        handler(this, args);
        return args.Size;
      }

      return result;
    }

    protected override void Dispose(bool disposing)
    {
      if (hintWindow != null)
        hintWindow.DestroyHandle();
      hintWindow = null;
      base.Dispose(disposing);
    }

    private class HintWindow: HintNativeWindow
    {
      private readonly CustomHint owner;

      public HintWindow(IntPtr ownerHandle, CustomHint owner) : base(ownerHandle, Color.Lime)
      {
        this.owner = owner;
      }

      protected override void Paint(Graphics g, Size size)
      {
        owner.Paint(g, size);
      }

      protected override void Measure(ref Point pos, out Size size)
      {
        owner.Measure(ref pos, out size, HintPosition.BelowRight);
      }
    }
  }

#if SHARED_PUBLIC_API
  public
#else
  internal
#endif
  enum HintPosition
  {
    AboveRight,
    AboveLeft,
    BelowRight,
    BelowLeft,
  }
}
