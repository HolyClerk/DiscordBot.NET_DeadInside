namespace DiscordBot.CoreDuo;

/// <summary>
/// Панель разработчика для теста функционала
/// </summary>
public class Panel
{
    private static bool s_isRunning = true;

    public static async Task<Task> StartAsync()
    {
        await HandleInput();

        return Task.CompletedTask;
    }

    static async Task HandleInput()
    {
        await Task.Run(() =>
        {
            while (s_isRunning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("-> ");
                Console.ResetColor();

                var input = Console.ReadLine();

                switch (input)
                {
                    case "connect":
                        Core.CurrentConnection?.ConnectVoiceAsync();
                        break;
                    case "disconnect":
                        Core.CurrentConnection?.DisconnectVoiceAsync();
                        break;
                    case "e":
                        s_isRunning = false;
                        break;
                }
            }
        });
    }
}

