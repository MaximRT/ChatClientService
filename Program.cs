using ChatClientService.Handlers;
using ChatClientService.Interfaces;
using ChatClientService.Realizations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IHubConnectionFactory, HubConnectionFactory>();
        services.AddTransient<IChatClient, ChatClient>();
        services.AddTransient<IMessageHandler, MessageHandler>();;
    })
    .Build();

Console.Write("Enter your name in Chat: ");

var name = "";

while (string.IsNullOrWhiteSpace(name))
{
    name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(name))
        Console.WriteLine("Enter your name in Chat: ");
}

Console.WriteLine("Ok. Now you can write your messages in chat. Type /exit to quit.");

var serviceProvider = host.Services;
var chatClient = serviceProvider.GetRequiredService<IChatClient>();

while (true)
{
    var message = await Task.Run(() => Console.ReadLine());
    
    if (string.IsNullOrWhiteSpace(message))
        continue;
    
    if (message.Trim().Equals("/exit", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Exiting chat...");
        break;
    }
    
    try
    {
        await chatClient.SendMessageAsync(name, message);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error sending message: {ex.Message}");
    }
}