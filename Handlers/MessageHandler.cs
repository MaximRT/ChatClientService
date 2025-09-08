using ChatClientService.Interfaces;

namespace ChatClientService.Handlers;

public class MessageHandler : IMessageHandler
{
    public Task HandleMessage(string name, string message)
    {
        Console.WriteLine($"{name}: {message}");
        
        return Task.CompletedTask;
    }

    public Task HandleConnectionClosed(Exception? exception)
    {
        Console.WriteLine(exception is null
            ? "Connection closed without error."
            : $"Connection closed due to an error: {exception}");

        return Task.CompletedTask;
    }

    public Task HandleReconnecting(Exception? exception)
    {
        Console.WriteLine($"Connection started reconnecting due to an error: {exception}");
        return Task.CompletedTask;
    }

    public Task HandleReconnected(string connectionId)
    {
        Console.WriteLine($"Connection successfully reconnected. The ConnectionId is now: {connectionId}");
        return Task.CompletedTask;
    }
}