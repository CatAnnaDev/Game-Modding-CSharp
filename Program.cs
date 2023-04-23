using System.Diagnostics;
using Memory.Win64;
using Memory.Utils;

namespace MemoryRW
{
    internal abstract class Program
    {
        private static void Main()
        {
            Process? process = Process.GetProcessesByName("Tutorial-x86_64").FirstOrDefault();
            if (process == null)
                return;
            
            var memory = new MemoryHelper64(process);
            ulong baseAddress = memory.GetBaseAddress(0x325A70);
            Console.WriteLine("Base Address: 0x{0:X}", baseAddress);
            int[] offsets = { 0x468, 0x40, 0xD0, 0x28, 0xF8, 0x18, 0x7F8 };
            ulong address = MemoryUtils.OffsetCalculator(memory, baseAddress, offsets);
            Console.WriteLine($"Address: 0x{address:X}");
            
            while (true)
            {
                Console.WriteLine($"Value: {memory.ReadMemory<int>(address)}"); // 0x014DE3E8
                memory.WriteMemory(address, 1000);
            }
        }
    }
}
