using System;
using Xamarin.Forms.Platform.UWP;


namespace Samples.Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : WindowsPage
    {

        public MainPage()
        {
            InitializeComponent();
            this.LoadApplication(new Samples.App());
        }
    }
}
