using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AhpDemo
{
    public static class ExpressionUtils
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return GetPropertyName((LambdaExpression)propertyExpression);
        }

        public static string GetPropertyName<T1, T2>(Expression<Func<T1, T2>> propertyExpression)
        {
            return GetPropertyName((LambdaExpression)propertyExpression);
        }

        private static string GetPropertyName(LambdaExpression propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            if (propertyExpression.Body is MemberExpression)
            {
                var property = ((MemberExpression)propertyExpression.Body).Member as PropertyInfo;
                if (property == null)
                {
                    throw new ArgumentException("Argument is not a property", "propertyExpression");
                }

                return property.Name;
            }
            else if (propertyExpression.Body is UnaryExpression) //in case of extra System.Object convert operation
            {
                var body = ((UnaryExpression)propertyExpression.Body).Operand as MemberExpression;
                if (body == null)
                {
                    throw new ArgumentException("Invalid argument", "propertyExpression");
                }

                var property = body.Member as PropertyInfo;
                if (property == null)
                {
                    throw new ArgumentException("Argument is not a property", "propertyExpression");
                }

                return property.Name;
            }
            else
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }
        }
    }
}
