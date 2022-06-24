using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.BotCommands;

/// <summary>
/// 
/// Каждая команда может взаимодействовать с контекстом ввода.
/// Все методы ДОЛЖНЫ БЫТЬ асинхронными.
/// Вывод через бота осуществляется с помощью асинхронного метода
/// ReplyAsync(метод, унаследованный от ModuleBase.
/// 
/// </summary>
public class General : ModuleBase<SocketCommandContext>
{

    [Command("info", true)]
    private async Task Info()
    {
        var bot = Context.Client.CurrentUser;

        var embed = new EmbedBuilder()
                .WithAuthor("Привет, ебланы!", bot.GetAvatarUrl())
                .WithColor(Color.Orange)
                .WithDescription("Я никто. И буду никем. Я - ничтожное хуйло, но я это признаю!")

                .AddField("Бот был создан: ", bot.CreatedAt, true)
                .AddField("ID бота: ", bot.Id, true)

                .AddField("\n ОСНОВНЫЕ КОМАНДЫ: ", "\n!play \"ссылка\"\n!stop \n!connect \n!disconnect", false)
                .AddField("\n ДОП. КОМАНДЫ: ", "\n!info \n!help \n!echo", inline: true)

                .WithFooter("Хули надо?");

        await Context.Channel.SendMessageAsync(embed: embed.Build());
    }

    [Command("help")]
    private async Task Help() => await Info();
}