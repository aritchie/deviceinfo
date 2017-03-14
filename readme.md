# ACR Device Information for Xamarin & Windows
A cross platform plugin to get device and application details

* Unique DeviceID
    * IMEI on android
    * Vendor ID on iOS (this is unique to your applications on a device)
* Camera Availability
* Screen Dimensions
* Manufacturer and Model
* Operating System
* Application Version Information
* Current Culture
* Battery Monitoring
* Network Monitoring

## Supported Operating Systems
* NET Standard 1.0
* Android
* iOS
* tvOS
* macOS
* Windows UWP

## Setup

### Android

_Permissions_
* Device ID- IMEI (DeviceInfo.Hardware.DeviceId), set permission READ_PHONE_STATE
* Battery status - set permissions BATTERY_STATE and DEVICE_POWER
* Network Monitoring - set permission ACCESS_NETWORK_STATE


## To Use

Make sure to install the nuget package in your platform and PCL projects.  To use, simply start calling

    CrossDevice.Hardware.<property>
    CrossDevice.App.<property>
    CrossDevice.Battery.<property>
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