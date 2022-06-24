namespace DiscordBot.CoreDuo;

internal class BotToken
{
    #warning Реализовать чтение токена из JSON
    public string GetTokenFromJSON(string path)
    {
        // ЧТЕНИЕ ИЗ JSON.
        return "?";
    }

    public string? AskToToken()
    {
        Console.WriteLine("Приложение запущено. Здесь будут отображаться все логи бота.");
        Console.WriteLine("Репозиторий бота на GitHub: https://github.com/HolyClerk/DiscordBot.NET_DeadInside");
        Console.Write("\nВведите токен вашего бота: ");
        return Console.ReadLine();
    }
}

