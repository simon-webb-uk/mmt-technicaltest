using Mmt.TechnicalTest.Core.Patterns.Operations.Enum;

namespace Mmt.TechnicalTest.Core.Patterns.Operations.Interfaces
{
    public interface IOperationResult
    {
        /// <summary>
        /// Gets or sets a value indicating the overall status of the request after any processing has been finished.
        /// Typically used by clients for flow control.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        OperationStatus Result { get; }

        /// <summary>
        /// Gets or sets a general message giving more information on the result of the operation.
        /// MUST NOT be used for flow control. Use for internal/debugging information only.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; }

        /// <summary>
        /// Gets or sets a general message giving more information on the error of the operation.
        /// MUST NOT be used for flow control. Use for internal/debugging information only.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string ErrorMessage { get; }
    }
}