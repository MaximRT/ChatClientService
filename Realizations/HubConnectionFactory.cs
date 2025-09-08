using ChatClientService.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace ChatClientService.Realizations;

public class HubConnectionFactory : IHubConnectionFactory
{
    private readonly IConfiguration  _configuration;

    public HubConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public HubConnection CreateConnection()
    {
        var sd = _configuration["SignalR:PublicChatUrl"];
        
        var connection = new HubConnectionBuilder()
            .WithUrl(_configuration["SignalR:PublicChatUrl"])
            .Build();
        
        return connection;
    }
}