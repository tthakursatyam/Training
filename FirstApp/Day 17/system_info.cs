// using System;
// using System.Management;
// class System_Info
// {
//     public static void func()
//     {
//         Console.WriteLine("=== BASIC INFO ===");
//         Console.WriteLine("Machine Name: " + Environment.MachineName);
//         Console.WriteLine("OS Version: " + Environment.OSVersion);
//         Console.WriteLine("64-bit OS: " + Environment.Is64BitOperatingSystem);
//         Console.WriteLine("Logical Processors: " + Environment.ProcessorCount);
//         Console.WriteLine("User Name: " + Environment.UserName);

//         Console.WriteLine("\n=== CPU INFO ===");
//         foreach (var item in new ManagementObjectSearcher("select * from Win32_Processor").Get())
//         {
//             Console.WriteLine("CPU Name: " + item["Name"]);
//             Console.WriteLine("Cores: " + item["NumberOfCores"]);
//             Console.WriteLine("Logical Processors: " + item["NumberOfLogicalProcessors"]);
//             Console.WriteLine("Max Clock Speed (MHz): " + item["MaxClockSpeed"]);
//         }

//         Console.WriteLine("\n=== MEMORY INFO ===");
//         foreach (var item in new ManagementObjectSearcher("select * from Win32_ComputerSystem").Get())
//         {
//             double ram = Convert.ToDouble(item["TotalPhysicalMemory"]) / (1024 * 1024 * 1024);
//             Console.WriteLine("Installed RAM (GB): " + ram.ToString("0.00"));
//         }

//         Console.WriteLine("\n=== DISK INFO ===");
//         foreach (var item in new ManagementObjectSearcher("select * from Win32_LogicalDisk").Get())
//         {
//             Console.WriteLine("Drive: " + item["Name"]);
//             Console.WriteLine("  Free Space (GB): " +
//                 (Convert.ToDouble(item["FreeSpace"]) / (1024 * 1024 * 1024)).ToString("0.00"));
//             Console.WriteLine("  Total Size (GB): " +
//                 (Convert.ToDouble(item["Size"]) / (1024 * 1024 * 1024)).ToString("0.00"));
//         }

//         Console.WriteLine("\n=== GPU INFO ===");
//         foreach (var item in new ManagementObjectSearcher("select * from Win32_VideoController").Get())
//         {
//             Console.WriteLine("GPU: " + item["Name"]);
//         }
//     }
// }
