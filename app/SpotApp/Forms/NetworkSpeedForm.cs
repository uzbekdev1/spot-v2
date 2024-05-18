using Newtonsoft.Json.Serialization;
using SpotApp.Helpers;
using SpotApp.Services;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace SpotApp.Forms
{
    public partial class NetworkSpeedForm : Form
    {
        private const int timerUpdate = 1000;

        private NetworkInterface[] nicArr;

        private Timer timer;
        private long TotalBytesReceived = 0;
        private long TotalBytesSent = 0;
        private long MaxSpeedDownload = 0;
        private long MaxSpeedUpload = 0;

        private NetworkInterface SelectedNetworkInterface = null;

        public NetworkSpeedForm()
        {
            InitializeComponent();
        }

        private void NetworkSpeed_Load(object sender, EventArgs e)
        {
            InitializeTimer();
        }

        private string ConvertByteSpeed(long bytes, string suffix, int unit = 1000)
        {
            if (bytes < unit)
                return $"{bytes} B";

            var exp = (int)(Math.Log(bytes) / Math.Log(unit));
            return $"{bytes / Math.Pow(unit, exp):F1} {("KMGTP")[exp - 1]}{suffix}";
        }

        private void InitializeTimer()
        {
            timer = new Timer
            {
                Interval = timerUpdate
            };
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void InitializeNetworkInterface()
        {
            UIHelper.RunForce(this, form =>
            {
                nicArr = NetworkInterface.GetAllNetworkInterfaces();
                var goodAdapters = new List<string>();

                foreach (NetworkInterface nicnac in nicArr)
                {
                    if (nicnac.SupportsMulticast && nicnac.GetIPv4Statistics().UnicastPacketsReceived >= 1 && nicnac.OperationalStatus.ToString() == "Up")
                    {
                        string nicName = nicnac.Name + " @ " + ConvertByteSpeed(nicnac.Speed, "bps");
                        goodAdapters.Add(nicName);
                    }
                }

                if (goodAdapters.Count != cmbInterface.Items.Count && goodAdapters.Count != 0)
                {
                    UIHelper.SafeInvokeForm(this, form1 =>
                    {
                        cmbInterface.Items.Clear();
                        foreach (string gadpt in goodAdapters)
                        {
                            cmbInterface.Items.Add(gadpt);
                        }
                        cmbInterface.SelectedIndex = 0;
                    });
                }

                if (goodAdapters.Count == 0)
                {
                    UIHelper.SafeInvokeForm(this, form1 =>
                    {
                        cmbInterface.Items.Clear();
                    });
                }
            });
        }

        private void UpdateNetworkInterface()
        {
            if (cmbInterface.Items.Count >= 1)
            {
                UIHelper.SafeInvokeForm(this, form =>
                    {
                        IPv4InterfaceStatistics interfaceStats = SelectedNetworkInterface.GetIPv4Statistics();

                        long speedDownload = interfaceStats.BytesReceived - TotalBytesReceived;
                        TotalBytesReceived = interfaceStats.BytesReceived;
                        if (speedDownload > MaxSpeedDownload) MaxSpeedDownload = speedDownload;

                        long speedUpload = interfaceStats.BytesSent - TotalBytesSent;
                        TotalBytesSent = interfaceStats.BytesSent;
                        if (speedUpload > MaxSpeedUpload) MaxSpeedUpload = speedUpload;

                        string localIP = "127.0.0.1";
                        UnicastIPAddressInformationCollection ipInfo = SelectedNetworkInterface.GetIPProperties().UnicastAddresses;

                        foreach (UnicastIPAddressInformation item in ipInfo)
                        {
                            if (item.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                localIP = item.Address.ToString();
                                break;
                            }
                        }

                        UIHelper.SafeInvokeForm(this, form1 =>
                        {
                            lblCurrentDownload.Text = ConvertByteSpeed(speedDownload, "Bps", 1024);
                            lblMaxDownload.Text = ConvertByteSpeed(MaxSpeedDownload, "Bps", 1024);
                            lblTotalDownload.Text = ConvertByteSpeed(interfaceStats.BytesReceived, "Bps", 1024);

                            lblCurrentUpload.Text = ConvertByteSpeed(speedUpload, "Bps", 1024);
                            lblMaxUpload.Text = ConvertByteSpeed(MaxSpeedUpload, "Bps", 1024);
                            lblTotalUpload.Text = ConvertByteSpeed(interfaceStats.BytesSent, "Bps", 1024);
                            labelIPAddress.Text = localIP;
                        });
                    });
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            InitializeNetworkInterface();
            UpdateNetworkInterface();
        }

        private void cmbInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (NetworkInterface n in nicArr)
            {
                string adapterName = cmbInterface.SelectedItem.ToString().Split('@')[0].Trim();
                if (n.Name == adapterName)
                {
                    SelectedNetworkInterface = n;
                    break;
                }
            }
            IPv4InterfaceStatistics interfaceStats = SelectedNetworkInterface.GetIPv4Statistics();
            TotalBytesReceived = interfaceStats.BytesReceived;
            TotalBytesSent = interfaceStats.BytesSent;
        }
    }
}
