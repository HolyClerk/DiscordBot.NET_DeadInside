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
using Discord.Audio;
using System.Diagnostics;

namespace DiscordBot.ChannelConnection;

public class ChannelConnection
{
    protected SocketGuild? Guild;
    protected DiscordSocketClient Client;
    protected SocketVoiceChannel? VoiceChannel;

    public ChannelConnection(DiscordSocketClient client) 
    {
        Client = client;
    }

    public IAudioClient? AudioClient { get; set; }

    public async Task ConnectVoiceAsync(ulong guildId, ulong channelId)
    {
        Guild = Client.GetGuild(guildId);
        VoiceChannel = Guild.GetVoiceChannel(channelId);

        AudioClient = await VoiceChannel.ConnectAsync();
    }

    public async Task DisconnectVoiceAsync()
    {
        if (VoiceChannel != null)
        {
            await VoiceChannel.DisconnectAsync();
        }
    }
}

