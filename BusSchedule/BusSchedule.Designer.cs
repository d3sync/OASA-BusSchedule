namespace BusSchedule
{
    partial class BusSchedule
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusSchedule));
            timer1 = new System.Windows.Forms.Timer(components);
            rtb = new RichTextBox();
            btn = new Button();
            statusStrip1 = new StatusStrip();
            tsslTimer = new ToolStripStatusLabel();
            ntfy = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            otsmi = new ToolStripMenuItem();
            statusStrip1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // rtb
            // 
            rtb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtb.ContextMenuStrip = contextMenuStrip1;
            rtb.Location = new Point(0, 0);
            rtb.Name = "rtb";
            rtb.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            rtb.Size = new Size(796, 295);
            rtb.TabIndex = 0;
            rtb.Text = "";
            // 
            // btn
            // 
            btn.Dock = DockStyle.Bottom;
            btn.Location = new Point(0, 324);
            btn.Name = "btn";
            btn.Size = new Size(800, 29);
            btn.TabIndex = 2;
            btn.Text = "btn";
            btn.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsslTimer });
            statusStrip1.Location = new Point(0, 302);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsslTimer
            // 
            tsslTimer.Name = "tsslTimer";
            tsslTimer.Size = new Size(0, 16);
            // 
            // ntfy
            // 
            ntfy.Icon = (Icon)resources.GetObject("ntfy.Icon");
            ntfy.Text = "Notification";
            ntfy.Visible = true;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { otsmi });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(131, 28);
            // 
            // otsmi
            // 
            otsmi.Name = "otsmi";
            otsmi.Size = new Size(130, 24);
            otsmi.Text = "Options";
            otsmi.Click += otsmi_Click;
            // 
            // BusSchedule
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 353);
            ContextMenuStrip = contextMenuStrip1;
            Controls.Add(statusStrip1);
            Controls.Add(btn);
            Controls.Add(rtb);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(818, 400);
            MdiChildrenMinimizedAnchorBottom = false;
            MinimumSize = new Size(818, 400);
            Name = "BusSchedule";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BUS Schedule";
            Load += BusSchedule_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private RichTextBox rtb;
        private Button btn;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsslTimer;
        private NotifyIcon ntfy;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem otsmi;
    }
}
