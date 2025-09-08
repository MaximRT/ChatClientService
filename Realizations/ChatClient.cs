using ChatClientService.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatClientService.Realizations;

public class ChatClient : IChatClient
{
    private readonly IMessageHandler _messageHandler;
    private readonly HubConnection  _hubConnection;
    private Task? _connectTask;
    
    
    public ChatClient(IHubConnectionFactory hubConnectionFactory, IMessageHandler messageHandler)
    {
        _messageHandler = messageHandler;
        _hubConnection = hubConnectionFactory.CreateConnection();
    }

    public async Task SendMessageAsync(string name, string message)
    {
        await ConnectAsync();
        await _hubConnection.InvokeAsync("SendMessage", name, message);
    }

    private async Task ConnectAsync()
    {
        _connectTask ??= InternalConnectAsync();

        await _connectTask;
    }

    private async Task InternalConnectAsync()
    {
        await _hubConnection.StartAsync();
        
        _hubConnection.On<string, string>("ReceiveMessage",  _messageHandler.HandleMessage);
        
        _hubConnection.Closed += async exception => await _messageHandler.HandleConnectionClosed(exception);

        _hubConnection.Reconnecting += async exception => await _messageHandler.HandleReconnecting(exception);

        _hubConnection.Reconnected += async connectionId => await _messageHandler.HandleReconnected(connectionId);
    }
}