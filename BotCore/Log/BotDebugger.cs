using Discord;

namespace DiscordBot.Log;

internal class BotDebugger
{
    public Task OnLog(LogMessage arg)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(arg);
        Console.ResetColor();
        return Task.CompletedTask;
    }

    public static void WriteLogLine(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void WriteErrorLine(string reason)
    {
        Console.WriteLine("\n!!! Бот вызвал ошибку !!!");
        Console.WriteLine(reason);
        Console.WriteLine("!!! Бот вызвал ошибку !!!");
    }
}

