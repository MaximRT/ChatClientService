using ChatClientService.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatClientService.Realizations;

public class ChatClient : IChatClient
{
    private readonly IHubConnectionFactory _hubConnectionFactory;
    
    private readonly HubConnection  _hubConnection;
    public ChatClient(IHubConnectionFactory hubConnectionFactory)
    {
        _hubConnectionFactory = hubConnectionFactory;
        _hubConnection = _hubConnectionFactory.CreateConnection();
    }

    public async Task SendMessageAsync(string name, string message)
    {
        await _hubConnection.InvokeAsync("SendMessage", name, message);
    }

    private async Task ConnectAsync()
    {
        await _hubConnection.StartAsync();
        
        _hubConnection.On<string, string>("ReceiveMessage", (name, message) =>
        {
            Console.WriteLine($"{name}: {message}");
        });
        
        _hubConnection.Closed += async exception =>
        {
            if (exception is null)
            {
                Console.WriteLine("Connection closed without error.");
            }
            else
            {
                Console.WriteLine($"Connection closed due to an error: {exception}");
            }

            await Task.CompletedTask;
        };

        _hubConnection.Reconnecting += async exception =>
        {
            Console.WriteLine($"Connection started reconnecting due to an error: {exception}");
            await Task.CompletedTask;
        };

        _hubConnection.Reconnected += async connectionId =>
        {
            Console.WriteLine($"Connection successfully reconnected. The ConnectionId is now: {connectionId}");
            await Task.CompletedTask;
        };
    }
}