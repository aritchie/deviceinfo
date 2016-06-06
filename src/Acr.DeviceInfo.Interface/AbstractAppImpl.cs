using System;
using System.Globalization;


namespace Acr.DeviceInfo
{
    public abstract class AbstractAppImpl : IApp
    {
        public abstract CultureInfo CurrentCulture { get; }
        public abstract string Version { get; }
        public abstract bool IsForegrounded { get; }
        public bool IsBackgrounded => !this.IsForegrounded;


        EventHandler localeHandler;
        public event EventHandler LocaleChanged
        {
            add
            {
                if (this.localeHandler == null)
                {
                    this.StartMonitoringLocaleUpdates();
                }
                this.localeHandler += value;
            }
            remove
            {
                this.localeHandler -= value;

                if (this.localeHandler == null)
                    this.StopMonitoringLocaleUpdates();
            }
        }


        EventHandler appStateChanged;
        public event EventHandler AppStateChanged
        {
            add
            {
                if (this.appStateChanged == null)
                    this.StartMonitoringAppState();

                this.appStateChanged += value;
            }
            remove
            {
                this.appStateChanged -= value;

                if (this.appStateChanged == null)
                    this.StopMonitoringAppState();
            }
        }


        protected abstract void StartMonitoringLocaleUpdates();
        protected abstract void StopMonitoringLocaleUpdates();


        protected abstract void StartMonitoringAppState();
        protected abstract void StopMonitoringAppState();


        protected virtual void OnLocaleChanged()
        {
            this.localeHandler?.Invoke(this, EventArgs.Empty);
        }


        protected virtual void OnAppStateChanged()
        {
            this.appStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
