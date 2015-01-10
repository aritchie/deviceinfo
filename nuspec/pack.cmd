@echo off
del *.nupkg
nuget pack Acr.MvvmCross.nuspec
nuget pack Acr.MvvmCross.Plugins.FileSystem.nuspec
nuget pack Acr.MvvmCross.Plugins.BarCodeScanner.nuspec
nuget pack Acr.MvvmCross.Plugins.DeviceInfo.nuspec
nuget pack Acr.MvvmCross.Plugins.Settings.nuspec
nuget pack Acr.MvvmCross.Plugins.UserDialogs.nuspec
nuget pack Acr.MvvmCross.Plugins.Network.nuspec
nuget pack Acr.MvvmCross.Plugins.SignaturePad.nuspec

nuget pack Acr.MvvmCross.Pack.nuspec
pause