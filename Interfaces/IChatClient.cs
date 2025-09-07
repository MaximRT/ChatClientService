namespace ChatClientService.Interfaces;

public interface IChatClient
{
    Task SendMessageAsync(string name, string message);
}