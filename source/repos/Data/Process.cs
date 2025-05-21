namespace CPUSchedulerProject.Data
{
    public class Process
    {
        public string Id { get; set; }
        public int ArrivalTime { get; set; }
        public int BurstTime { get; set; }
        public int Priority { get; set; }
        public int StartTime { get; set; }
        public int FinishTime { get; set; }
        public int RemainingTime { get; set; }
        public int WaitingTime => StartTime - ArrivalTime;
        public int TurnaroundTime => FinishTime - ArrivalTime;
    }
}
