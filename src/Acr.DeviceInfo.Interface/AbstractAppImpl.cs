using System;
using System.Globalization;


namespace Acr.DeviceInfo
{
    public abstract class AbstractAppImpl : IApp
    {
        public CultureInfo Locale { get; protected set; }
        public bool IsBackgrounded { get; protected set; }
        public string Version { get; protected set; }


        EventHandler localeHandler;
        public event EventHandler LocaleChanged
        {
            add
            {
                if (this.localeHandler == null)
                    this.StartMonitoringLocaleUpdates();

                this.localeHandler += value;
            }
            remove
            {
                this.localeHandler -= value;

                if (this.localeHandler == null)
                    this.StopMonitoringLocaleUpdates();
            }
        }


        EventHandler resumeHandler;
        public event EventHandler Resuming
        {
            add
            {
                if (this.resumeHandler == null && this.sleepHandler == null)
                    this.StartMonitoringAppState();

                this.resumeHandler += value;
            }
            remove
            {
                this.resumeHandler -= value;

                if (this.resumeHandler == null && this.sleepHandler == null)
                    this.StopMonitoringAppState();
            }
        }


        EventHandler sleepHandler;
        public event EventHandler EnteringSleep
        {
            add
            {
                if (this.resumeHandler == null && this.sleepHandler == null)
                    this.StartMonitoringAppState();

                this.sleepHandler += value;
            }
            remove
            {
                this.sleepHandler -= value;

                if (this.resumeHandler == null && this.sleepHandler == null)
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


        protected virtual void OnResuming()
        {
            this.resumeHandler?.Invoke(this, EventArgs.Empty);
        }


        protected virtual void OnEnteringSleep()
        {
            this.sleepHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}
