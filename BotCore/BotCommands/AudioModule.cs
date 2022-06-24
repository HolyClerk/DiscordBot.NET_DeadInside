﻿using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.ChannelConnection;
using DiscordBot.CoreDuo;

namespace DiscordBot.BotCommands;

public class AudioModule : ModuleBase<SocketCommandContext>
{
    [Command("play", RunMode = RunMode.Async)]
    private async Task Connect()
    {
        var channel = (Context.User as IGuildUser)?.VoiceChannel;

        if (channel == null)
        {
            await Context.Channel.SendMessageAsync("Вы должны быть подключены к каналу чтобы начать воспроизведение");
            return;
        }

        await Core.AudioClient.StartStreamAsync(@"audio/test.mp3");
    }

    [Command("stop", RunMode = RunMode.Async)]
    private async Task Disconnect()
    {
       // await Core.AudioClient.StopAudioStreamAsync();
    }
}