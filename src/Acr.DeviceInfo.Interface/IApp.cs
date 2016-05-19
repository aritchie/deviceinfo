using System;
using System.Globalization;


namespace Acr.DeviceInfo
{

    public interface IApp
    {
        string Version { get; }
        bool IsBackgrounded { get; }
        CultureInfo Locale { get; }

        event EventHandler LocaleChanged;
        event EventHandler Resuming;
        event EventHandler Backgrounding;
    }
}
