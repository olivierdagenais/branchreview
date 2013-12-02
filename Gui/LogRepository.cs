using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SoftwareNinjas.BranchAndReviewTools.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public class LogRepository : IBuildRepository
    {
        private readonly IList<StatusMessage> _messages;

        private readonly DataTable _logs = new DataTable
        {
            Columns =
            {
                new HiddenDataColumn("ID", typeof(int)),
                new DataColumn("Name", typeof(string)) { Caption = "Status" },
                new DataColumn("Progress", typeof(string)),
                new DataColumn("Date", typeof(DateTime)) { Caption = "Time Stamp" },
                {"Message", typeof(string)},
            },
        };


        public LogRepository(IList<StatusMessage> messages)
        {
            _messages = messages;
        }

        #region IBuildRepository Members

        string IRepository.Name { get { return "Log repository"; } }

        public ILog Log
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public DataTable LoadBuilds()
        {
            var result = _logs.Clone();
            lock (_messages)
            {
                var c = 0;
                foreach (var statusMessage in _messages)
                {
                    var progress = statusMessage.PrettyPrintProgress();
                    result.Rows.Add(
                        c,
                        statusMessage.StatusKind.ToString(),
                        progress,
                        statusMessage.TimeStamp,
                        statusMessage.Message
                    );
                    c++;
                }
            }
            return result;
        }

        public IList<MenuAction> GetBuildActions(object buildId)
        {
            return MenuAction.EmptyList;
        }

        public string GetBuildLog(object buildId)
        {
            StatusMessage message;
            lock (_messages)
            {
                message = _messages[(int) buildId];
            }
            var sb = new StringBuilder();
            sb.Append(message.StatusKind).Append(": ").Append(message.TimeStamp);
            var progress = message.PrettyPrintProgress();
            if (!String.IsNullOrEmpty(progress))
            {
                sb.Append(" (").Append(progress).Append(")");
            }
            sb.AppendLine();
            sb.Append(message.Message);
            var result = sb.ToString();
            return result;
        }

        #endregion
    }
}