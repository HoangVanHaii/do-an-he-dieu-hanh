using Algorithms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Data;

namespace CPUSchedulerProject {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            JobPool.Invalidate();
            int rows = int.Parse(numProcess.Text);
            MessageBox.Show(rows.ToString());
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numProcess_TextChanged(object sender, EventArgs e)
        {
            string inp = numProcess.Text;
            if (!string.IsNullOrWhiteSpace(inp))
            {
                JobPool.Rows.Clear();
                int row = int.Parse(inp);
                for(int i = 0; i< row; i++)
                {
                    JobPool.Rows.Add();
                    JobPool.Rows[i].HeaderCell.Value = "P" + (i + 1).ToString();
                }
                JobPool.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string algorithm = AlorithmCombo.SelectedItem?.ToString();
            List<Process> processList = new List<Process>();

            try
            {
                int rowCount = int.Parse(numProcess.Text);
                for (int i = 0; i < rowCount; i++)
                {
                    int arrivalTime = int.Parse(JobPool.Rows[i].Cells[0].Value.ToString());
                    int burstTime = int.Parse(JobPool.Rows[i].Cells[1].Value.ToString());
                    Process process = new Process
                    {
                        ID = i + 1,
                        ArrivalTime = arrivalTime,
                        BurstTime = burstTime
                    };

                    processList.Add(process);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi dữ liệu đầu vào: " + ex.Message);
                return;
            }

            if (algorithm == "FCFS")
            {
                FCFS scheduler = new FCFS();
                double avgWait, avgTat;

                var resultList = scheduler.Run(processList, out avgWait, out avgTat);

                // Hiển thị kết quả trong DataGridView mới hoặc hiện tại
                string result = "";
                foreach (var p in resultList)
                {
                    result += $"ID: {p.ID}, Arrival: {p.ArrivalTime}, Burst: {p.BurstTime}, " +
                              $"Start: {p.StartTime}, Finish: {p.FinishTime}, " +
                              $"Wait: {p.WaitTime}, Turnaround: {p.TurnaroundTime}\n";
                }
                MessageBox.Show(result, "Tất cả tiến trình");

                MessageBox.Show($"Thời gian chờ trung bình: {avgWait:F2}\nThời gian hoàn thành trung bình: {avgTat:F2}", "Kết quả FCFS");
            }
        }

    }
}