using System;
using System.Reflection;


namespace Plugin.DeviceInfo
{
    public class AppImpl : AbstractApp
    {
        public override string BundleName => this.Version;

        public override string Version => Assembly
            .GetEntryAssembly()
            .GetName()
            .Version
            .ToString();

        public override string ShortVersion => this.Version;
    }
}
