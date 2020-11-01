using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace WebDevElin.Data
{
  public class DiagnosticInfo
  {
    private PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
    private PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");

    public ushort GetCPULoad()
    {
      return (ushort)cpuCounter.NextValue();
    }

    public int GetRAMLoad()
    {
      return (int)ramCounter.NextValue();
    }

    public async Task<List<string>> GetLogs()
    {
      var list = new List<string>();
      var eventlog = EventLog.GetEventLogs("DESKTOP-O0O4A8D");
      await Task.Run(() =>
      {
        foreach (var log in eventlog)
        {
          foreach (var entry in log.Entries.OfType<EventLogEntry>().TakeLast(1))
          {
            list.Add(entry.Message);
          }
        }
      });
      return list;
    }
  }
}
