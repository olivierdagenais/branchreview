﻿using System;
using System.Data;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    public static class DataTableExtensions
    {
        public static DataRow FindFirst(this DataTable dataTable, string columnName, object needle)
        {
            var needleValue = needle.ToString();
            var result = FindFirstOrDefault(dataTable, columnName, needleValue);
            if (result == null)
            {
                throw new ArgumentException("Could not find {0}".FormatInvariant(needleValue));
            }
            return result;
        }

        public static DataRow FindFirstOrDefault(this DataTable dataTable, string columnName, string needleValue)
        {
            DataRow result = null;
            var dc = dataTable.Columns[columnName];
            var columnType = dc.DataType;
            var chunk = new FilterChunk(needleValue);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var chunkMatchesAtLeastOneColumn = false;
                var cellValue = dataRow[dc];
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
                        chunkMatchesAtLeastOneColumn = v == chunk.Text;
                    }
                }
                if (chunkMatchesAtLeastOneColumn)
                {
                    result = dataRow;
                    break;
                }
            }
            return result;
        }
    }
}
