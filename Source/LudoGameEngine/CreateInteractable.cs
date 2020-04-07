﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class CreateInteractable
    {
        public static int SingleButton(Func<int> methodName, string buttonText)
        {
            int valueToReturn = 0;
            Console.CursorVisible = false;
            ConsoleKey? key = null;

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.WriteLine("  " + buttonText + "  ");
            Console.WriteLine("");
            Console.ResetColor();

            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey().Key;
            }

            valueToReturn = methodName();

            Console.Clear();
            return valueToReturn;
        }

        public static int OptionMenu(bool horizontal, string[] option)
        {

            int selectedIndex = 0;
            Console.CursorVisible = false;
            ConsoleKey? key = null;

            while (key != ConsoleKey.Enter)
            {
                if (horizontal == false)
                {
                    for (int i = 0; i < option.Length; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.WriteLine(option[i]);
                        Console.ResetColor();
                    }

                    key = Console.ReadKey().Key;

                    if (key == ConsoleKey.DownArrow)
                    {
                        selectedIndex++;
                        if (selectedIndex == option.Length)
                            selectedIndex = 0;
                    }
                    else if (key == ConsoleKey.UpArrow)
                    {
                        selectedIndex--;
                        if (selectedIndex == -1)
                            selectedIndex = option.Length - 1;
                    }

                    Console.Clear();
                }
                else
                {
                    for (int i = 0; i < option.Length; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.Write(" " + option[i] + " ");
                        Console.ResetColor();
                    }

                    key = Console.ReadKey().Key;

                    if (key == ConsoleKey.RightArrow)
                    {
                        selectedIndex++;
                        if (selectedIndex == option.Length)
                            selectedIndex = 0;
                    }
                    else if (key == ConsoleKey.LeftArrow)
                    {
                        selectedIndex--;
                        if (selectedIndex == -1)
                            selectedIndex = option.Length - 1;
                    }                   
                }

                Console.Clear();

            }

            return selectedIndex;
        }
    }
}
