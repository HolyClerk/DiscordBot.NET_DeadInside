using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Handler;

public class Echo : ModuleBase<SocketCommandContext>
{
    [Command("echo", true)]
    private async Task EchoAsync()
    {
        var message = Context.Message.Content.Remove(0, 5);

        await Context.Channel.SendMessageAsync(message);
    }
}