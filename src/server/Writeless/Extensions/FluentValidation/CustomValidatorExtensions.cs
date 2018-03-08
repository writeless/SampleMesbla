using FluentValidation;
using FluentValidation.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Writeless.Extensions.FluentValidation
{
    //https://laravel.com/docs/5.6/validation
    //http://regexlib.com/Search.aspx
    public static class CustomValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> Alphanumeric<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches(new Regex("^[a-zA-Z0-9]*$")).WithMessage("not_alphanumeric");
        }

        public static IRuleBuilderOptions<T, TProperty> Unique<T, TEntity, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, DbSet<TEntity> dbset)
            where TEntity : class
        {
            return ruleBuilder.MustAsync(async (e, cancellationToken) =>
            {
                var rule = ruleBuilder as RuleBuilder<T, TProperty>;

                ParameterExpression argParam = Expression.Parameter(typeof(TEntity), "e");
                Expression nameProperty = Expression.Property(argParam, rule.Rule.PropertyName);

                var valueToCompare = Expression.Constant(e);
                Expression expression = Expression.Equal(nameProperty, valueToCompare);

                var predicate = Expression.Lambda<Func<TEntity, bool>>(expression, argParam);

                return !await dbset.AnyAsync(predicate);
            });
        }
    }
}
