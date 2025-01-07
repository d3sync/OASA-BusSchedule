using System;
using System.Windows.Forms;

namespace BusSchedule
{
    public partial class SettingsForm : Form
    {
        private readonly BusDataFetcher _busDataFetcher;

        public SettingsForm(BusDataFetcher busDataFetcher)
        {
            _busDataFetcher = busDataFetcher;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Settings";
            this.Width = 300;
            this.Height = 250;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowIcon = false;
            this.MinimumSize = new Size(this.Width,this.Height);
            this.MaximumSize = new Size(this.Width,this.Height);

            var lblBusStop = new Label { Text = "Bus Stop:", Left = 10, Top = 20 };
            var txtBusStop = new TextBox { Left = 120, Top = 20, Width = 150 };
            txtBusStop.Text = _busDataFetcher.BusStop;

            var lblStartTime = new Label { Text = "Start Time:", Left = 10, Top = 50 };
            var dtpStartTime = new DateTimePicker { Left = 120, Top = 50, Width = 150, Format = DateTimePickerFormat.Custom, CustomFormat = "HH:mm", ShowUpDown = true };

            var lblEndTime = new Label { Text = "End Time:", Left = 10, Top = 80 };
            var dtpEndTime = new DateTimePicker { Left = 120, Top = 80, Width = 150, Format = DateTimePickerFormat.Custom, CustomFormat = "HH:mm", ShowUpDown = true };

            var chkShowNotification = new CheckBox { Text = "Show Notification", Left = 10, Top = 110 };

            var btnSave = new Button { Text = "Save", Left = 100, Top = 140, Width = 80,Height = 35};
            btnSave.AutoSize = true;
            btnSave.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSave.Font = new System.Drawing.Font("Consolas", 10);
            btnSave.Click += (sender, args) =>
            {
                _busDataFetcher.BusStop = txtBusStop.Text;
                Settings.StartTime = dtpStartTime.Value.TimeOfDay;
                Settings.EndTime = dtpEndTime.Value.TimeOfDay;
                Settings.ShowNotification = chkShowNotification.Checked;
                // Save start time, end time, and show notification settings as needed
                Settings.SaveSettings();
                this.Close();
            };

            
            dtpStartTime.Value = DateTime.Today.Add(Settings.StartTime);
            dtpEndTime.Value = DateTime.Today.Add(Settings.EndTime);
            chkShowNotification.Checked = Settings.ShowNotification;

            this.Controls.Add(lblBusStop);
            this.Controls.Add(txtBusStop);
            this.Controls.Add(lblStartTime);
            this.Controls.Add(dtpStartTime);
            this.Controls.Add(lblEndTime);
            this.Controls.Add(dtpEndTime);
            this.Controls.Add(chkShowNotification);
            this.Controls.Add(btnSave);
        }
    }
}
