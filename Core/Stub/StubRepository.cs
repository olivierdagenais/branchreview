namespace SoftwareNinjas.BranchAndReviewTools.Core.Stub
{
    /// <summary>
    /// Base class for implementations of <see cref="IRepository"/>-derived interfaces.
    /// </summary>
    public abstract class StubRepository : IRepository
    {
        /// <summary>
        /// Gets or sets the name of the repository.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ILog"/> instance used for reporting status and progress.
        /// </summary>
        public virtual ILog Log { get; set; }
    }
}
