using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.DeviceInfo;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;


namespace Samples
{
    public class MainViewModel : ReactiveObject
    {
        public MainViewModel(IApp app, IBattery battery, IConnectivity connectivity, IHardware hardware)
        {
            this.App = app;
            this.Battery = battery;
            this.Connectivity = connectivity;
            this.Hardware = hardware;

            this.ClearApp = new Command(this.AppEvents.Clear);
            this.ClearBattery = new Command(this.BatteryEvents.Clear);
            this.ClearConnectivity = new Command(this.ConnectivityEvents.Clear);
        }

        [Reactive] public bool HasCamera { get; private set; }
        [Reactive] public bool HasBluetooth { get; private set; }
        [Reactive] public bool HasBluetoothLE { get; private set; }
        [Reactive] public bool HasFrontCamera { get; private set; }
        [Reactive] public bool HasBackCamera { get; private set; }

        public IApp App { get; private set; }
        public IBattery Battery { get; private set; }
        public IHardware Hardware { get; private set; }
        public IConnectivity Connectivity { get; private set; }

        public ObservableCollection<EventViewModel> AppEvents { get; } = new ObservableCollection<EventViewModel>();
        public ObservableCollection<EventViewModel> ConnectivityEvents { get; } = new ObservableCollection<EventViewModel>();
        public ObservableCollection<EventViewModel> BatteryEvents { get; } = new ObservableCollection<EventViewModel>();

        public ICommand ClearApp { get; }
        public ICommand ClearBattery { get; }
        public ICommand ClearConnectivity { get; }

        bool firstStart = true;
        IDisposable batteryPower;
        IDisposable batteryPercent;
        IDisposable connectivityChange;


        public void OnActivate()
        {
            Task.Run(async () =>
            {
                this.batteryPower = this.Battery
                    .WhenPowerStatusChanged()
                    .Subscribe(x => this.OnBatteryEvent($"Status Change: {x}"));

                this.batteryPercent = this.Battery
                    .WhenBatteryPercentageChanged()
                    .Subscribe(x => this.OnBatteryEvent($"Battery Charge Change: {x}%"));

                this.connectivityChange = this.Connectivity
                    .WhenStatusChanged()
                    .Subscribe(x =>
                    {
                        this.Connectivity = this.Connectivity;
                        this.RaiseAndSafe("Connectivity", () =>
                            this.ConnectivityEvents.Insert(0, new EventViewModel
                            {
                                Detail = $"Network Reachability Change: {x}"
                            })
                            );
                    });

                if (!this.firstStart)
                    return;

                this.App
                    .WhenEnteringForeground()
                    .Subscribe(x => this.OnAppEvent("Foregrounding App"));

                this.App
                    .WhenEnteringBackground()
                    .Subscribe(x => this.OnAppEvent("Backgrounding App"));

                this.HasCamera = await this.Hardware.HasFeature(Feature.Camera);
                this.HasFrontCamera = await this.Hardware.HasFeature(Feature.CameraFront);
                this.HasBackCamera = await this.Hardware.HasFeature(Feature.CameraBack);
                this.HasBluetooth = await this.Hardware.HasFeature(Feature.Bluetooth);
                this.HasBluetoothLE = await this.Hardware.HasFeature(Feature.BluetoothLE);
            });
        }


        public void OnDeactivate()
        {
            this.batteryPower.Dispose();
            this.batteryPercent.Dispose();
            this.connectivityChange.Dispose();
        }


        void RaiseAndSafe(string raise, Action action)
        {
            this.RaisePropertyChanged(raise);
            if (this.Hardware.OS == OperatingSystemType.WindowUniversal)
            {
                Device.BeginInvokeOnMainThread(action);
            }
            else
            {
                action();
            }
        }


        void OnAppEvent(string detail)
        {
            this.RaiseAndSafe("App", () =>
                this.AppEvents.Insert(0, new EventViewModel
                {
                    Detail = detail
                })
            );
        }


        void OnBatteryEvent(string detail)
        {
            this.RaiseAndSafe("Battery", () =>
                this.BatteryEvents.Insert(0, new EventViewModel
                {
                    Detail = detail
                })
            );
        }
    }
}
