using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

using DiscordBot.Log;
using DiscordBot.CoreDuo;
using Discord.Audio;

namespace DiscordBot.Services;

public class AudioService
{
    public async Task StartStreamAsync(string link = @"audio\test.mp3")
    {
        var audioClient = Core.ConnectionClient?.GetAudioClient();

        if (audioClient == null)
        {
            BotDebugger.WriteLogLine("AudioClient был null");
            return;
        }

        await CreateStream(link, audioClient);
    }

    /// <summary>
    /// NEED TO REALIZE
    /// </summary>
    public void StopStreamAsync()
    {

    }

    private async Task CreateStream(string link, IAudioClient audioClient)
    {
        BotDebugger.WriteLogLine("Создание потока... ");

        using (var ffmpeg = Collector.CreateStream(link))
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
}
