using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BotCore;

internal class BotToken
{
    public string? AskToToken()
    {
        Console.WriteLine("Приложение запущено. Здесь будут отображаться все логи бота.");
        Console.WriteLine("Репозиторий бота на GitHub: ");
        Console.Write("\nВведите токен вашего бота: ");
        return Console.ReadLine();
    }
}

