using System.Windows.Forms;
using LauncherPlugin;

namespace ShortcutPlugin
{
    class Plugin : ILauncherPlugin
    {
        private ShortcutPluginControl controle;

        public string Name => "ShortcutPlugin";

        public string Version => MajorVersion + "." + MinorVersion;

        public int MajorVersion => 0;

        public int MinorVersion => 1;

        public UserControl Init()
        {
            if (controle == null) controle = new ShortcutPluginControl();
            return controle;
        }
    }
}
