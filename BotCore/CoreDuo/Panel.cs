﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.CoreDuo;

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
                    case "e":
                        s_isRunning = false;
                        break;
                }
            }
        });
    }
}

