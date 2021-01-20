using Mmt.TechnicalTest.Core.Patterns.Operations.Enum;
using Mmt.TechnicalTest.Core.Patterns.Operations.Interfaces;

namespace Mmt.TechnicalTest.Core.Patterns.Operations
{
    public class OperationResult : IOperationResult
    {
        /// <inheritdoc/>
        public OperationStatus Result { get; set; } = OperationStatus.NoWorkDone;

        /// <inheritdoc/>
        public string Message { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string ErrorMessage { get; set; } = string.Empty;

        public static class WithData<TData>
        {
            public static IDataOperationResult<TData> Ok(TData data)
            {
                return new DataOperationResult<TData>
                {
                    Result = OperationStatus.Ok,
                    Data = data
                };
            }

            public static IDataOperationResult<TData> Failed
            {
                get
                {
                    return new DataOperationResult<TData>
                    {
                        Result = OperationStatus.Failed
                    };
                }
            }

            public static IDataOperationResult<TData> NoRecords
            {
                get
                {
                    return new DataOperationResult<TData>
                    {
                        Result = OperationStatus.NoRecords
                    };
                }
            }
        }

        public static IOperationResult Ok =>
            new OperationResult
            {
                Result = OperationStatus.Ok
            };

        public static IOperationResult Failed =>
            new OperationResult
            {
                Result = OperationStatus.Failed
            };

        public static IOperationResult NoRecords =>
            new OperationResult
            {
                Result = OperationStatus.NoRecords
            };
    }
}
