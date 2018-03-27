using System;
using System.Reflection;


namespace Plugin.DeviceInfo
{

    public class AppImpl : AbstractApp
    {
        public override bool EnableSleepMode { get; set; }

        public override string Version => Assembly
            .GetEntryAssembly()
            .GetName()
            .Version
            .ToString();

        public override string ShortVersion => this.Version;
    }
}
