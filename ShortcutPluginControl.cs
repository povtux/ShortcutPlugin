using System;
using System.Windows.Forms;
using System.Diagnostics;
using ShortcutPlugin.Properties;

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
            foreach (Launcher launch in Config.Instance.Launchers)
            {
                Console.WriteLine(launch.Name);
                ll = new LinkLabel
                {
                    Text = launch.Name,
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
                rootpane.Controls.Add(ll, 1, row);
                if(launch.VPN)
                {
                    PictureBox pb = new PictureBox
                    {
                        Image = Resources.vpn,
                        Width = 14,
                        Height = 14,
                        SizeMode = PictureBoxSizeMode.Zoom
                    };
                    rootpane.Controls.Add(pb, 0, row);
                }

             
                row ++;
            }
        }
    }
}
