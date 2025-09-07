namespace ChatClient.Interfaces;

public interface IChatClient
{
    Task SendMessageAsync(string message);
}