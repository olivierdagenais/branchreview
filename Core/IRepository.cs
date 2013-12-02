namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// Top-level interface to repositories.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Gets or sets the <see cref="ILog"/> instance used for reporting status and progress.
        /// </summary>
        ILog Log { get; set; }

        /// <summary>
        /// The name of the repository, useful for disambiguating two implementations.
        /// </summary>
        string Name { get; }
    }
}
