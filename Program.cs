using ChatClientService.Interfaces;
using ChatClientService.Realizations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IHubConnectionFactory, HubConnectionFactory>();
        services.AddTransient<IChatClient, ChatClient>();
    });

Console.Write("Enter your name in Chat: ");

var name = Console.ReadLine();

Console.WriteLine("Ok. Now you can write your messages in chat");

while (true)
{
    var message = Console.ReadLine();
}