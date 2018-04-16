@echo off
nuget push .\Plugin.DeviceInfo\bin\Release\*.nupkg -Source https://www.nuget.org/api/v2/package
nuget push .\Plugin.DeviceInfo\bin\Release\*.nupkg -Source https://www.myget.org/F/acr/api/v2/package
del .\Plugin.DeviceInfo\bin\Release\*.nupkg
pause