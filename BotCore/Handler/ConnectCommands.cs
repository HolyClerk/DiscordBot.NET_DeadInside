using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;

using DiscordBot.CoreDuo;

namespace DiscordBot.Handler;

/// <summary>
/// 
/// Каждая команда может взаимодействовать с контекстом ввода.
/// Все методы ДОЛЖНЫ БЫТЬ асинхронными.
/// Вывод через бота осуществляется с помощью асинхронного метода
/// ReplyAsync(метод, унаследованный от ModuleBase.
/// 
/// </summary>
public class ConnectCommands : ModuleBase<SocketCommandContext>
{
    [Command("connect", true)]
    private async Task Connect()
    {
        if (Core.VoiceManager != null)
        {
            await Core.VoiceManager.ConnectVoiceAsync();
        }
    }

    [Command("disconnect", true)]
    private async Task Disconnect()
    {
        if (Core.VoiceManager != null)
        {
            await Core.VoiceManager.DisconnectVoiceAsync();
        }
    }
}