using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Services;
using DiscordBot.CoreDuo;
using YoutubeExplode;

namespace DiscordBot.BotCommands;

public class AudioModule : ModuleBase<SocketCommandContext>
{
    [Command("play", true, RunMode = RunMode.Async)]
    private async Task Connect()
    {
        var channel = (Context.User as IGuildUser)?.VoiceChannel;

        if (channel == null)
        {
            await Context.Channel.SendMessageAsync("Вы должны быть подключены к каналу чтобы начать воспроизведение");
            return;
        }

        if ((Core.CurrentConnection != null) && (Core.CurrentConnection.IsConnected == false))
        {
            await Core.CurrentConnection.ConnectVoiceAsync(guildId: Context.Guild.Id, channelId: channel.Id); // channelId: usedChannel
        }

        var uclient = new YoutubeClient();
        var link = Context.Message.Content.Remove(0, 5);
        var video = uclient.Videos.GetAsync(link);

        await Context.Channel.SendMessageAsync($"Пытаюсь запустить твоё ебаное видео: \n{video.Result.Title} от {video.Result.Author}");

        await Core.CurrentAudioClient.StartStreamAsync(link);
    }

    [Command("stop", RunMode = RunMode.Async)]
    private void Disconnect()
    {
        Core.CurrentAudioClient.StopStreamAsync();
    }
}