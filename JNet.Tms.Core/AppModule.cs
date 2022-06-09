using System;
using System.Collections.Generic;

namespace JNet
{
    public sealed class AppModule
    {
        public IList<Type> EntityTypes { get; }

        public AppModule(IList<Type> entityTypes)
        {
            EntityTypes = entityTypes;
        }

        public static bool IsEntity(Type type)
        {
            return type.IsPublic &&
                   !type.IsAbstract &&
                   !type.ContainsGenericParameters &&
                   typeof(IEntity).IsAssignableFrom(type);
        }
    }
}
