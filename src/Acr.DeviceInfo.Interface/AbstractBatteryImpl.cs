using System;


namespace Acr.DeviceInfo {

    public abstract class AbstractBatteryImpl : AbstractNpc, IBattery {

        int percentage;
        public int Percentage {
            get { return this.percentage; }
            protected set { this.SetProperty(ref this.percentage, value); }
        }


        bool charging;
        public bool IsCharging {
            get { return this.charging; }
            protected set { this.SetProperty(ref this.charging, value); }
        }
    }
}
