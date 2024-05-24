namespace Integration.Common;

public sealed class Result
{
    public bool Success { get; private set; }
    public string Message { get; private set; }

    public Result(bool success, string message)
    {
        #if DEBUG
        // To debug the result of the operations.
        Console.WriteLine($"[{success}] {message}");
        #endif

        Success = success;
        Message = message;
    }
}