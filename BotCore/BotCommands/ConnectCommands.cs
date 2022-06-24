using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;

using DiscordBot.CoreDuo;

namespace DiscordBot.BotCommands;

public class ConnectCommands : ModuleBase<SocketCommandContext>
{
    [Command("connect", true)]
    private async Task Connect()
    {
        var channel = (Context.User as IGuildUser)?.VoiceChannel;

        if (channel == null)
        {
            await Context.Channel.SendMessageAsync("Вы должны быть подключены к каналу чтобы использовать это.");
            return;
        }

        if (Core.ConnectionClient != null)
        {
            await Core.ConnectionClient.ConnectVoiceAsync(guildId: Context.Guild.Id, channelId: channel.Id); // channelId: usedChannel
        }
    }

    [Command("disconnect", true)]
    private async Task Disconnect()
    {
        if (Core.ConnectionClient != null)
        {
            await Core.ConnectionClient.DisconnectVoiceAsync();
        }
    }
}