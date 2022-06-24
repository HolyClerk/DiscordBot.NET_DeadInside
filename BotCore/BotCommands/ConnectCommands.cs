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
        // Канал, используемый по умолчанию
        ulong usedChannelId = 989937736083472403;

        List<SocketVoiceChannel> usedChannel = (
            from voiceChannel in Context.Guild.VoiceChannels
            where voiceChannel.ConnectedUsers.Contains(Context.User)
            select voiceChannel).ToList();

        if (usedChannel.Count > 0)
        {
            usedChannelId = usedChannel[0].Id;
        }

        if (Core.VoiceManager != null)
        {
            await Core.VoiceManager.ConnectVoiceAsync(guildId: Context.Guild.Id, channelId: usedChannelId); // channelId: usedChannel
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