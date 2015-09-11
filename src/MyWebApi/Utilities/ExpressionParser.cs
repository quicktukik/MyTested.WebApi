﻿// MyWebApi - ASP.NET Web API Fluent Testing Framework
// Copyright (C) 2015 Ivaylo Kenov.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.

namespace MyWebApi.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Common;

    /// <summary>
    /// Utility class helping parsing expression trees.
    /// </summary>
    public static class ExpressionParser
    {
        /// <summary>
        /// Parses method name from method call lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Method name as string.</returns>
        public static string GetMethodName(LambdaExpression expression)
        {
            var methodCallExpression = GetMethodCallExpresstion(expression);
            return methodCallExpression.Method.Name;
        }

        /// <summary>
        /// Resolves arguments from method in method call lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Collection of type-value pairs.</returns>
        public static IEnumerable<TypeValuePair> ResolveMethodArguments(LambdaExpression expression)
        {
            var methodCallExpression = GetMethodCallExpresstion(expression);
            return methodCallExpression.Arguments
                .Select(argument => Expression.Lambda(argument).Compile().DynamicInvoke())
                .Select(value => new TypeValuePair
                {
                    Type = value.GetType(),
                    Value = value
                })
                .ToList();
        }

        /// <summary>
        /// Parses member name from member lambda expression.
        /// </summary>
        /// <param name="expression">Expression to be parsed.</param>
        /// <returns>Member name as string.</returns>
        public static string GetPropertyName(LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Provided expression is not a valid member expression.");
            }

            return memberExpression.Member.Name;
        }

        private static MethodCallExpression GetMethodCallExpresstion(LambdaExpression expression)
        {
            var methodCallExpression = expression.Body as MethodCallExpression;
            if (methodCallExpression == null)
            {
                throw new ArgumentException("Provided expression is not a valid method call.");
            }

            return methodCallExpression;
        }
    }
}