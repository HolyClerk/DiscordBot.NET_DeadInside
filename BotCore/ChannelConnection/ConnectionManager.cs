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

    private SocketGuild? _guild;
    private DiscordSocketClient _client;
    private SocketVoiceChannel? _voiceChannel;

    public ConnectionManager(DiscordSocketClient client)
    {
        _logger = new();
        _client = client;
    }

    public IAudioClient? AudioClient { get; set; }

    public async Task ConnectVoiceAsync()
    {
        _guild = _client.GetGuild(587364626325569556);
        _voiceChannel = _guild.GetVoiceChannel(758747064430624768);

        AudioClient = await _voiceChannel.ConnectAsync();

        _logger.WriteLogLine("Успешно!");
    }

    public async Task ConnectVoiceAsync(ulong guildId, ulong channelId)
    {
        try
        {
            _guild = _client.GetGuild(guildId);
            _voiceChannel = _guild.GetVoiceChannel(channelId);

            AudioClient = await _voiceChannel.ConnectAsync();

            _logger.WriteLogLine("Успешно!");
        }
        catch (Exception e)
        {
            _logger.WriteErrorLine(e.Message);
        }

    }

    public async Task DisconnectVoiceAsync()
    {
        try
        {
            if (_voiceChannel != null)
            {
                await _voiceChannel.DisconnectAsync();
                _logger.WriteLogLine("Бот был отключен от канала!");
            }
            else
            {

                _logger.WriteLogLine("Нет соединения!");
            }
        }
        catch (Exception e)
        {
            _logger.WriteErrorLine(e.Message);
        }
    }

    private async Task SendAudioAsync(string path)
    {
        if (AudioClient == null)
        {
            return;
        }

        using (var ffmpeg = CreateStream(path))
        using (var output = ffmpeg.StandardOutput.BaseStream)
        using (var discord = AudioClient.CreatePCMStream(AudioApplication.Mixed))
        {
            try { await output.CopyToAsync(discord); }
            finally { await discord.FlushAsync(); }
        }
    }

    private Process? CreateStream(string path)
    {
        return Process.Start(new ProcessStartInfo
        {
            FileName = "ffmpeg",
            Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
            UseShellExecute = false,
            RedirectStandardOutput = true,
        });
    }
}