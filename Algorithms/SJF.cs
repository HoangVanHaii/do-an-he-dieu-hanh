using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Data;  

namespace CPUSchedulerProject.Algorithms
{
    internal class SJF
    {
        private Queue<Process> queue = new Queue<Process>();

        public void Reset()
        {
            queue.Clear();
        }

        public Process GetNextProcess(List<Process> readyQueue, int currentTime)
        {   
            // sắp xếp theo BurstTime (thời gian ngắn nhất trước), rồi theo ID để ổn định thứ tự
            if (queue.Count == 0)
            {
                var sorted = readyQueue
                    .Where(p => !p.IsCompleted && p.ArrivalTime <= currentTime)
                    .OrderBy(p => p.BurstTime)
                    .ThenBy(p => p.ID)
                    .ToList();

                foreach (var process in sorted)
                {
                    if (!queue.Contains(process))
                        queue.Enqueue(process);
                }
            }

            // Lấy tiến trình đầu tiên trong queue (theo thứ tự BurstTime nhỏ nhất)
            while (queue.Count > 0)
            {
                var p = queue.Peek();

                if (p.IsCompleted)
                {
                    MessageBox.Show("finish: " + p.ID.ToString());
                    queue.Dequeue(); // Loại bỏ tiến trình đã hoàn thành
                    continue;
                }

                MessageBox.Show("Process ID: " + p.ID.ToString());
                return p;
            }

            return null; 
        }
    }
}
