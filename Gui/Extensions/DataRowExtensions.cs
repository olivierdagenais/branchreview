using System;
using System.Collections.Generic;
using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    public static class DataRowExtensions
    {
        public static bool Matches(this DataRow row, IList<Type> columnTypes, IList<bool> columnSearchable, IEnumerable<FilterChunk> filterChunks)
        {
            foreach (var chunk in filterChunks)
            {
                var chunkMatchesAtLeastOneColumn = Matches(row, columnTypes, columnSearchable, chunk);

                if (!chunkMatchesAtLeastOneColumn) return false;
            }

            return true;
        }

        public static bool Matches(this DataRow row, IList<Type> columnTypes, IList<bool> columnSearchable, FilterChunk chunk)
        {
            var chunkMatchesAtLeastOneColumn = false;
            for (var c = 0; c < columnTypes.Count; c++)
            {
                if (!columnSearchable[c])
                {
                    continue;
                }
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

        public static bool PointsToSameData(this DataRow a, DataRow b)
        {
            return PointsToSameData(a.ItemArray, b.ItemArray);
        }

        internal static bool PointsToSameData(object[] aItems, object[] bItems)
        {
            if (aItems.Length != bItems.Length)
            {
                return false;
            }
            var result = true;
            for (var i = 0; i < aItems.Length; i++)
            {
                if (!Equals(aItems[i], bItems[i]))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
