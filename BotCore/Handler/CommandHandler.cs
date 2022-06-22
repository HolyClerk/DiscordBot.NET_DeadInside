using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Handler;

/// <summary>
/// 
/// Класс-обработчик. 
/// Каждая команда может взаимодействовать с контекстом ввода.
/// Все методы ДОЛЖНЫ БЫТЬ асинхронными.
/// Вывод через бота осуществляется с помощью асинхронного метода
/// ReplyAsync(метод, унаследованный от ModuleBase.
/// 
/// </summary>
public class CommandHandler : ModuleBase<SocketCommandContext>
{
    private List<SocketUser> _usersList = new();

    #region Команды бота
    [Command("echo", true)]
    private async Task Echo()
    {
        _usersList.Add(Context.User);

        var message = Context.Message.Content;
        message = message.Remove(0, 5);

        await ReplyAsync($"{message}");
    }

    // 

    [Command("usable")]
    private async Task ShowUsers()
    {
        var message = "";

        foreach (SocketUser user in _usersList)
        {
            message += $"{user.Username}, ";
        }

        await ReplyAsync($"{message}");
    }

    //

    [Command("help")]
    private async Task ShowCommands()
    {

        await ReplyAsync($"");
    }
    #endregion

}

