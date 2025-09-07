using ChatClient.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatClient.Realizations;

public class HubConnectionFactory : IHubConnectionFactory
{
    public HubConnection CreateConnection()
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5134/publicChat")
            .Build();
        
        return connection;
    }
}