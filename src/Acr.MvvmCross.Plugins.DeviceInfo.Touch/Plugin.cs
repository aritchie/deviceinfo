using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.DeviceInfo.Touch {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton(Acr.DeviceInfo.DeviceInfo.Hardware);
            Mvx.RegisterSingleton(Acr.DeviceInfo.DeviceInfo.App);
            Mvx.RegisterSingleton(Acr.DeviceInfo.DeviceInfo.Connectivity);
            Mvx.RegisterSingleton(Acr.DeviceInfo.DeviceInfo.Battery);
        }
    }
}