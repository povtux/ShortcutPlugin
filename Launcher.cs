using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutPlugin
{
    class Launcher
    {
        public static int TYPE_APPLICATION = 1;
        public static int TYPE_URL = 2;

        private String name;
        private int type;
        private Boolean msbrowser;
        private Boolean vpn;
        private String subject;

        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public int Type
        {
            get
            {
                return type;
            }
            set
            {
                if(value >= 1 && value <= 2)
                    type = value;  
            }
        }

        public String Subject
        {
            get
            {
                return subject;
            }
            set
            {
                subject = value;
            }
        }

        public Boolean MsBrowser
        {
            get
            {
                return msbrowser;
            }
            set
            {
                msbrowser = value;
            }
        }

        public Boolean VPN
        {
            get
            {
                return vpn;
            }
            set
            {
                vpn = value;
            }
        }
    }
}
