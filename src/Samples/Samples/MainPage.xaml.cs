using System;
using Acr.DeviceInfo;
using Xamarin.Forms;


namespace Samples
{

    public partial class MainPage : TabbedPage
    {
        readonly MainViewModel viewModel;


        public MainPage()
        {
            InitializeComponent();
            this.viewModel = new MainViewModel(DeviceInfo.App, DeviceInfo.Battery, DeviceInfo.Connectivity, DeviceInfo.Hardware);
            this.BindingContext = this.viewModel;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.viewModel.OnActivate();
        }
    }
}
