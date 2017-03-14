using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Plugin.DeviceInfo;
using PropertyChanged;


namespace Samples
{
    [ImplementPropertyChanged]
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel(IAppInfo app, IBatteryInfo battery, INetworkInfo network, IHardwareInfo hardware)
        {
            this.App = app;
            this.Battery = battery;
            this.Network = network;
            this.Hardware = hardware;

            this.ClearApp = new Command(this.AppEvents.Clear);
            this.ClearBattery = new Command(this.BatteryEvents.Clear);
            this.ClearNetwork = new Command(this.NetworkEvents.Clear);
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public string IpAddress => this.Network.IpAddress;
        public string CellularNetworkCarrier => this.Network.CellularNetworkCarrier;
        public NetworkReachability InternetReachability => this.Network.InternetReachability;
        public string WifiSsid => this.Network.WifiSsid;

        public PowerStatus BatteryStatus { get; set; }
        public int BatteryPercent { get; set; }

        public IAppInfo App { get; private set; }
        public IBatteryInfo Battery { get; private set; }
        public IHardwareInfo Hardware { get; private set; }
        public INetworkInfo Network { get; private set; }

        public ObservableCollection<EventViewModel> AppEvents { get; } = new ObservableCollection<EventViewModel>();
        public ObservableCollection<EventViewModel> NetworkEvents { get; } = new ObservableCollection<EventViewModel>();
        public ObservableCollection<EventViewModel> BatteryEvents { get; } = new ObservableCollection<EventViewModel>();

        public ICommand ClearApp { get; }
        public ICommand ClearBattery { get; }
        public ICommand ClearNetwork { get; }

        bool firstStart = true;
        IDisposable batteryPower;
        IDisposable batteryPercent;
        IDisposable connectivityChange;


        public void Start()
        {
            if (!this.firstStart)
                return;

            this.batteryPower = this.Battery
                .WhenPowerStatusChanged()
                .Subscribe(x => Device.BeginInvokeOnMainThread(() =>
                {
                    this.BatteryStatus = x;
                    this.BatteryEvents.Insert(0, new EventViewModel
                    {
                        Detail = $"Status Change: {x}"
                    });
                }));

            this.batteryPercent = this.Battery
                .WhenBatteryPercentageChanged()
                .Subscribe(x => Device.BeginInvokeOnMainThread(() =>
                {
                    this.BatteryPercent = x;
                    this.BatteryEvents.Insert(0, new EventViewModel
                    {
                        Detail = $"Charge Change: {x}%"
                    });
                }));

            this.connectivityChange = this.Network
                .WhenStatusChanged()
                .Subscribe(x => Device.BeginInvokeOnMainThread(() =>
                {
                    this.Raise(nameof(IpAddress));
                    this.Raise(nameof(CellularNetworkCarrier));
                    this.Raise(nameof(InternetReachability));
                    this.Raise(nameof(WifiSsid));
                    this.NetworkEvents.Insert(0, new EventViewModel
                    {
                        Detail = $"Network Reachability Change: {x}"
                    });
                }));

            this.App
                .WhenEnteringForeground()
                .Subscribe(x => Device.BeginInvokeOnMainThread(() =>
                    this.AppEvents.Insert(0, new EventViewModel
                    {
                        Detail = "Foregrounding App"
                    }))
                );

            this.App
                .WhenEnteringBackground()
                .Subscribe(x => Device.BeginInvokeOnMainThread(() =>
                    this.AppEvents.Insert(0, new EventViewModel
                    {
                        Detail = "Backgrounding App"
                    }))
                );
        }


        void Raise(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
     }
}
