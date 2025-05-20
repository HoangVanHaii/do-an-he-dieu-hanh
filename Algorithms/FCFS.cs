using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Data; // Ch?a class Process

namespace Algorithms
{
    public class FCFS 
    {
        private Queue<Process> queue = new Queue<Process>();

        public void Reset()
        {
            queue.Clear();
        }

        public Process GetNextProcess(List<Process> readyQueue, int currentTime)
        {
            // N?u hàng ??i r?ng thì n?p ti?n trình vào theo th? t? ??n
            if (queue.Count == 0)
            {
                var sorted = readyQueue
                    .Where(p => !p.IsCompleted && p.ArrivalTime <= currentTime)
                    .OrderBy(p => p.ArrivalTime)
                    .ThenBy(p => p.ID)
                    .ToList();

                foreach (var process in sorted)
                {
                    if (!queue.Contains(process))
                        queue.Enqueue(process);
                }
            }

            // L?y ti?n trình ??u tiên (n?u có)
            while (queue.Count > 0)
            {
                var p = queue.Peek();
                if (p.IsCompleted)
                {
                    queue.Dequeue(); // B? ti?n trình ?ã xong
                    continue;
                }
                MessageBox.Show("Process ID: " + p.ID.ToString());
                return p;
            }

            return null;
        }
    }
}
