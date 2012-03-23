using System;
using System.Linq.Expressions;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Extensions
{
    public static class ExpressionExtensions
    {
        /// <remarks>
        /// Derived from <see href="https://gist.github.com/1377167">Rename-proof argument validation in C#</see>.
        /// </remarks>
        public static string DetermineName<T>(this Expression<Func<T>> valueExpression)
        {
            var expression = valueExpression.Body;
            if (expression.NodeType == ExpressionType.Convert)
            {
                // "A cast or conversion operation"
                var unaryExpression = (UnaryExpression) expression;
                expression = unaryExpression.Operand;
            }
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    var memberExpression = (MemberExpression) expression;
                    var name = memberExpression.Member.Name;
                    return name;
                default:
                    throw new NotImplementedException(
                        "Invalid use of valueExpression:  Only member access is supported.  "
                        + "Use an expression that looks like '() => member'");
            }
        }
    }
}
