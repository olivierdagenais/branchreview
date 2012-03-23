namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    internal class StatusMessage
    {
        private readonly StatusKind _statusKind;
        public StatusKind StatusKind { get { return _statusKind; } }

        private readonly string _message;
        public string Message { get { return _message; } }

        private readonly int? _progressValue;
        public int? ProgressValue { get { return _progressValue; } }

        private readonly int? _progressMaximumValue;
        public int? ProgressMaximumValue { get { return _progressMaximumValue; } }

        public StatusMessage(StatusKind statusKind, string message)
        {
            _statusKind = statusKind;
            _message = message;
        }

        public StatusMessage(StatusKind statusKind, string message, int progressValue, int progressMaximumValue)
        {
            _statusKind = statusKind;
            _message = message;
            _progressValue = progressValue;
            _progressMaximumValue = progressMaximumValue;
        }
    }
}
