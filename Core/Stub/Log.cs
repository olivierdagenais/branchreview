namespace SoftwareNinjas.BranchAndReviewTools.Core.Stub
{
    /// <summary>
    /// An implementation of <see cref="ILog"/> that does nothing useful.
    /// </summary>
    public class Log : ILog
    {
        /// <summary>
        /// Reports status with the specified <paramref name="message"/>.
        /// </summary>
        /// 
        /// <param name="message">
        /// The status to show the user.
        /// </param>
        public virtual void Info(string message)
        {
        }

        /// <summary>
        /// Reports status with the specified <paramref name="message"/>
        /// and progress with the specified <paramref name="progressValue"/> and <paramref name="progressMaximum"/>.
        /// </summary>
        /// 
        /// <param name="message">
        /// The status to show the user.
        /// </param>
        /// 
        /// <param name="progressValue">
        /// An integer representing the progress toward <paramref name="progressMaximum"/>.
        /// </param>
        /// 
        /// <param name="progressMaximum">
        /// An integer representing the maximum value of <paramref name="progressValue"/>.
        /// </param>
        public virtual void Info(string message, int progressValue, int progressMaximum)
        {
        }

        /// <summary>
        /// Reports a warning with the specified <paramref name="message"/>.
        /// </summary>
        /// 
        /// <param name="message">
        /// The warning to show the user.
        /// </param>
        public virtual void Warning(string message)
        {
        }

        /// <summary>
        /// Reports an error with the specified <paramref name="message"/>.
        /// </summary>
        /// 
        /// <param name="message">
        /// The error to show the user.
        /// </param>
        public virtual void Error(string message)
        {
        }
    }
}
