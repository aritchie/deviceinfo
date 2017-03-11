using System;
using Xamarin.Forms;


namespace Samples
{

    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            //this.viewModel = new MainViewModel(DeviceInfo.App, DeviceInfo.Battery, DeviceInfo.Connectivity, DeviceInfo.Hardware);
        }


        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    this.viewModel.OnActivate();
        //}
    }
}
