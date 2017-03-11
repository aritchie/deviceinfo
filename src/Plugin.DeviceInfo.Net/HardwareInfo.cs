using System;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Plugin.DeviceInfo
{

    public class HardwareInfo : IHardwareInfo
    {

        public HardwareInfo()
        {

            //var mos = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            //var obj = mos.Get().Cast<ManagementObject>().FirstOrDefault();

            //this.Manufacturer = obj["Manufacturer"].ToString();
            //this.Model = obj["model"].ToString();
            //this.OperatingSystem = $"{obj["Caption"]} - {obj["Version"]}";

            //mos = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor");
            //obj = mos.Get().Cast<ManagementObject>().FirstOrDefault();
            //this.DeviceId = obj["ProcessorId"].ToString();
        }


        public int ScreenHeight { get; } = SystemInformation.VirtualScreen.Height;
        public int ScreenWidth { get; } = SystemInformation.VirtualScreen.Width;
        public string DeviceId { get; }
        public string Manufacturer { get; }
        public string Model { get; }
        public string OperatingSystem { get; }
        public bool IsSimulator { get; } = false;
        public bool IsTablet { get; } = false;
        public OperatingSystemType OS { get; } = OperatingSystemType.NetCore;
    }
}
