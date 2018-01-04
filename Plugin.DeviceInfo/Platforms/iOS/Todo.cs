using NetworkExtension;
using System;
using System.Threading.Tasks;


namespace Plugin.DeviceInfo.Platforms.iOS
{
    public class Todo
    {
        public static async Task Test(string ssid, string password, bool isWep = false)
        {
            // LifetimeIndDays
            var config = new NEHotspotConfiguration(ssid, password, isWep) { JoinOnce = true };
            var configManager = new NEHotspotConfigurationManager();
            await configManager.ApplyConfigurationAsync(config);
        }
    }
}
