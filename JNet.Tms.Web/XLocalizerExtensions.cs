using Microsoft.Extensions.DependencyInjection;
using XLocalizer;
using XLocalizer.ErrorMessages;

namespace JNet.Tms
{
    internal static class XLocalizerExtensions
    {
        public static IMvcBuilder AddXLocalizer(this IMvcBuilder builder)
        {
            builder.AddXLocalizer<LocSource>(opts =>
            {
                opts.ValidationErrors = new ValidationErrors()
                {
                    RequiredAttribute_ValidationError = "请填写{0}",
                    MaxLengthAttribute_ValidationError = "{0}必须在{1}个字内",
                    StringLengthAttribute_ValidationError = "{0}必须在{1}个字内",
                    StringLengthAttribute_ValidationErrorIncludingMinimum = "{0}必须为{2}-{1}个字",
                    RangeAttribute_ValidationError = "无效的{0}",
                    RegexAttribute_ValidationError = "{0}格式不正确"
                };

                opts.ModelBindingErrors = new ModelBindingErrors()
                {
                    AttemptedValueIsInvalidAccessor = "{1}的值'{0}'无效",
                    ValueIsInvalidAccessor = "输入值'{0}'无效",
                    ValueMustNotBeNullAccessor = "未输入有效值",
                    UnknownValueIsInvalidAccessor = "'{0}'无效值",
                    MissingBindRequiredValueAccessor = "这是必须的",
                };
            });

            return builder;
        }

        private class LocSource { }
    }
}
