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

##iOS Setup




##Android Specifics

If you want to request the IMEI (DeviceInfo.Instance.DeviceId), you must include the permission 'READ_PHONE_STATE' in your app.


##Windows Phone Specifics

If you want to request the Device ID (DeviceInfo.Instance.DeviceId), you must include the capability 'ID_CAP_IDENTITY_DEVICE' in your WP manifest.  


##To Use

Make sure to install the nuget package in your platform and PCL projects.  To use, simply start calling

    Acr.DeviceInfo.DeviceInfo.Instance.<property> 