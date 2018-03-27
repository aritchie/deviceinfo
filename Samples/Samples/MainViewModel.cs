using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Plugin.DeviceInfo;


namespace Samples
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel(IApp app, IPowerState power, INetwork network, IDevice device)
        {
            this.App = app;
            this.Device = device;
            this.Network = network;
            this.Power = power;

            this.ClearApp = new Command(this.AppEvents.Clear);
            this.ClearPower = new Command(this.BatteryEvents.Clear);
            this.ClearNetwork = new Command(this.NetworkEvents.Clear);
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public string IpAddress => this.Network.IpAddress;
        public string CellularNetworkCarrier => this.Network.CellularNetworkCarrier;
        public NetworkType InternetReachability => this.Network.InternetNetworkType;
        public string WifiSsid => this.Network.WifiSsid;

        public PowerStatus BatteryStatus { get; set; }
        public int BatteryPercent { get; set; }

        public IApp App { get; }
        public IDevice Device { get; }
        public INetwork Network { get; }
        public IPowerState Power { get; }

        public ObservableCollection<EventViewModel> AppEvents { get; } = new ObservableCollection<EventViewModel>();
        public ObservableCollection<EventViewModel> NetworkEvents { get; } = new ObservableCollection<EventViewModel>();
        public ObservableCollection<EventViewModel> BatteryEvents { get; } = new ObservableCollection<EventViewModel>();

        public ICommand ClearApp { get; }
        public ICommand ClearPower { get; }
        public ICommand ClearNetwork { get; }

        bool firstStart = true;
        IDisposable batteryPower;
        IDisposable batteryPercent;
        IDisposable connectivityChange;


        public void Start()
        {
            if (!this.firstStart)
                return;

            this.batteryPower = this.Power
                .WhenPowerStatusChanged()
                .Subscribe(x => Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    this.BatteryStatus = x;
                    this.BatteryEvents.Insert(0, new EventViewModel
                    {
                        Detail = $"Status Change: {x}"
                    });
                }));

            this.batteryPercent = this.Power
                .WhenBatteryPercentageChanged()
                .Subscribe(x => Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    this.BatteryPercent = x;
                    this.BatteryEvents.Insert(0, new EventViewModel
                    {
                        Detail = $"Charge Change: {x}%"
                    });
                }));

            this.connectivityChange = this.Network
                .WhenNetworkTypeChanged()
                .Subscribe(x => Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
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
                .Subscribe(x => Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    this.AppEvents.Insert(0, new EventViewModel
                    {
                        Detail = "Foregrounding App"
                    }))
                );

            this.App
                .WhenEnteringBackground()
                .Subscribe(x => Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    this.AppEvents.Insert(0, new EventViewModel
                    {
                        Detail = "Backgrounding App"
                    }))
                );
        }


        void Raise(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
     }
}
