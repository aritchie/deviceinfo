@echo off
del *.nupkg
nuget pack Acr.DeviceInfo.nuspec
nuget pack Acr.MvvmCross.Plugins.DeviceInfo.nuspec
pause