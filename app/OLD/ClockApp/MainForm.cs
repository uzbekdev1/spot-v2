using ClockApp.Helpers;
using ClockApp.Services;
using SpotApp.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClockApp
{
    public partial class MainForm : Form
    {
        private readonly Timer _timer;

        private double _timeDifference = 0d;

        private void MoveRightBottom()
        {
            var workingArea = Screen.GetWorkingArea(this);
            var spanPosition = 10;

            Location = new Point(workingArea.Right - Size.Width - spanPosition, workingArea.Bottom - Size.Height - spanPosition);
        }

        private void MoveTextCenter()
        {
            var g = CreateGraphics();
            var startingPoint = (Width / 2) - (g.MeasureString(Text.Trim(), Font).Width / 2);
            var widthOfASpace = g.MeasureString(" ", Font).Width;
            var tmpText = " ";
            var tmpWidth = 0d;

            while ((tmpWidth + widthOfASpace) < startingPoint)
            {
                tmpText += " ";
                tmpWidth += widthOfASpace;
            }

            Text = tmpText + Text.Trim();
        }

        public MainForm()
        {
            InitializeComponent();

            _timer = new Timer()
            {
                Interval = 100,
                Enabled = true,
            };
            _timer.Tick += Timer_Tick; 
        }

        private DateTime ReloadTime()
        {
            var service = new TimeService();
            var serverTime = service.GetTime();
            var clientTime = DateTime.Now;
            var compareTime = DateTime.Compare(serverTime, clientTime);

            if (compareTime < 0)
            {
                _timeDifference = (-1.0) * clientTime.Subtract(serverTime).TotalMilliseconds;
            }
            else
            {
                _timeDifference = serverTime.Subtract(clientTime).TotalMilliseconds;
            }

            return serverTime;
        }

        private void FormatTime(bool force = false)
        {
            var serverTime = force ? ReloadTime() : DateTime.Now.AddMilliseconds(_timeDifference);

            UIHelper.RunForce(this, form =>
            {
                lblServerTime.Text = serverTime.ToString("HH:mm:ss.fff");

                var clientTime = DateTime.Now;

                lblLocaleTime.Text = clientTime.ToString("HH:mm:ss.fff");

                var differenceTime = clientTime.Subtract(serverTime).TotalSeconds;

                if (differenceTime > 0)
                {
                    lblDiffTime.Text = $"+{differenceTime:0.000} сек";
                }
                else
                {
                    lblDiffTime.Text = $"{differenceTime:0.000} сек";
                }
            });
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            FormatTime(_timeDifference == 0d);
        }

#if !DEBUG
        private const int WS_SYSMENU = 0x80000;
#endif

        protected override CreateParams CreateParams
        {
            get
            {

                var cp = base.CreateParams;

#if !DEBUG
                cp.Style &= ~WS_SYSMENU;
#endif
                return cp;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MoveTextCenter();
            MoveRightBottom();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SpotHelper.Close();
        }

        private void btnTimeUpdate_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            btnTimeUpdate.Enabled = false;

            _timeDifference = 0d;

            UIHelper.RunAsync(this, form =>
            {
                while (_timeDifference == 0d)
                {
                    System.Threading.Thread.Sleep(100);

                    Application.DoEvents();
                }
            }, form =>
            {
                Cursor = Cursors.Default;
                btnTimeUpdate.Enabled = true;
            });
        }
    }
}
