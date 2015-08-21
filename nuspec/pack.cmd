@echo off
del *.nupkg
nuget pack Acr.DeviceInfo.nuspec
rem nuget pack Acr.MvvmCross.Plugins.DeviceInfo.nuspec
pause