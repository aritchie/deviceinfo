using System;
using Plugin.DeviceInfo;


namespace Net4Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            CrossDevice
                .Network
                .WhenStatusChanged()
                .Subscribe(x =>
                    Console.WriteLine("Reachability: " + x)
                );
            Console.WriteLine("Press <ENTER> to exit");
            Console.ReadLine();
        }
    }
}