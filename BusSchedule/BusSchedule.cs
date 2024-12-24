using System.Text;

namespace BusSchedule
{
    public partial class BusSchedule : Form
    {
        private readonly BusDataFetcher _busDataFetcher = new BusDataFetcher(null);
        public BusSchedule()
        {
            InitializeComponent();
            //timer1.Interval = 1000 * 30;
            timer1.Enabled = true;
            this.Text = $"Bus Schedule Viewer [{_busDataFetcher.BusStop}]";
            this.Width = 800;
            this.Height = 600;

            //rtb.Dock = DockStyle.Fill;
            rtb.Font = new System.Drawing.Font("Consolas", 10);

            btn.Text = "Fetch Arrivals";
            //btn.Dock = DockStyle.Top;
            btn.Click += async (sender, args) =>
            {
                rtb.Clear();
                var arrivals = await _busDataFetcher.GetArrivalsAsync();
                foreach (var arrival in arrivals)
                {
                    await _busDataFetcher.FetchRoutesForStopAsync(arrival.route_code);
                }

                DisplayColoredArrivals(rtb, arrivals);
                if (Settings.ShowNotification && (DateTime.Now.TimeOfDay >= Settings.StartTime && DateTime.Now.TimeOfDay <= Settings.EndTime))
                {
                    var critical = arrivals.Where(x => int.Parse(x.btime2) <= 8).ToList();
                    if (critical.Count > 0)
                    {
                        var textBus = new StringBuilder();
                        foreach (var bus in critical)
                        {
                            textBus.AppendLine(
                                $"{bus.btime2} minutes for {_busDataFetcher._storedRoutes[bus.route_code].LineID + " " + _busDataFetcher._storedRoutes[bus.route_code].LineDescr}");
                        }

                        ntfy.BalloonTipIcon = ToolTipIcon.Info;
                        ntfy.BalloonTipTitle = "Next Bus Arrival";
                        ntfy.BalloonTipText = textBus.ToString();
                        ntfy.ShowBalloonTip(2000);

                    }
                }
            };
        }

        private int _countdown = 30;

        private void timer1_Tick(object sender, EventArgs e)
        {
            _countdown--;
            tsslTimer.Text = $"Next update in: {_countdown} seconds";
            if (_countdown <= 0)
            {
                btn.PerformClick();
                _countdown = 30;
            }
        }

        private void BusSchedule_Load(object sender, EventArgs e)
        {
            btn.PerformClick();
            rtb.ReadOnly = true;
            Settings.LoadSettings();
        }
        public void DisplayColoredArrivals(RichTextBox richTextBox, List<BusArrival> arrivals)
        {
            richTextBox.BackColor = Color.DimGray;
            richTextBox.Clear();
            richTextBox.SelectionFont = new System.Drawing.Font("Consolas", 10, FontStyle.Bold);
            richTextBox.SelectionColor = System.Drawing.Color.Cyan;
            richTextBox.AppendText("Line\t" + "Destination".PadRight(55) + "\tTime\tBus Code\n");

            foreach (var arrival in arrivals)
            {
                if (_busDataFetcher._storedRoutes.TryGetValue(arrival.route_code, out var route))
                {
                    string colorCode = arrival.btime2 switch
                    {
                        var time when int.TryParse(time, out int minutes) && minutes <= 5 => "Red",
                        var time when int.TryParse(time, out int minutes) && minutes <= 15 => "Yellow",
                        _ => "Green"
                    };

                    // Set text color based on btime2
                    richTextBox.SelectionColor = colorCode switch
                    {
                        "Red" => System.Drawing.Color.Red,
                        "Yellow" => System.Drawing.Color.Orange,
                        "Green" => System.Drawing.Color.LawnGreen,
                        _ => System.Drawing.Color.Black
                    };

                    richTextBox.AppendText($"{route.LineID}\t{route.LineDescr.PadRight(55)}\t{arrival.btime2}\t{arrival.veh_code}\n");
                }
            }
        }

        private void otsmi_Click(object sender, EventArgs e)
        {
            // open a settings form
            var settingsForm = new SettingsForm(_busDataFetcher);
            settingsForm.ShowDialog();
        }
    }
}
