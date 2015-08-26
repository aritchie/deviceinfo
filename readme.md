#ACR Device Information for Xamarin & Windows

---

A cross platform plugin to get device specific information

* Unique DeviceID
    * IMEI on android
    * Vendor ID on iOS (this is unique to your applications on a device)
    * Windows (Phone) Device ID
* Camera Availability
* Screen Dimensions
* Manufacturer and Model
* Operating System
* Application Version Information
* Current Culture
* Battery Monitoring
* Network Monitoring

##iOS Setup




##Android Specifics

Device ID- IMEI (DeviceInfo.Hardware.DeviceId), set permission READ_PHONE_STATE
Battery status - set permissions BATTERY_STATE and DEVICE_POWER
Network Monitoring - set permission ACCESS_NETWORK_STATE

##Windows Phone Specifics

If you want to request the Device ID (DeviceInfo.Hardware.DeviceId), you must include the capability 'ID_CAP_IDENTITY_DEVICE' in your WP manifest.  


##To Use

Make sure to install the nuget package in your platform and PCL projects.  To use, simply start calling

    Acr.DeviceInfo.DeviceInfo.Hardware.<property> 
    Acr.DeviceInfo.DeviceInfo.App.<property>
    Acr.DeviceInfo.DeviceInfo.Battery.<property>
    Acr.DeviceInfo.DeviceInfo.Connectivity.<property>