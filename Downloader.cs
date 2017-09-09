using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Net;
using System.ComponentModel;
using static System.Environment;
using System.IO;

namespace ShortcutPlugin
{
    class Downloader
    {
        private Boolean initOk = false;
        private String destFolder;

        public Downloader()
        {
            destFolder = Path.Combine(Environment.GetFolderPath(SpecialFolder.CommonApplicationData), "quicklauncher");
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
        }

        public void LoadRessources()
        {
            // Get XML Url from registry
            String url;
            url = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\QuickLauncher\\Plugins\\ShortcutPlugin", "URL", "");
            if(url == null || url == "")
                url = (String)Registry.GetValue("HKEY_LOCAL_MACHINE\\Software\\QuickLauncher\\Plugins\\ShortcutPlugin", "URL", "");
            if (url == null || url == "")
                throw new Exception("No URL configured");

            Console.WriteLine(url);

            DownloadFile(url, "conf.xml");
            String destFile = Path.Combine(destFolder, "conf.xml");
            // traitement du XML
            Config.Instance.LoadFromXML(destFile);

            // lancement de la récup des favicon pour l'applis et les URLs


            initOk = true;
        }

        public void DownloadFile(string url, string fileName)
        {
            // Get XML config File
            WebClient webClient = new WebClient();
            webClient.DownloadFile(url, Path.Combine(Path.GetTempPath(), "quicklauncher.tmp"));

            // déplacement du fichier à son emplacement définitif

            String destFile = Path.Combine(destFolder, fileName);
            if (File.Exists(destFile))
                File.Delete(destFile);

            File.Move(Path.Combine(Path.GetTempPath(), "quicklauncher.tmp"), destFile);
        }

        public Boolean InitOk
        {
            get
            {
                return initOk;
            }
        }

        public string GetDestFolder()
        {
            return destFolder;
        }
    }
}
