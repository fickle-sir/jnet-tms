namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// JNetValidationAttribute:DimensionAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class DimensionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            var type = value.GetType();
            var gType = TypeDescriptor.GetConverter(type.GetGenericArguments()[0]);
            var objMin = gType.ConvertFrom("0");

            var length = type.GetProperty("Length").GetValue(value);
            var width = type.GetProperty("Width").GetValue(value);
            var height = type.GetProperty("Height").GetValue(value);
            return Compare(length, objMin) && Compare(width, objMin) && Compare(height, objMin);
        }

        public override string FormatErrorMessage(string name) => $"{name}格式有误";

        private static bool Compare(object value, object target) => ((IComparable)value).CompareTo(target) > 0;
    }
}
