# ACR Device Information for Xamarin & Windows
A cross platform plugin to get device and application details

* Unique DeviceID
    * IMEI on android
    * Vendor ID on iOS (this is unique to your applications on a device)
* Screen Dimensions
* Manufacturer and Model
* Operating System
* Application Version Information
* Current Culture
* Power State Monitoring
* Network Monitoring


[![NuGet](https://img.shields.io/nuget/v/Acr.DeviceInfo.svg?maxAge=2592000)](https://www.nuget.org/packages/Acr.DeviceInfo/)
[Change Log - August 8, 2018](changelog.md)

## Supported Runtimes
* NET Standard
* Android
* iOS
* macOS
* Windows UWP

## Setup

* Make sure to install the nuget package to your core net standard library as well as your platform projects (iOS, Android, etc)

### Android

_Permissions_
* Battery status - set permissions BATTERY_STATE and DEVICE_POWER
* Network Monitoring - set permission ACCESS_NETWORK_STATE


## To Use

Make sure to install the nuget package in your platform and PCL projects.  To use, simply start calling

    CrossDevice.Device.<property>
    CrossDevice.App.<property>
    CrossDevice.PowerState.<property>
    CrossDevice.Network.<property>

## FAQ

1. DeviceId is coming back null
* Follow permission setting as listed above

2. I'm getting "Platform implementation not supported"
* Ensure you have installed the nuget package on your actual application project

3. UWP battery implementation doesn't do anything
* I haven't been able to find a way to support this yet

4. The DeviceId changes if I uninstall my app from the phone
* There isn't a "static" ID on an iphone that really works.  I use IdentifierForVendor()
