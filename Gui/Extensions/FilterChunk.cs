using System;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    public struct FilterChunk
    {
        public string Text;
        public int? Integer;
        public FilterChunk(string text)
        {
            Text = text;
            int m;
            Integer = Int32.TryParse (text, out m) ? (int?) m : null;
        }
    }
}