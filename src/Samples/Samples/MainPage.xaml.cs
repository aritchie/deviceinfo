using System;
using Acr.DeviceInfo;
using Xamarin.Forms;


namespace Samples {

    public partial class MainPage : ContentPage {

        public MainPage() {
            InitializeComponent();
            this.BindingContext = DeviceInfo.Instance;
        }
    }
}
