using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutPlugin
{
    class Config
    {
        private static Config conf;

        private String iconUrl = "";
        private String logoUrl = "";
        private String description = "";
        private ArrayList launchers;

        private Config() { }

        public static Config Instance
        {
            get
            {
                if (conf == null)
                    conf = new Config();

                return conf;
            }
        }

        public void LoadFromXML(String xmlFileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFileName);

            XmlNodeList elemList = doc.GetElementsByTagName("param");
            for (int i = 0; i < elemList.Count; i++)
            {
                XmlElement elem = (XmlElement)elemList[i];
                switch(elem.GetAttribute("name"))
                {
                    case "icon":
                        iconUrl = elem.InnerText;
                        break;
                    case "logo":
                        logoUrl = elem.InnerText;
                        break;
                    case "description":
                        description = elem.InnerText;
                        break;
                }
            }

            launchers = new ArrayList();
            elemList = doc.GetElementsByTagName("launcher");
            for (int i = 0; i < elemList.Count; i++)
            {
                XmlElement elem = (XmlElement)elemList[i];
                Launcher l = new Launcher()
                {
                    Name = elem.GetAttribute("name"),
                    Subject = elem.GetAttribute("subject"),
                    MsBrowser = elem.GetAttribute("force-msbrowser").ToLower().Equals("true"),
                    VPN = elem.GetAttribute("vpn-required").ToLower().Equals("true")
            };
                if (elem.GetAttribute("type").Equals("EXE"))
                    l.Type = Launcher.TYPE_APPLICATION;
                else if (elem.GetAttribute("type").Equals("URL"))
                    l.Type = Launcher.TYPE_URL;

                launchers.Add(l);
            }
        }

        public ArrayList Launchers
        {
            get
            {
                return launchers;
            }
        }

        public String IconUrl
        {
            get
            {
                return iconUrl;
            }
        }

        public String LogoUrl
        {
            get
            {
                return logoUrl;
            }
        }

        public String Description
        {
            get
            {
                return description;
            }
        }
    }
}
