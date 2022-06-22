using Discord;

namespace DiscordBot.BotLogger;

internal class Logger
{
    public Task OnLog(LogMessage arg)
    {
        Console.WriteLine(arg);
        return Task.CompletedTask;
    }

    public void Write(string reason)
    {
        Console.WriteLine("\n!!! Бот вызвал ошибку !!!");
        Console.WriteLine(reason);
        Console.WriteLine("!!! Бот вызвал ошибку !!!");
    }
}

