using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Handler;

/// <summary>
/// 
/// Каждая команда может взаимодействовать с контекстом ввода.
/// Все методы ДОЛЖНЫ БЫТЬ асинхронными.
/// Вывод через бота осуществляется с помощью асинхронного метода
/// ReplyAsync(метод, унаследованный от ModuleBase.
/// 
/// </summary>
public class Echo : ModuleBase<SocketCommandContext>
{
    [Command("echo", true)]
    private async Task EchoAsync()
    {
        var message = Context.Message.Content.Remove(0, 5);

        await Context.Channel.SendMessageAsync(message);
    }
}