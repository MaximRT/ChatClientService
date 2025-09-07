using Microsoft.AspNetCore.SignalR.Client;

namespace ChatClientService.Interfaces;

public interface IHubConnectionFactory
{
    HubConnection CreateConnection();
}