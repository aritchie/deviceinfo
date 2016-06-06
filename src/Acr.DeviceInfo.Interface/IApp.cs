using System;
using System.Globalization;


namespace Acr.DeviceInfo
{

    public interface IApp
    {
        CultureInfo CurrentCulture { get; }
        string Version { get; }
        bool IsForegrounded { get; }
        bool IsBackgrounded { get; }

        event EventHandler AppStateChanged;
        event EventHandler LocaleChanged;
    }
}
