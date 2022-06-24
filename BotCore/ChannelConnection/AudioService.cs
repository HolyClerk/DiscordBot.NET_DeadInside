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

        BotDebugger.WriteLogLine("Попытка начала потока... ");

        using (var ffmpeg = CreateStream(path))
        {
            if (ffmpeg == null)
            {
                BotDebugger.WriteLogLine("Процесс задан не верно. ffmpeg == null");
                return;
            }

            using (var output = ffmpeg.StandardOutput.BaseStream)
            using (var discord = audioClient.CreatePCMStream(AudioApplication.Mixed))
            {
                try { await output.CopyToAsync(discord); }
                finally { await discord.FlushAsync(); }
            }
        }

        BotDebugger.WriteLogLine("Поток закончен!");
    }

    /// <summary>
    /// NEED TO REALIZE
    /// </summary>
    public void StopStreamAsync()
    {

    }

    private Process? CreateStream(string path)
    {
        return Process.Start(new ProcessStartInfo
        {
            FileName = @"C:\Users\PHPpr\Documents\Dev\BotCore\DiscordBot.NET_DeadInside\BotCore\bin\x86\Release\net6.0\ffmpeg_audiotool\bin\ffmpeg",
            Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
            UseShellExecute = false,
            RedirectStandardOutput = true,
        });
    }
}
