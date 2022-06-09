using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace JNet.Tms.Mvc.Formatters
{
    /// <summary>
    /// A <see cref="TextInputFormatter"/> for JSON content that uses <see cref="JsonSerializer"/>.
    /// </summary>
    public class SystemTextJsonInputFormatterWrapper : IInputFormatter
    {
        private readonly SystemTextJsonInputFormatter _formmater;

        public SystemTextJsonInputFormatterWrapper(SystemTextJsonInputFormatter formatter)
        {
            _formmater = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        public bool CanRead(InputFormatterContext context) => _formmater.CanRead(context);

        public async Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            var result = await _formmater.ReadAsync(context);
            return result;
        }
    }
}
