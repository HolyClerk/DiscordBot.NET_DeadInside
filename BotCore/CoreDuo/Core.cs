using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

using DiscordBot.Log;
using DiscordBot.Services;
using Discord.Audio;

namespace DiscordBot.CoreDuo;

internal class Core
{
    private BotDebugger _logger;

    private DiscordSocketClient _client;
    private CommandService _commands;

    private IServiceProvider _services;

    public Core()
    {
        _logger = new BotDebugger();

        // Клиент и его команды.
        _client = new DiscordSocketClient();
        _commands = new CommandService();

        // Объявдение сервисов клиента и комманд как единственно-реализуемых(синглтон ебливый).
        _services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .BuildServiceProvider();

        _client.Log += _logger.OnLog;
        _client.MessageReceived += MessageReceived;

        CurrentAudioClient = new();
        CurrentConnection = new(_client);
    }

    public static AudioService CurrentAudioClient { get; set; }
    public static ConnectionService CurrentConnection { get; set; }

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
            BotDebugger.WriteErrorLine("Скорее всего вы ввели неверный токен, попробуйте еще раз.");
            BotDebugger.WriteErrorLine(e.ToString());
        }
    }
    
    /// <summary>
    /// Метод, срабатывающий при отправке пользователем к-либо сообщения.
    /// Проверяет является ли сообщение командой.
    /// </summary>
    /// <param name="arg">Проверяемое сообщение</param>
    /// <returns>Task</returns>
    private Task MessageReceived(SocketMessage arg)
    {
        _ = Task.Run(async () =>
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
                    BotDebugger.WriteErrorLine(result.ErrorReason);
                }

                if (result.Error.Equals(CommandError.UnmetPrecondition))
                {
                    await message.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        });

        return Task.CompletedTask;
    }
}