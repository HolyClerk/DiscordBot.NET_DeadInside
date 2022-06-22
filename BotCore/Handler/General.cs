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
public class General : ModuleBase<SocketCommandContext>
{

    [Command("info", true)]
    private async Task Info()
    {
        var bot = Context.Client.CurrentUser;

        var embed = new EmbedBuilder()
                .WithAuthor("Привет, ебланы!", bot.GetAvatarUrl())
                .WithColor(Color.Orange)
                .WithDescription("Я никто. И буду никем. Ничтожное хуйло, которое ебало твою мать!")

                .AddField("Бот был создан: ", bot.CreatedAt, true)
                .AddField("ID бота: ", bot.Id, true)

                .AddField("\nКОМАНДЫ: ", "!info !help !echo !play", false)

                .WithFooter("Хули надо?");

        await Context.Channel.SendMessageAsync(embed: embed.Build());
    }

    [Command("help")]
    private async Task Help() => await Info();
}