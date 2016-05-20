using System;
using System.Globalization;


namespace Acr.DeviceInfo
{
    public abstract class AbstractAppImpl : IApp
    {
        public CultureInfo Locale { get; protected set; }
        public bool IsBackgrounded { get; protected set; }
        public string Version { get; protected set; }


        public event EventHandler LocaleChanged;
        public event EventHandler Resuming;
        public event EventHandler EnteringSleep;


        protected abstract void StartMonitoringLocaleUpdates();
        protected abstract void StopMonitoringLocaleUpdates();

        protected abstract void StartMonitoringAppState();
        protected abstract void StopMonitoringAppState();


        protected virtual void OnLocaleChanged()
        {
            this.LocaleChanged?.Invoke(this, EventArgs.Empty);
        }


        protected virtual void OnResuming()
        {
            this.Resuming?.Invoke(this, EventArgs.Empty);
        }


        protected virtual void OnEnteringSleep()
        {
            this.EnteringSleep?.Invoke(this, EventArgs.Empty);
        }
    }
}
