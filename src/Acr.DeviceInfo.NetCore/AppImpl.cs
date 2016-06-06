using System;
using System.Globalization;
using System.Reflection;


namespace Acr.DeviceInfo
{

    public class AppImpl : AbstractAppImpl
    {

        public AppImpl()
        {
            //this.Locale = CultureInfo.DefaultThreadCurrentCulture;
            this.Version = Assembly
                .GetEntryAssembly()
                .GetName()
                .Version
                .ToString();
        }

        public override CultureInfo Locale { get; }
        public override bool IsForegrounded { get; }
        protected override void StartMonitoringLocaleUpdates()
        {
            throw new NotImplementedException();
        }

        protected override void StopMonitoringLocaleUpdates()
        {
            throw new NotImplementedException();
        }

        protected override void StartMonitoringAppState()
        {
            throw new NotImplementedException();
        }

        protected override void StopMonitoringAppState()
        {
            throw new NotImplementedException();
        }
    }
}
