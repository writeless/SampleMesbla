using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Writeless.Extensions.AutoMapper
{
    public static class MappingExtensions
    {
        public static IEnumerable<PropertyInfo> GetPropertiesWithInaccessibleSetter(this Type type)
        {
            return type.GetProperties()
                    .Where(p => {
                        var setMethod = p.GetSetMethod(true);
                        return setMethod == null
                            || setMethod.IsPrivate
                            || setMethod.IsFamily;
                    });
        }

        public static void IgnoreAllPropertiesWithAnInaccessibleSetter(this IMappingExpression map, TypeMap type)
        {
            var properties = type.DestinationType.GetPropertiesWithInaccessibleSetter();
            foreach (var property in properties)
                map.ForMember(property.Name, opt => opt.Ignore());

            properties = type.SourceType.GetPropertiesWithInaccessibleSetter();
            foreach (var property in properties)
                map.ForSourceMember(property.Name, opt => opt.Ignore());
        }
    }
}
