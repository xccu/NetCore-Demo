namespace Client_Demo.Operation
{
    public class OperationScoped : IOperationScoped
    {
        public string OperationId { get; } = Guid.NewGuid().ToString()[^4..];
    }
}
