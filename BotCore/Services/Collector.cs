using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Common;
using YoutubeExplode.Videos.Streams;

using DiscordBot.Log;

namespace DiscordBot.Services;

public static class Collector
{
    static YoutubeClient s_client = new();
    static int s_videoCounter = 0;

    public static Process? CreateStream(string link)
    {
        var video = s_client.Videos.GetAsync(link);

        BotDebugger.WriteLogLine($"Название: {video.Result.Title}");
        BotDebugger.WriteLogLine($"Продолжительность: {video.Result.Duration}");
        BotDebugger.WriteLogLine($"Автор: {video.Result.Author}");

        BotDebugger.WriteLogLine("Берем манифест потока");
        var streamManifest = s_client.Videos.Streams.GetManifestAsync(link);
        BotDebugger.WriteLogLine("Берем информацию потока");
        var streamInfo = (IStreamInfo)streamManifest.Result.GetAudioStreams();

        var audioPath = @$"C:\Users\PHPpr\Documents\Dev\BotCore\DiscordBot.NET_DeadInside\BotCore\bin\x86\Release\net6.0\audio\{s_videoCounter++}_audio.{streamInfo.Container}";
        
        BotDebugger.WriteLogLine("Начало скачивания..");

        s_client.Videos.Streams.DownloadAsync(streamInfo, audioPath);

        BotDebugger.WriteLogLine("Скачивание окончено");

        var fileName = @"C:\Users\PHPpr\Documents\Dev\BotCore\DiscordBot.NET_DeadInside\BotCore\bin\x86\Release\net6.0\ffmpeg_audiotool\bin\ffmpeg";

        return Process.Start(new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = $"-hide_banner -loglevel panic -i \"{audioPath}\" -ac 2 -f s16le -ar 48000 pipe:1",
            UseShellExecute = false,
            RedirectStandardOutput = true,
        });
    }
}
