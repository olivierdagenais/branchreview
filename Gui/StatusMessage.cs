using System;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public class StatusMessage
    {
        private readonly StatusKind _statusKind;
        public StatusKind StatusKind { get { return _statusKind; } }

        private readonly string _message;
        public string Message { get { return _message; } }

        private readonly int? _progressValue;
        public int? ProgressValue { get { return _progressValue; } }

        private readonly int? _progressMaximumValue;
        public int? ProgressMaximumValue { get { return _progressMaximumValue; } }

        private readonly DateTime _timeStamp;
        public DateTime TimeStamp { get { return _timeStamp; } }

        public StatusMessage(StatusKind statusKind, string message)
            : this(statusKind, message, DateTime.Now)
        {
        }

        internal StatusMessage(StatusKind statusKind, string message, DateTime timeStamp)
        {
            _statusKind = statusKind;
            _message = message;
            _timeStamp = timeStamp;
        }

        public StatusMessage(StatusKind statusKind, string message, int progressValue, int progressMaximumValue)
            : this(statusKind, message, progressValue, progressMaximumValue, DateTime.Now)
        {
        }

        internal StatusMessage
            (StatusKind statusKind, string message, int progressValue, int progressMaximumValue, DateTime timeStamp)
        {
            _statusKind = statusKind;
            _message = message;
            _progressValue = progressValue;
            _progressMaximumValue = progressMaximumValue;
            _timeStamp = timeStamp;
        }

        public string PrettyPrintProgress()
        {
            var result = String.Empty;
            if (_progressValue.HasValue && _progressMaximumValue.HasValue && _progressMaximumValue.Value != 0)
            {
                result = "{0}%".FormatInvariant(100 * _progressValue.Value /_progressMaximumValue.Value);
            }
            return result;
        }
    }
}
