using DiscordBot.BotCore;

namespace DiscordBot;

internal class Program
{
    static void Main(string[] args) => new Core().RunBotAsync().GetAwaiter().GetResult();
}
