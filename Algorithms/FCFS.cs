using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Algorithms
{
    public class FCFS
    {
        public List<Process> Run(List<Process> processes, out double avgWaitTime, out double avgTurnaroundTime)
        {
            var sorted = processes.OrderBy(p => p.ArrivalTime).ThenBy(p => p.ID).ToList();
            int currentTime = 0;

            foreach (var process in sorted)
            {
                if (currentTime < process.ArrivalTime)
                    currentTime = process.ArrivalTime;

                process.StartTime = currentTime;
                process.WaitTime = currentTime - process.ArrivalTime;

                currentTime += process.BurstTime;

                process.FinishTime = currentTime;
                process.TurnaroundTime = process.FinishTime - process.ArrivalTime;

                process.IsCompleted = true;
            }

            avgWaitTime = sorted.Average(p => p.WaitTime);
            avgTurnaroundTime = sorted.Average(p => p.TurnaroundTime);

            return sorted;
        }
    }
}
