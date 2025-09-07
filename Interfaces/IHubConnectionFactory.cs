using Microsoft.AspNetCore.SignalR.Client;

namespace ChatClient.Interfaces;

public interface IHubConnectionFactory
{
    HubConnection CreateConnection();
}