using JNet;
using System;
using System.ComponentModel;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders
{
    public static class DimensionPropertyBuilderExtensions
    {
        public static PropertyBuilder<Dimension<T>> IsDimension<T>(this PropertyBuilder<Dimension<T>> builder) where T : struct, IComparable
        {
            builder.HasConversion(p => p.ToString(), p => TypeDescriptor.GetConverter(typeof(Dimension<T>)).ConvertFrom(p) as Dimension<T>);
            return builder;
        }
    }
}
