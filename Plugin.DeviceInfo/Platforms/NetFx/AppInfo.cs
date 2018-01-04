using System;
using System.Reflection;


namespace Plugin.DeviceInfo
{

    public class AppInfo : AbstractAppInfo
    {
        public override string Version => Assembly
            .GetEntryAssembly()
            .GetName()
            .Version
            .ToString();

        public override string ShortVersion => this.Version;
    }
}
