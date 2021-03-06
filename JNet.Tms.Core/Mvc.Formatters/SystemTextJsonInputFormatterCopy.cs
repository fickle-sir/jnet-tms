using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JNet.Tms.Mvc.Formatters
{
    /// <summary>
    /// A <see cref="TextInputFormatter"/> for JSON content that uses <see cref="JsonSerializer"/>.
    /// </summary>
    public class SystemTextJsonInputFormatterCopy : TextInputFormatter, IInputFormatterExceptionPolicy
    {
        private readonly ILogger<SystemTextJsonInputFormatter> _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="SystemTextJsonInputFormatter"/>.
        /// </summary>
        /// <param name="options">The <see cref="JsonOptions"/>.</param>
        /// <param name="logger">The <see cref="ILogger"/>.</param>
        public SystemTextJsonInputFormatterCopy(
            JsonOptions options,
            ILogger<SystemTextJsonInputFormatter> logger)
        {
            SerializerOptions = options.SerializerOptions;
            _logger = logger;

            SupportedEncodings.Add(UTF8EncodingWithoutBOM);
            SupportedEncodings.Add(UTF16EncodingLittleEndian);

            SupportedMediaTypes.Add(MediaTypeHeaderValues.ApplicationJson);
            SupportedMediaTypes.Add(MediaTypeHeaderValues.TextJson);
            SupportedMediaTypes.Add(MediaTypeHeaderValues.ApplicationAnyJsonSyntax);
        }

        /// <summary>
        /// Gets the <see cref="JsonSerializerOptions"/> used to configure the <see cref="JsonSerializer"/>.
        /// </summary>
        /// <remarks>
        /// A single instance of <see cref="SystemTextJsonInputFormatter"/> is used for all JSON formatting. Any
        /// changes to the options will affect all input formatting.
        /// </remarks>
        public JsonSerializerOptions SerializerOptions { get; }

        /// <inheritdoc />
        InputFormatterExceptionPolicy IInputFormatterExceptionPolicy.ExceptionPolicy => InputFormatterExceptionPolicy.MalformedInputExceptions;

        /// <inheritdoc />
        public sealed override async Task<InputFormatterResult> ReadRequestBodyAsync(
            InputFormatterContext context,
            Encoding encoding)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            var httpContext = context.HttpContext;
            var (inputStream, usesTranscodingStream) = GetInputStream(httpContext, encoding);

            object model;
            try
            {
                model = await JsonSerializer.DeserializeAsync(inputStream, context.ModelType, SerializerOptions);
            }
            catch (JsonException jsonException)
            {
                var path = jsonException.Path;

                var formatterException = new InputFormatterException(jsonException.Message, jsonException);

                context.ModelState.TryAddModelError(path, formatterException, context.Metadata);

                Log.JsonInputException(_logger, jsonException);

                return InputFormatterResult.Failure();
            }
            catch (Exception exception) when (exception is FormatException || exception is OverflowException)
            {
                // The code in System.Text.Json never throws these exceptions. However a custom converter could produce these errors for instance when
                // parsing a value. These error messages are considered safe to report to users using ModelState.

                context.ModelState.TryAddModelError(string.Empty, exception, context.Metadata);
                Log.JsonInputException(_logger, exception);

                return InputFormatterResult.Failure();
            }
            finally
            {
                if (usesTranscodingStream)
                {
                    await inputStream.DisposeAsync();
                }
            }

            if (model == null && !context.TreatEmptyInputAsDefaultValue)
            {
                // Some nonempty inputs might deserialize as null, for example whitespace,
                // or the JSON-encoded value "null". The upstream BodyModelBinder needs to
                // be notified that we don't regard this as a real input so it can register
                // a model binding error.
                return InputFormatterResult.NoValue();
            }
            else
            {
                Log.JsonInputSuccess(_logger, context.ModelType);
                return InputFormatterResult.Success(model);
            }
        }

        private static (Stream inputStream, bool usesTranscodingStream) GetInputStream(HttpContext httpContext, Encoding encoding)
        {
            if (encoding.CodePage == Encoding.UTF8.CodePage)
            {
                return (httpContext.Request.Body, false);
            }

            var inputStream = Encoding.CreateTranscodingStream(httpContext.Request.Body, encoding, Encoding.UTF8, leaveOpen: true);
            return (inputStream, true);
        }

        private static class Log
        {
            private static readonly Action<ILogger, string, Exception> _jsonInputFormatterException;
            private static readonly Action<ILogger, string, Exception> _jsonInputSuccess;

            static Log()
            {
                _jsonInputFormatterException = LoggerMessage.Define<string>(
                    LogLevel.Debug,
                    new EventId(1, "SystemTextJsonInputException"),
                    "JSON input formatter threw an exception: {Message}");
                _jsonInputSuccess = LoggerMessage.Define<string>(
                    LogLevel.Debug,
                    new EventId(2, "SystemTextJsonInputSuccess"),
                    "JSON input formatter succeeded, deserializing to type '{TypeName}'");
            }

            public static void JsonInputException(ILogger logger, Exception exception)
                => _jsonInputFormatterException(logger, exception.Message, exception);

            public static void JsonInputSuccess(ILogger logger, Type modelType)
                => _jsonInputSuccess(logger, modelType.FullName, null);
        }
    }
}
