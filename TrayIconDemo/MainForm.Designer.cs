namespace BrokenEvent.TrayIconDemo
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.showWindowsExplorerIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cbNotificationIcon = new System.Windows.Forms.ComboBox();
      this.tbHintText = new System.Windows.Forms.TextBox();
      this.tbNotificationText = new System.Windows.Forms.TextBox();
      this.btnShow = new System.Windows.Forms.Button();
      this.lblNotificationIcon = new System.Windows.Forms.Label();
      this.lblTrayHint = new System.Windows.Forms.Label();
      this.lblNotificationText = new System.Windows.Forms.Label();
      this.lblIconHint = new System.Windows.Forms.Label();
      this.lblIconHintHint = new System.Windows.Forms.Label();
      this.cbLargeIcons = new System.Windows.Forms.CheckBox();
      this.lblHint = new System.Windows.Forms.Label();
      this.lblCustomHintTest = new System.Windows.Forms.Label();
      this.cbShowDefaultTips = new System.Windows.Forms.CheckBox();
      this.myTrayIcon = new BrokenEvent.Shared.TrayIcon();
      this.customHint = new BrokenEvent.Shared.CustomHint();
      this.contextMenuStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // contextMenuStrip
      // 
      this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showWindowsExplorerIconToolStripMenuItem});
      this.contextMenuStrip.Name = "contextMenuStrip";
      this.contextMenuStrip.Size = new System.Drawing.Size(227, 26);
      // 
      // showWindowsExplorerIconToolStripMenuItem
      // 
      this.showWindowsExplorerIconToolStripMenuItem.Name = "showWindowsExplorerIconToolStripMenuItem";
      this.showWindowsExplorerIconToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
      this.showWindowsExplorerIconToolStripMenuItem.Text = "Show Windows Explorer Icon";
      this.showWindowsExplorerIconToolStripMenuItem.Click += new System.EventHandler(this.showWindowsExplorerIconToolStripMenuItem_Click);
      // 
      // cbNotificationIcon
      // 
      this.cbNotificationIcon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbNotificationIcon.FormattingEnabled = true;
      this.cbNotificationIcon.Location = new System.Drawing.Point(109, 53);
      this.cbNotificationIcon.Name = "cbNotificationIcon";
      this.cbNotificationIcon.Size = new System.Drawing.Size(286, 21);
      this.cbNotificationIcon.TabIndex = 1;
      this.cbNotificationIcon.SelectedValueChanged += new System.EventHandler(this.cbNotificationIcon_SelectedValueChanged);
      // 
      // tbHintText
      // 
      this.tbHintText.Location = new System.Drawing.Point(109, 12);
      this.tbHintText.MaxLength = 64;
      this.tbHintText.Name = "tbHintText";
      this.tbHintText.Size = new System.Drawing.Size(286, 21);
      this.tbHintText.TabIndex = 2;
      this.tbHintText.Text = "Some test text";
      this.tbHintText.TextChanged += new System.EventHandler(this.tbHintText_TextChanged);
      // 
      // tbNotificationText
      // 
      this.tbNotificationText.Location = new System.Drawing.Point(109, 93);
      this.tbNotificationText.MaxLength = 256;
      this.tbNotificationText.Name = "tbNotificationText";
      this.tbNotificationText.Size = new System.Drawing.Size(286, 21);
      this.tbNotificationText.TabIndex = 3;
      this.tbNotificationText.Text = "Hello world";
      // 
      // btnShow
      // 
      this.btnShow.Location = new System.Drawing.Point(281, 120);
      this.btnShow.Name = "btnShow";
      this.btnShow.Size = new System.Drawing.Size(114, 23);
      this.btnShow.TabIndex = 4;
      this.btnShow.Text = "Show notification";
      this.btnShow.UseVisualStyleBackColor = true;
      this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
      // 
      // lblNotificationIcon
      // 
      this.lblNotificationIcon.AutoSize = true;
      this.lblNotificationIcon.Location = new System.Drawing.Point(12, 56);
      this.lblNotificationIcon.Name = "lblNotificationIcon";
      this.lblNotificationIcon.Size = new System.Drawing.Size(87, 13);
      this.lblNotificationIcon.TabIndex = 5;
      this.lblNotificationIcon.Text = "Notification icon:";
      // 
      // lblTrayHint
      // 
      this.lblTrayHint.AutoSize = true;
      this.lblTrayHint.Location = new System.Drawing.Point(12, 15);
      this.lblTrayHint.Name = "lblTrayHint";
      this.lblTrayHint.Size = new System.Drawing.Size(76, 13);
      this.lblTrayHint.TabIndex = 6;
      this.lblTrayHint.Text = "Tray icon hint:";
      // 
      // lblNotificationText
      // 
      this.lblNotificationText.AutoSize = true;
      this.lblNotificationText.Location = new System.Drawing.Point(12, 96);
      this.lblNotificationText.Name = "lblNotificationText";
      this.lblNotificationText.Size = new System.Drawing.Size(88, 13);
      this.lblNotificationText.TabIndex = 7;
      this.lblNotificationText.Text = "Notification text:";
      // 
      // lblIconHint
      // 
      this.lblIconHint.AutoSize = true;
      this.lblIconHint.ForeColor = System.Drawing.Color.Navy;
      this.lblIconHint.Location = new System.Drawing.Point(106, 77);
      this.lblIconHint.Name = "lblIconHint";
      this.lblIconHint.Size = new System.Drawing.Size(35, 13);
      this.lblIconHint.TabIndex = 8;
      this.lblIconHint.Text = "label1";
      // 
      // lblIconHintHint
      // 
      this.lblIconHintHint.AutoSize = true;
      this.lblIconHintHint.ForeColor = System.Drawing.Color.Navy;
      this.lblIconHintHint.Location = new System.Drawing.Point(106, 36);
      this.lblIconHintHint.Name = "lblIconHintHint";
      this.lblIconHintHint.Size = new System.Drawing.Size(271, 13);
      this.lblIconHintHint.TabIndex = 9;
      this.lblIconHintHint.Text = "When enter, hover tray icon my mouse to see changes";
      // 
      // cbLargeIcons
      // 
      this.cbLargeIcons.AutoSize = true;
      this.cbLargeIcons.Checked = true;
      this.cbLargeIcons.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbLargeIcons.Location = new System.Drawing.Point(15, 124);
      this.cbLargeIcons.Name = "cbLargeIcons";
      this.cbLargeIcons.Size = new System.Drawing.Size(185, 17);
      this.cbLargeIcons.TabIndex = 10;
      this.cbLargeIcons.Text = "Show large system icons (Vista+)";
      this.cbLargeIcons.UseVisualStyleBackColor = true;
      this.cbLargeIcons.CheckedChanged += new System.EventHandler(this.cbLargeIcons_CheckedChanged);
      // 
      // lblHint
      // 
      this.lblHint.AutoSize = true;
      this.lblHint.ForeColor = System.Drawing.Color.Navy;
      this.lblHint.Location = new System.Drawing.Point(12, 169);
      this.lblHint.Name = "lblHint";
      this.lblHint.Size = new System.Drawing.Size(175, 13);
      this.lblHint.TabIndex = 11;
      this.lblHint.Text = "Also check tray icon context menu.";
      // 
      // lblCustomHintTest
      // 
      this.lblCustomHintTest.AutoSize = true;
      this.lblCustomHintTest.ForeColor = System.Drawing.Color.Blue;
      this.lblCustomHintTest.Location = new System.Drawing.Point(213, 169);
      this.lblCustomHintTest.Name = "lblCustomHintTest";
      this.lblCustomHintTest.Size = new System.Drawing.Size(186, 13);
      this.lblCustomHintTest.TabIndex = 12;
      this.lblCustomHintTest.Text = "Also try to hover this label by mouse.";
      this.lblCustomHintTest.MouseEnter += new System.EventHandler(this.lblCustomHintTest_MouseEnter);
      this.lblCustomHintTest.MouseLeave += new System.EventHandler(this.lblCustomHintTest_MouseLeave);
      // 
      // cbShowDefaultTips
      // 
      this.cbShowDefaultTips.AutoSize = true;
      this.cbShowDefaultTips.Location = new System.Drawing.Point(15, 147);
      this.cbShowDefaultTips.Name = "cbShowDefaultTips";
      this.cbShowDefaultTips.Size = new System.Drawing.Size(177, 17);
      this.cbShowDefaultTips.TabIndex = 13;
      this.cbShowDefaultTips.Text = "Show default tips (WinXP-style)";
      this.cbShowDefaultTips.UseVisualStyleBackColor = true;
      this.cbShowDefaultTips.CheckedChanged += new System.EventHandler(this.cbShowDefaultTips_CheckedChanged);
      // 
      // myTrayIcon
      // 
      this.myTrayIcon.ContextMenu = this.contextMenuStrip;
      this.myTrayIcon.Enabled = true;
      this.myTrayIcon.Guid = new System.Guid("3883dabe-3dfc-4aec-8d32-c851fb52725a");
      this.myTrayIcon.HintText = "Some test text";
      this.myTrayIcon.Icon = null;
      this.myTrayIcon.LongHintText = "BrokenEvent.TrayIconDemo";
      this.myTrayIcon.OwnerForm = this;
      this.myTrayIcon.ShowDefaultTips = false;
      this.myTrayIcon.ShowOwnerUI = false;
      this.myTrayIcon.TrimLongText = true;
      this.myTrayIcon.TooltipMeasure += new System.EventHandler<BrokenEvent.Shared.HintMeasureEventArgs>(this.customHint_OnMeasure);
      this.myTrayIcon.TooltipPaint += new System.EventHandler<BrokenEvent.Shared.HintPaintEventArgs>(this.myTrayIcon_TooltipPaint);
      // 
      // customHint
      // 
      this.customHint.InnerPadding = new System.Windows.Forms.Padding(4);
      this.customHint.OwnerForn = this;
      this.customHint.Text = "Here is a some tooltip with fully custom render";
      this.customHint.OnMeasure += new System.EventHandler<BrokenEvent.Shared.HintMeasureEventArgs>(this.customHint_OnMeasure);
      this.customHint.OnPaint += new System.EventHandler<BrokenEvent.Shared.HintPaintEventArgs>(this.customHint_OnPaint);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(407, 191);
      this.Controls.Add(this.cbShowDefaultTips);
      this.Controls.Add(this.lblCustomHintTest);
      this.Controls.Add(this.lblTrayHint);
      this.Controls.Add(this.lblHint);
      this.Controls.Add(this.lblIconHintHint);
      this.Controls.Add(this.cbLargeIcons);
      this.Controls.Add(this.lblIconHint);
      this.Controls.Add(this.lblNotificationIcon);
      this.Controls.Add(this.cbNotificationIcon);
      this.Controls.Add(this.lblNotificationText);
      this.Controls.Add(this.tbNotificationText);
      this.Controls.Add(this.tbHintText);
      this.Controls.Add(this.btnShow);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.Text = "BrokenEvent.TrayIconDemo";
      this.contextMenuStrip.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Shared.TrayIcon myTrayIcon;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem showWindowsExplorerIconToolStripMenuItem;
    private System.Windows.Forms.Label lblNotificationText;
    private System.Windows.Forms.Label lblTrayHint;
    private System.Windows.Forms.Label lblNotificationIcon;
    private System.Windows.Forms.Button btnShow;
    private System.Windows.Forms.TextBox tbNotificationText;
    private System.Windows.Forms.TextBox tbHintText;
    private System.Windows.Forms.ComboBox cbNotificationIcon;
    private System.Windows.Forms.Label lblIconHintHint;
    private System.Windows.Forms.Label lblIconHint;
    private System.Windows.Forms.CheckBox cbLargeIcons;
    private System.Windows.Forms.Label lblHint;
    private System.Windows.Forms.Label lblCustomHintTest;
    private Shared.CustomHint customHint;
    private System.Windows.Forms.CheckBox cbShowDefaultTips;
  }
}

