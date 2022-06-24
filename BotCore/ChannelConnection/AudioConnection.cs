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

public class AudioConnection
{
    public AudioConnection(IAudioClient? audioClient)
    {
        AudioClient = audioClient;
    }

    public IAudioClient? AudioClient { get; set; }

    public async Task SendAudioAsync(string path)
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
