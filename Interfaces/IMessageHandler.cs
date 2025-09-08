namespace ChatClientService.Interfaces;

public interface IMessageHandler
{
    Task HandleMessage(string name, string message);
    Task HandleConnectionClosed(Exception? exception);
    Task HandleReconnecting(Exception? exception);
    Task HandleReconnected(string connectionId);
}