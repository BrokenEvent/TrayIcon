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

      Icon = myTrayIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

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
  }
}
