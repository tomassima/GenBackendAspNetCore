using Microsoft.Extensions.Logging;

namespace UnitTests;

public class MockLogger<T> : ILogger<T>
{
    private readonly List<string> _logMessages = new();

    public IReadOnlyList<string> LogMessages => _logMessages;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        _logMessages.Add(formatter(state, exception));
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
}
