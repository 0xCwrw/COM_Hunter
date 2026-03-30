/*
    Author: Cwrw
    Date: 2026-03-29
    Description: This small project allows a privileged attacker (Administrator privileges required) to easily identify potential COM hijack opportunities directly from the command line. No need for GUI and procmon access :p.
*/
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Session;

class Program
{
    static void Main()
    {
        // Checks for admin privileges to start a kernel session.
        if (!(bool)TraceEventSession.IsElevated())
        {
            Console.WriteLine("Requires Admin skid.");
            return;
        }
        // Create a real-time monitoring kernel session.
        using (var session = new TraceEventSession(KernelTraceEventParser.KernelSessionName))
        {
            // Focus on registry events.
            session.EnableKernelProvider(KernelTraceEventParser.Keywords.Registry);

            // Filter on failed registry open events focusing on COM paths.
            session.Source.Kernel.RegistryOpen += data =>
            {
                if(data.Status != 0 && (data.KeyName.Contains(@"InprocServer32", StringComparison.OrdinalIgnoreCase) | data.KeyName.Contains(@"LocalServer32", StringComparison.OrdinalIgnoreCase)))
                Console.WriteLine($"PID: {data.ProcessID}, " +
                                  $"Process: {data.ProcessName}, " +
                                  $"KeyName: {data.KeyName}, " +
                                  $"Status: {data.Status}");
            };
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[+] Listening for potential COM hijacks, this may take a while... Press Ctrl+C to exit.");
            Console.ResetColor();
            session.Source.Process();
        }
    }
}