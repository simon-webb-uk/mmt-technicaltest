using Mmt.TechnicalTest.Core.Patterns.Operations.Enum;
using Mmt.TechnicalTest.Core.Patterns.Operations.Interfaces;

namespace Mmt.TechnicalTest.Core.Patterns.Operations
{
    public class DataOperationResult<TDataModelType> : IDataOperationResult<TDataModelType>
    {

        /// <inheritdoc />
        public OperationStatus Result { get; set; } = OperationStatus.NoWorkDone;

        /// <inheritdoc />
        public string Message { get; set; } = string.Empty;

        /// <inheritdoc />
        public string ErrorMessage { get; set; } = string.Empty;

        /// <inheritdoc />
        public TDataModelType Data { get; set; }

    }
}