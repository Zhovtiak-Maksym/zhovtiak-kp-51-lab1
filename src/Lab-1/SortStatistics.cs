namespace Lab_1
{
    public class SortStatistics
    {
        public int Comparisons { get; set; }
        public int SwapsOrCopies { get; set; }
        public int RecursiveCalls { get; set; }
        public int Passes { get; set; }
        public TimeSpan ExecutionTime { get; set; }
    }
}
