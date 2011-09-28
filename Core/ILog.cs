namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// Represents the mechanism used for reporting status to the host.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Reports status with the specified <paramref name="message"/>.
        /// </summary>
        /// 
        /// <param name="message">
        /// The status to show the user.
        /// </param>
        void Info(string message);

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
        void Info(string message, int progressValue, int progressMaximum);

        /// <summary>
        /// Reports a warning with the specified <paramref name="message"/>.
        /// </summary>
        /// 
        /// <param name="message">
        /// The warning to show the user.
        /// </param>
        void Warning(string message);

        /// <summary>
        /// Reports an error with the specified <paramref name="message"/>.
        /// </summary>
        /// 
        /// <param name="message">
        /// The error to show the user.
        /// </param>
        void Error(string message);
    }
}
