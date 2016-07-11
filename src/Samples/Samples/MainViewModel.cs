using System;
using Acr.DeviceInfo;
using ReactiveUI;


namespace Samples
{
    public class MainViewModel : ReactiveObject
    {
        readonly IApp app;
        readonly IBattery battery;
        readonly IConnectivity connectivity;
        readonly IHardware hardware;


        public MainViewModel(IApp app, IBattery battery, IConnectivity connectivity, IHardware hardware)
        {

        }


        public void OnActivate()
        {

        }


        public void OnDeactivate()
        {

        }
    }
}
