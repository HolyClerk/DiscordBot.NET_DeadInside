using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

using DiscordBot.BotLogger;

namespace DiscordBot.CoreDuo;

internal class Core
{
    private Logger _logger;
    private DiscordSocketClient _client;
    private CommandService _commands;
    private IServiceProvider _services;

    public Core()
    {
        _logger = new Logger();
        _client = new DiscordSocketClient();
        _commands = new CommandService();

        _services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .BuildServiceProvider();

        _client.Log += _logger.OnLog;
        _client.MessageReceived += CommandHandleAsync;
    }

    public async Task RunBotAsync()
    {
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        // AskToToken запрашивает токен в консоли. Можно безопаснее, но зачем?
        try
        {
            await _client.LoginAsync(TokenType.Bot, new BotToken().AskToToken());
            await _client.StartAsync();

            await Panel.StartAsync();

            await Task.Delay(-1);
        }
        catch (Exception e)
        {
            _logger.Write("Скорее всего вы ввели неверный токен, попробуйте еще раз.");
            _logger.Write(e.ToString());
        }
    }

    private async Task CommandHandleAsync(SocketMessage arg)
    {
        var message = (SocketUserMessage)arg;

        var context = new SocketCommandContext(_client, message);

        if (message.Author.IsBot)
        {
            return;
        }

        var argPos = 0;

        if (message.HasStringPrefix("!", ref argPos))
        {
            var result = await _commands.ExecuteAsync(context, argPos, _services);

            if (!result.IsSuccess)
            {
                _logger.Write(result.ErrorReason);
            }
                
            if (result.Error.Equals(CommandError.UnmetPrecondition))
            {
                await message.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}