using System;
using System.Globalization;


namespace Acr.DeviceInfo
{
    public abstract class AbstractAppImpl : IApp
    {
        public CultureInfo Locale { get; protected set; }
        public bool IsBackgrounded { get; protected set; }
        public event EventHandler LocaleChanged;
        public event EventHandler Resuming;
        public event EventHandler Backgrounding;

        public string Version { get; protected set; }

    }
}
