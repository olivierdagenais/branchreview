using System;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.History
{
    public interface IHistoryContainer
    {
        event EventHandler Navigated;
        void Push(IHistoryItem historyItem);
        bool CanGoBack { get; }
        void GoBack();
        bool CanGoForward { get; }
        void GoForward();
        IHistoryItem Current { get; }
    }
}
