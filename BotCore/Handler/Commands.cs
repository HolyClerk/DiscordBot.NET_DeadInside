using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Handler;

public struct CommandsHelper
{
    public Dictionary<string, string> BotCommands = new Dictionary<string, string>()
    {
        { "!help", "Отображает список возможных команд." },
        { "!echo", "Вывод текста с помощью бота." },
        { "!курс", "Выводит текущий курс доллара в рублях." },
        { "!help", "Отображает список возможных команд" },
        { "!help", "Отображает список возможных команд" },
    };

    public CommandsHelper() { }
}

