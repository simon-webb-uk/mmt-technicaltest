namespace Mmt.TechnicalTest.Core.Patterns.Operations.Interfaces
{
    /// <summary>
    /// Defines a response message for operations that return a data set.
    /// </summary>
    /// <typeparam name="TDataModelType">The type of the data model type.</typeparam>
    public interface IDataOperationResult<TDataModelType> : IOperationResult
    {
        /// <summary>
        /// Gets information returned by calling this operation.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        TDataModelType Data { get; }
    }
}