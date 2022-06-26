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

namespace DiscordBot.Services;

public class ConnectionService
{
    private DiscordSocketClient _discordClient;
    private SocketVoiceChannel? _voiceChannel;

    private IAudioClient? _audioClient;

    public bool IsConnected = false;

    public ConnectionService(DiscordSocketClient client) 
    {
        _discordClient = client;
    }

    public async Task ConnectVoiceAsync(ulong guildId = 989937735630471178, ulong channelId = 989937736083472403)
    {
        var guild = _discordClient.GetGuild(guildId);
        _voiceChannel = guild.GetVoiceChannel(channelId);

        _audioClient = await _voiceChannel.ConnectAsync();

        IsConnected = true;

        BotDebugger.WriteLogLine($"Бот был подключен к {_voiceChannel.Name}!");
    }

    public async Task DisconnectVoiceAsync()
    {
        if (_voiceChannel != null)
        {
            await _voiceChannel.DisconnectAsync();
            _audioClient = null;
            IsConnected = false;
            BotDebugger.WriteLogLine($"Бот был отключен от {_voiceChannel.Name}!");
        }
    }

    public IAudioClient? GetAudioClient()
    {
        return _audioClient;
    } 
}

