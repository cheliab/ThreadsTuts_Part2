namespace Threading_Albahari_Part2
{
    public class ProgressStatus
    {
        public readonly int PercentComplete;
        public readonly string StatusMessage;

        public ProgressStatus(int percentComplete, string statusMessage)
        {
            PercentComplete = percentComplete;
            StatusMessage = statusMessage;
        }
    }

    public class ThreadSafety_03_ImmutableObjects
    {
        private readonly object _statusLocker = new object();
        private ProgressStatus _status;

        public static void Start()
        {
            var status = new ProgressStatus(50, "");
        }
    }
}