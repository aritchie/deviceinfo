using System;
using System.Globalization;
using System.Reflection;


namespace Acr.DeviceInfo {

    public class AppImpl : AbstractAppImpl {

        public AppImpl() {
            this.Locale = CultureInfo.DefaultThreadCurrentCulture;
            this.Version = Assembly
                .GetEntryAssembly()
                .GetName()
                .Version
                .ToString();
        }
    }
}
