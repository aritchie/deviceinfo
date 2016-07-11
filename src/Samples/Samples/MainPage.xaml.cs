using System;
using System.Diagnostics;
using Acr.DeviceInfo;
using Xamarin.Forms;


namespace Samples
{

    public partial class MainPage : ContentPage
    {
        readonly MainViewModel viewModel;


        public MainPage()
        {
            InitializeComponent();
            this.viewModel = new MainViewModel(DeviceInfo.App, DeviceInfo.Battery, DeviceInfo.Connectivity, DeviceInfo.Hardware);
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.viewModel.OnActivate();
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.viewModel.OnDeactivate();
        }
    }
}
