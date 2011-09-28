using System;
using System.Collections.Generic;
using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    public static class DataRowExtensions
    {
        public static bool Matches(this DataRow row, IList<Type> columnTypes, IEnumerable<FilterChunk> filterChunks)
        {
            foreach (var chunk in filterChunks)
            {
                var chunkMatchesAtLeastOneColumn = Matches(row, columnTypes, chunk);

                if (!chunkMatchesAtLeastOneColumn) return false;
            }

            return true;
        }

        public static bool Matches(this DataRow row, IList<Type> columnTypes, FilterChunk chunk)
        {
            var chunkMatchesAtLeastOneColumn = false;
            for (var c = 0; c < columnTypes.Count; c++)
            {
                var columnType = columnTypes[c];
                var cellValue = row[c];

                if (columnType == typeof(int))
                {
                    if (chunk.Integer.HasValue)
                    {
                        var v = (int) cellValue;
                        chunkMatchesAtLeastOneColumn = v == chunk.Integer.Value;
                    }
                }
                else
                {
                    if (cellValue != null)
                    {
                        var v = cellValue.ToString();
                        chunkMatchesAtLeastOneColumn = v.ContainsInvariantIgnoreCase(chunk.Text);
                    }
                }

                if (chunkMatchesAtLeastOneColumn)
                {
                    break;
                }
            }
            return chunkMatchesAtLeastOneColumn;
        }
    }
}
