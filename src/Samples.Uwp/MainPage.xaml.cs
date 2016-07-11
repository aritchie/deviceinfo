using System;
using Xamarin.Forms.Platform.UWP;


namespace Samples.Uwp
{
    public sealed partial class MainPage : WindowsPage
    {

        public MainPage()
        {
            InitializeComponent();
            this.LoadApplication(new Samples.App());
        }
    }
}
