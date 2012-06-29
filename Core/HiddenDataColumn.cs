using System;
using System.Data;

namespace SoftwareNinjas.BranchAndReviewTools.Core
{
    /// <summary>
    /// Represents the schema of a non-visible column in a <see cref="T:System.Data.DataTable"/>.
    /// </summary>
    public class HiddenDataColumn : DataColumn
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="HiddenDataColumn"/> class as type string.
        /// </summary>
        public HiddenDataColumn()
            : base()
        {
            this.ExtendedProperties.Add("Visible", false);
        }

        /// <summary>
        /// Inititalizes a new instance of the <see cref="HiddenDataColumn"/> class, as type string,
        /// using the specified column name.
        /// </summary>
        /// <param name="columnName">
        /// A string that represents the name of the column to be created. If set to null or an empty string (""),
        /// a default name will be specified when added to the columns collection.
        /// </param>
        public HiddenDataColumn(string columnName) 
            : base(columnName)
        {
            this.ExtendedProperties.Add("Visible", false);
        }

        /// <summary>
        /// Inititalizes a new instance of the <see cref="HiddenDataColumn"/> class 
        /// using the specified column name and data type.
        /// </summary>
        /// <param name="columnName">
        /// A string that represents the name of the column to be created. If set to null or an empty string (""), 
        /// a default name will be specified when added to the columns collection.
        /// </param>
        /// <param name="dataType">
        /// A supported <see cref="P:System.Data.DataColumn.DataType"/>.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// No <paramref name="dataType"/> was specified.
        /// </exception>
        public HiddenDataColumn(string columnName, Type dataType)
            : base(columnName, dataType)
        {
            this.ExtendedProperties.Add("Visible", false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HiddenDataColumn"/> class
        /// using the specified name, data type, and expression.
        /// </summary>
        /// <param name="columnName">
        /// A string that represents the name of the column to be created. If set to null or an empty string (""), 
        /// a default name will be specified when added to the columns collection.
        /// </param>
        /// <param name="dataType">
        /// A supported <see cref="P:System.Data.DataColumn.DataType"/>.
        /// </param>
        /// <param name="expr">
        /// The expression used to create this column.
        /// For more information, see the <see cref="P:System.Data.DataColumn.Expression"/> property.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// No <paramref name="dataType"/> was specified.
        /// </exception>
        public HiddenDataColumn(string columnName, Type dataType, string expr)
            : base(columnName, dataType, expr)
        {
            this.ExtendedProperties.Add("Visible", false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HiddenDataColumn"/> class
        /// using the specified name, data type, expression, and value that determines whether the column is an attribute.
        /// </summary>
        /// <param name="columnName">
        /// A string that represents the name of the column to be created. If set to null or an empty string (""), 
        /// a default name will be specified when added to the columns collection.
        /// </param>
        /// <param name="dataType">
        /// A supported <see cref="P:System.Data.DataColumn.DataType"/>.
        /// </param>
        /// <param name="expr">
        /// The expression used to create this column.
        /// For more information, see the <see cref="P:System.Data.DataColumn.Expression"/> property.
        /// </param>
        /// <param name="type">
        /// One of the <see cref="T:System.Data.MappingType"/> values.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// No <paramref name="dataType"/> was specified.
        /// </exception>
        public HiddenDataColumn(string columnName, Type dataType, string expr, MappingType type)
            : base(columnName, dataType, expr, type)
        {
            this.ExtendedProperties.Add("Visible", false);
        }
    }
}
