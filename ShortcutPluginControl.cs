using System;
using System.Windows.Forms;
using System.Diagnostics;
using ShortcutPlugin.Properties;
using System.Drawing;
using System.IO;

namespace ShortcutPlugin
{
    public partial class ShortcutPluginControl: UserControl
    {
        public ShortcutPluginControl()
        {
            InitializeComponent();
            Downloader dl = new Downloader();
            dl.LoadRessources();

            rootpane.AutoScroll = true;
            rootpane.RowCount = Config.Instance.Launchers.Count;
         
            int row = 0;
            LinkLabel ll;
            string url;
            foreach (Launcher launch in Config.Instance.Launchers)
            {
                Console.WriteLine(launch.Name);
                ll = new LinkLabel
                {
                    Text = launch.Name,
                    TextAlign = ContentAlignment.BottomLeft
                };
                ll.LinkClicked += (sender, args) => {
                    if(launch.Type == Launcher.TYPE_APPLICATION)
                        Process.Start(launch.Subject);
                    else
                    {
                        // type URL
                        if(launch.MsBrowser)
                        {
                            if(Environment.OSVersion.Version.Major>6 || (
                            Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor>=2)
                            )
                                Process.Start("microsoft-edge:" + launch.Subject);
                            else
                                Process.Start("iexplore.exe", launch.Subject);
                        }
                        else
                            Process.Start(launch.Subject);
                    }
                };
                rootpane.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                rootpane.Controls.Add(ll, 2, row);
                if(launch.VPN)
                {
                    PictureBox pb = new PictureBox
                    {
                        Image = Resources.vpn,
                        Width = 24,
                        Height = 24,
                        SizeMode = PictureBoxSizeMode.Zoom
                    };
                    rootpane.Controls.Add(pb, 0, row);
                }
                if (launch.Type == Launcher.TYPE_URL)
                {
                    var parts = launch.Subject.Split('/');
                    if (parts.Length >= 3)
                    {
                        url = parts[0] + "//" + parts[2] + "/favicon.ico";
                        Console.WriteLine(url);
                        dl.DownloadFile(url, launch.Name + "_fav.ico");
                        if (File.Exists(Path.Combine(dl.GetDestFolder(), launch.Name + "_fav.ico"))) {
                            PictureBox pb2 = new PictureBox
                            {
                                Image = new Icon(Path.Combine(dl.GetDestFolder(), launch.Name + "_fav.ico"), 24, 24).ToBitmap(),
                                Width = 24,
                                Height = 24,
                                SizeMode = PictureBoxSizeMode.Zoom
                            };
                            rootpane.Controls.Add(pb2, 1, row);
                        }
                    }
                }
                row++;
            }
        }
    }
}
