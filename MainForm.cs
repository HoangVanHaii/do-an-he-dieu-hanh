using Algorithms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Data;
using System.Drawing;
using System.Threading.Tasks;

namespace CPUSchedulerProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }

        // Biến toàn cục
        List<Process> processList = new List<Process>();
        List<Process> resultList = new List<Process>();
        double avgWaitTime = 0;
        double avgTurnaroundTime = 0;

        private void panel4_Paint(object sender, PaintEventArgs e) { }

        private void label5_Click(object sender, EventArgs e) { }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            JobPool.Invalidate();
            int rows = int.Parse(numProcess.Text);
            MessageBox.Show(rows.ToString());
        }

        private void panel6_Paint(object sender, PaintEventArgs e) { }

        private void numProcess_TextChanged(object sender, EventArgs e)
        {
            string inp = numProcess.Text;
            if (!string.IsNullOrWhiteSpace(inp))
            {
                JobPool.Rows.Clear();
                int row = int.Parse(inp);
                for (int i = 0; i < row; i++)
                {
                    JobPool.Rows.Add();
                    JobPool.Rows[i].HeaderCell.Value = "P" + (i + 1).ToString();
                }
                JobPool.Invalidate();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string algorithm = AlorithmCombo.SelectedItem?.ToString();
            processList.Clear();
            resultList.Clear();

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
                resultList = scheduler.Run(processList, out avgWaitTime, out avgTurnaroundTime);

                //string result = "";
                //foreach (var p in resultList)
                //{
                //    result += $"ID: {p.ID}, Arrival: {p.ArrivalTime}, Burst: {p.BurstTime}, " +
                //              $"Start: {p.StartTime}, Finish: {p.FinishTime}, " +
                //              $"Wait: {p.WaitTime}, Turnaround: {p.TurnaroundTime}\n";
                //}

                await DrawQueueAsync();
                await DrawGanttChart();
            }
        }

        private async Task DrawGanttChart()
        {
            if (resultList == null || resultList.Count == 0)
                return;

            Graphics g = panel2.CreateGraphics();
            g.Clear(panel2.BackColor);
            int unitWidth = 10; // Mỗi đơn vị thời gian = 20 pixel
            int height = 70;
            int x = 50;
            int y = panel2.Height / 3;
            int spacing = 1;

            foreach (var process in resultList)
            {
                for (int i = 0; i < process.BurstTime; i++)
                {
                    // Chọn màu theo ID (tuỳ chỉnh)
                    Color color = Color.Red;
                    if (process.ID == 2) color = Color.DarkGray;
                    if (process.ID == 3) color = Color.Black;
                    if (process.ID == 4) color = Color.BlueViolet;
                    if (process.ID == 5) color = Color.Green;

                    Brush brush = new SolidBrush(color);
                    Rectangle rect = new Rectangle(x, y, unitWidth, height);
                    g.FillRectangle(brush, rect);

                    // Vẽ ID tiến trình
                    g.DrawString("P" + process.ID.ToString(), this.Font, Brushes.White, x + 3, 15);
                    x += unitWidth + spacing;
                }
            }
        }
        private async Task DrawQueueAsync()
        {
            if (resultList == null || resultList.Count == 0)
                return;

            Graphics g = panel7.CreateGraphics();
            g.Clear(panel7.BackColor);
            int unitWidth = 10; // Mỗi đơn vị thời gian = 10 pixel
            int height = 50;
            int x = 50;
            int y = panel7.Height / 3;
            int spacing = 1;

            foreach (var process in resultList)
            {
                for (int i = 0; i < process.BurstTime; i++)
                {
                    // Chọn màu theo ID
                    Color color = Color.Red;
                    if (process.ID == 2) color = Color.DarkGray;
                    if (process.ID == 3) color = Color.Black;
                    if (process.ID == 4) color = Color.BlueViolet;
                    if (process.ID == 5) color = Color.Green;

                    Brush brush = new SolidBrush(color);
                    Rectangle rect = new Rectangle(x, y, unitWidth, height);
                    g.FillRectangle(brush, rect);

                    // Vẽ ID tiến trình
                    g.DrawString("P" + process.ID.ToString(), this.Font, Brushes.White, x + 2, y + 15);

                    x += unitWidth + spacing;
                    await Task.Delay(150);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AlorithmCombo.SelectedIndex = 0; // Chọn thuật toán đầu tiên
        }
    }
}
