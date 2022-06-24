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

public class ConnectionManager
{
    private BotDebugger _logger;

    private ChannelConnection channelConnection;
    private AudioConnection audioConnection;

    public ConnectionManager(DiscordSocketClient client)
    {
        _logger = new();
        channelConnection = new(client);
        audioConnection = new(channelConnection.AudioClient);
    }

    public async Task ConnectVoiceAsync()
    {
        try
        {
            await channelConnection.ConnectVoiceAsync(587364626325569556, 962698702416408616);
            _logger.WriteLogLine("Бот успешно подключен к каналу!");
        }
        catch (Exception e)
        {
            _logger.WriteErrorLine(e.Message);
            _logger.WriteErrorLine(e.ToString());
            throw;
        }
    }

    public async Task DisconnectVoiceAsync()
    {
        try
        {
            await channelConnection.DisconnectVoiceAsync();
            _logger.WriteLogLine("Бот был отключен от канала!");
        }
        catch (Exception e)
        {
            _logger.WriteErrorLine(e.Message);
            throw;
        }
    }

    public async Task StartAudioStreamAsync()
    {
        try
        {
            await audioConnection.SendAudioAsync("");
            _logger.WriteLogLine("Бот начал потоковое воиспроизведение!");
        }
        catch (Exception e)
        {
            _logger.WriteErrorLine(e.Message);
            throw;
        }
    }
}