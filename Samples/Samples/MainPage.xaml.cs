using System;
using Xamarin.Forms;
using Plugin.DeviceInfo;

namespace Samples
{

    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.BindingContext = new MainViewModel(
                CrossDevice.App,
                CrossDevice.PowerState,
                CrossDevice.Network,
                CrossDevice.Device
            );
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((MainViewModel)this.BindingContext).Start();
        }
    }
}
