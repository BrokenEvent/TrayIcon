using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BrokenEvent.Shared;

namespace BrokenEvent.TrayIconDemo
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();

      // a simple way to get application icon. Well, buggy, it doesn't work when app is running from share
      Icon = myTrayIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

      myTrayIcon.OwnerForm = this;
      customHint.OwnerForm = this;

      Array enumValues = Enum.GetValues(typeof (NotifyIconIcons));
      foreach (object enumValue in enumValues)
        cbNotificationIcon.Items.Add(enumValue);

      cbNotificationIcon.SelectedItem = NotifyIconIcons.User;
    }

    private void showWindowsExplorerIconToolStripMenuItem_Click(object sender, EventArgs e)
    {
      // a little trick - get icon from Exe file and use it for notification
      Icon icon = Icon.ExtractAssociatedIcon(Path.Combine(Environment.GetEnvironmentVariable("windir"), "Explorer.exe"));

      myTrayIcon.ShowBalloonTip("BrokenEvent.TrayIconDemo", "Notification with some external icon", icon, NotifyIconIcons.User);
    }

    protected override void WndProc(ref Message m)
    {
      if (myTrayIcon.WndProc(ref m))
        return;

      base.WndProc(ref m);
    }

    private void tbHintText_TextChanged(object sender, EventArgs e)
    {
      myTrayIcon.HintText = tbHintText.Text;
    }

    private void cbNotificationIcon_SelectedValueChanged(object sender, EventArgs e)
    {
      switch ((NotifyIconIcons)cbNotificationIcon.SelectedItem)
      {
        case NotifyIconIcons.Info:
          lblIconHint.Text = "System information icon";
          break;
        case NotifyIconIcons.Warning:
          lblIconHint.Text = "System warning icon";
          break;
        case NotifyIconIcons.Error:
          lblIconHint.Text = "System error icon";
          break;
        case NotifyIconIcons.None:
          lblIconHint.Text = "No icon";
          break;
        case NotifyIconIcons.User:
          lblIconHint.Text = "The same icon as in tray";
          break;
      }
    }

    private void btnShow_Click(object sender, EventArgs e)
    {
      myTrayIcon.ShowBalloonTip("BrokenEvent.TrayIconDemo", tbNotificationText.Text, (NotifyIconIcons)cbNotificationIcon.SelectedItem);
    }

    private void cbLargeIcons_CheckedChanged(object sender, EventArgs e)
    {
      myTrayIcon.UseLargeIcons = cbLargeIcons.Checked;
    }

    private void lblCustomHintTest_MouseLeave(object sender, EventArgs e)
    {
      customHint.Hide();
    }

    private void lblCustomHintTest_MouseEnter(object sender, EventArgs e)
    {
      customHint.Show(PointToScreen(new Point(lblCustomHintTest.Left, lblCustomHintTest.Top + lblCustomHintTest.Height)));
    }

    private void customHint_OnMeasure(object sender, HintMeasureEventArgs e)
    {
      e.Size = new Size(e.Size.Width + 32 + customHint.InnerPadding.Left, 32);
    }

    private void customHint_OnPaint(object sender, HintPaintEventArgs e)
    {
      DrawHintWithPicture(e, customHint.Text, "Rendered from form");
    }

    private void DrawHintWithPicture(HintPaintEventArgs e, string text, string subText)
    {
      if (e.Renderer != null)
        e.Renderer.DrawBackground(e.Graphics, new Rectangle(Point.Empty, e.Size));
      else // fallback for old windows versions
        using (Brush brush = new SolidBrush(SystemColors.Info))
          e.Graphics.FillRectangle(brush, new Rectangle(Point.Empty, e.Size));

      e.Graphics.DrawIcon(Icon, new Rectangle(customHint.InnerPadding.Left, customHint.InnerPadding.Top, 32, 32));

      if (e.Renderer != null)
      {
        int height = e.Renderer.GetTextExtent(e.Graphics, subText, TextFormatFlags.TextBoxControl).Height;

        e.Renderer.DrawText(
            e.Graphics,
            new Rectangle(
                  customHint.InnerPadding.Left * 2 + 32,
                  customHint.InnerPadding.Top,
                  e.Size.Width - customHint.InnerPadding.Right,
                  e.Size.Height - customHint.InnerPadding.Bottom
                ),
                text,
                false,
                TextFormatFlags.TextBoxControl
          );
        e.Renderer.DrawText(
            e.Graphics,
            new Rectangle(
                  customHint.InnerPadding.Left * 2 + 32,
                  customHint.InnerPadding.Top + height,
                  e.Size.Width - customHint.InnerPadding.Right,
                  e.Size.Height - customHint.InnerPadding.Bottom - height
                ),
                subText,
                true,
                TextFormatFlags.TextBoxControl
          );
      }
      else // fallback for old windows versions
      {
        int height = (int)e.Graphics.MeasureString(subText, SystemFonts.DefaultFont).Height;
        using (Brush brush = new SolidBrush(Color.DarkBlue))
          e.Graphics.DrawString(
              text,
              SystemFonts.DefaultFont,
              brush,
              new RectangleF(
                  customHint.InnerPadding.Left * 2 + 32,
                  customHint.InnerPadding.Top,
                  e.Size.Width - customHint.InnerPadding.Right,
                  e.Size.Height - customHint.InnerPadding.Bottom
                ),
              StringFormat.GenericDefault
            );

        using (Brush brush = new SolidBrush(Color.Gray))
          e.Graphics.DrawString(
              subText,
              SystemFonts.DefaultFont,
              brush,
              new RectangleF(
                  customHint.InnerPadding.Left * 2 + 32,
                  customHint.InnerPadding.Top + height,
                  e.Size.Width - customHint.InnerPadding.Right,
                  e.Size.Height - customHint.InnerPadding.Bottom - height
                ),
              StringFormat.GenericDefault
            );        
      }
    }

    private void myTrayIcon_TooltipPaint(object sender, HintPaintEventArgs e)
    {
      DrawHintWithPicture(e, myTrayIcon.LongHintText, myTrayIcon.HintText);
    }

    private void cbShowDefaultTips_CheckedChanged(object sender, EventArgs e)
    {
      myTrayIcon.ShowDefaultTips = cbShowDefaultTips.Checked;
    }
  }
}
