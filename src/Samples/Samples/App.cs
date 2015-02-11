using System;
using Acr.DeviceInfo;
using Xamarin.Forms;


namespace Samples {

    public class App : Application {

        public App() {
            var dev = DeviceInfo.Instance;

            this.MainPage = new ContentPage {
                Content = new TableView(new TableRoot("Device Info") {
                    new TableSection("Device") {
                        new TextCell {
                            Text = "Device ID",
                            Detail = dev.DeviceId
                        },
                        new TextCell {
                            Text = "Operating System",
                            Detail = dev.OperatingSystem
                        },
                        new TextCell {
                            Text = dev.Manufacturer,
                            Detail = dev.Model
                        },
                        new TextCell {
                            Text = "Simulator",
                            Detail = dev.IsSimulator.ToString()
                        },
                        new TextCell {
                            Text = "App Version",
                            Detail = dev.AppVersion
                        }
                    },
                    new TableSection("Display") {
                        new TextCell {
                            Text = "Width",
                            Detail = dev.ScreenWidth.ToString()
                        },
                        new TextCell {
                            Text = "Height",
                            Detail = dev.ScreenHeight.ToString()
                        }
                    },
                    new TableSection("Camera") {
                        new TextCell {
                            Text = "Front Camera",
                            Detail = dev.IsFrontCameraAvailable.ToString()
                        },
                        new TextCell {
                            Text = "Rear Camera",
                            Detail = dev.IsRearCameraAvailable.ToString()
                        }
                    }
                })
            };
        }
    }
}
