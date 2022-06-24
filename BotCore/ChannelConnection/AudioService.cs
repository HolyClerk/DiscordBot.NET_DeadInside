using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Diagnostics;

using DiscordBot.Log;
using DiscordBot.CoreDuo;
using Discord.Audio;


namespace DiscordBot.ChannelConnection;

public class AudioService
{
    public async Task StartStreamAsync(string path = @"audio\test.mp3")
    {
        var audioClient = Core.ConnectionClient?.GetAudioClient();

        if (audioClient == null)
        {
            BotDebugger.WriteLogLine("AudioClient был null");
            return;
        }
        
        using (var ffmpeg = CreateStream(path))
        using (var output = ffmpeg.StandardOutput.BaseStream)
        using (var discord = audioClient.CreatePCMStream(AudioApplication.Mixed))
        {
            try { await output.CopyToAsync(discord); }
            finally { await discord.FlushAsync(); }
        }

        BotDebugger.WriteLogLine("Поток начат!");
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
