﻿#region Copyright notice
/*
____   ___.__         __               .__    __________                        .__.__                
\   \ /   |__________/  |_ __ _______  |  |   \______   _____ ____________    __| _|__| ______ ____   
 \   Y   /|  \_  __ \   __|  |  \__  \ |  |    |     ___\__  \\_  __ \__  \  / __ ||  |/  ____/ __ \  
  \     / |  ||  | \/|  | |  |  // __ \|  |__  |    |    / __ \|  | \// __ \/ /_/ ||  |\___ \\  ___/  
   \___/  |__||__|   |__| |____/(____  |____/  |____|   (____  |__|  (____  \____ ||__/____  >\___  > 
                                     \/                      \/           \/     \/        \/     \/  
    This file is part of VPNET Version 1.0

    Copyright (c) 2012-2013 CUBE3 (Cit:36)

    VPNET is free software: you can redistribute it and/or modify it under the terms of the 
    GNU Lesser General Public License (LGPL) as published by the Free Software Foundation, either
    version 2.1 of the License, or (at your option) any later version.

    VPNET is distributed in the hope that it will be useful,but WITHOUT ANY WARRANTY; without even
    the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the LGPL License
    for more details.

    You should have received a copy of the GNU Lesser General Public License (LGPL) along with VPNET.
    If not, see <http://www.gnu.org/licenses/>. 
*/
#endregion

using System;
using System.Collections.Generic;

namespace VpNet.Examples.Gui
{

    public sealed class ConsoleHelpers 
    {
        public ConsoleColor BackgroundColor
        {
            get
            {
                return Console.BackgroundColor;
            }
            set
            {
                Console.BackgroundColor = value;
            }
        }

        public bool IsMaskedInput;

        private List<string> _commandHistory = new List<string>();
        private static int _historyIndex;

        public ConsoleColor PromptColor = ConsoleColor.White;

        public ConsoleHelpers()
        {
            Console.CancelKeyPress += Console_CancelKeyPress;
        }

        /// <summary>
        /// Disable special keys.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ConsoleCancelEventArgs"/> instance containing the event data.</param>
        private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            switch (e.SpecialKey)
            {
                case ConsoleSpecialKey.ControlC:
                    e.Cancel = true;
                    break;
                default:
                    e.Cancel = false;
                    break;
            }
        }


        public delegate string GetPrompt();
        public GetPrompt GetPromptTarget { get; set; }

        public ParseCommandLineDelegate ParseCommandLine;
        public delegate void ParseCommandLineDelegate(string commandLine);

        public static List<Char> keyBuffer = new List<char>();

        private bool _isReadlineMode;

        private string KeyBufferToString()
        {
            string commandLine = string.Empty;
            foreach (char item in keyBuffer)
                commandLine += item;
            return commandLine;
        }

        private void KeyBufferFromString(string commandLine)
        {
            keyBuffer.Clear();
            for (int i = 0; i < commandLine.Length; i++)
            {
                keyBuffer.Add(commandLine[i]);
            }
        }


        public bool IsPromptEnabled { get; private set; }

        static System.Threading.Thread t;

        public void ReadLine()
        {
            if (GetPromptTarget != null)
            {
                var oldColor = Console.ForegroundColor;
                Console.CursorLeft = 0;
                Console.ForegroundColor = PromptColor;
                Console.Write(GetPromptTarget());
                //Console.ForegroundColor = oldColor;
            }
            t = new System.Threading.Thread(readLine);
            t.Start();
        }

        private void ScrollDown()
        {
            if (Console.CursorTop >= Console.WindowHeight-1 /* Console.WindowHeight - 1*/)
            {
                //Console.MoveBufferArea(0, 1, Console.WindowWidth, Console.WindowHeight - 1, 0, 0);
                Console.MoveBufferArea(0, 1, 120, 40 - 1, 0, 0);
                return;
            }
            if (Console.CursorTop != Console.WindowHeight-1)
                Console.CursorTop++;

        }

        private void WritePrompt(int oldLength)
        {
            Console.CursorLeft = GetPromptTarget().Length;
            Console.Write("".PadRight(oldLength));
            Console.CursorLeft = GetPromptTarget().Length;

            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = PromptColor;
            if (GetPromptTarget != null)
                Console.Write(KeyBufferToString());
            Console.ForegroundColor = oldColor;
        }

        private void readLine()
        {
            _isReadlineMode = true;
            while (1 == 1)
            {

                var keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (_historyIndex > 0)
                        {
                            _historyIndex--;
                            var oldLength = keyBuffer.Count;
                            KeyBufferFromString(_commandHistory[_historyIndex]);
                            WritePrompt(oldLength);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (_historyIndex < _commandHistory.Count - 1)
                        {
                            _historyIndex++;
                            var oldLength = keyBuffer.Count;
                            KeyBufferFromString(_commandHistory[_historyIndex]);
                            WritePrompt(oldLength);
                        }
                        break;
                }

                if (keyInfo.KeyChar == 13 && KeyBufferToString() != string.Empty)
                {
                    string commandLine = string.Empty;
                    commandLine = KeyBufferToString();
                    _historyIndex = _commandHistory.Count;
                    _commandHistory.Add(commandLine);
                    keyBuffer.Clear();
                    ScrollDown();
                    _isReadlineMode = false;
                    ParseCommandLine(commandLine);
                    return;
                }
                if (keyInfo.KeyChar != 13 && (keyInfo.KeyChar > 31 && keyInfo.KeyChar < 127))
                {
                    keyBuffer.Add(keyInfo.KeyChar);
                    if (!IsMaskedInput)
                        Console.Write(keyInfo.KeyChar);
                    else
                    {
                        Console.Write('*');
                    }
                }
                else
                {
                    if (keyInfo.KeyChar == 8)
                    {
                        if (keyBuffer.Count > 0)
                        {
                            keyBuffer.RemoveAt(keyBuffer.Count - 1);
                            Console.Write(keyInfo.KeyChar);
                            Console.Write(" ");
                            Console.CursorLeft--;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the color of the console.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private ConsoleColor GetConsoleColor(ConsoleMessageType type)
        {
            switch (type)
            {
                case ConsoleMessageType.Normal:
                    return ConsoleColor.Gray;
                case ConsoleMessageType.Information:
                    break;
                case ConsoleMessageType.Error:
                    return ConsoleColor.Red;
            }
            return ConsoleColor.Gray;
        }

        public void WriteLine(ConsoleMessageType type, string text)
        {
            for (int i = 0; i < text.Length; i = i + Console.WindowWidth)
            {
                if (text.Length - i < Console.WindowWidth)
                    _writeLine(GetConsoleColor(type), text.Substring(i, text.Length - i));
                else
                    Write(type, text.Substring(i, Console.WindowWidth));
            }

            if (Console.CursorTop > 0 /* && _isReadlineMode*/)
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = PromptColor;
                if (GetPromptTarget != null)
                    Console.Write(GetPromptTarget() + KeyBufferToString());
                Console.ForegroundColor = oldColor;
            }

        }

        private void _writeLine(ConsoleColor color, string text)
        {
            // move prompt to next line.
            if (Console.CursorTop > 0)
            {
                Console.Write("".PadRight(Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
            Console.ForegroundColor = color;
            Console.WriteLine(text.PadRight(Console.WindowWidth - text.Length)); // Todo handle width > 120
        }

        public void WriteLine(string text)
        {
            WriteLine(ConsoleMessageType.Normal, text);
        }

        public void Write(ConsoleMessageType type, string text)
        {
            int left = Console.CursorLeft;
            // move prompt to next line.
            if (Console.CursorTop > 0)
            {
                Console.Write("".PadRight(Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
            Console.ForegroundColor = GetConsoleColor(type);
            Console.Write(text.PadRight(Console.WindowWidth - text.Length)); // Todo handle width > 120
            if (Console.CursorTop > 0 && _isReadlineMode)
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = PromptColor;
                if (GetPromptTarget != null)
                    Console.Write(GetPromptTarget() + KeyBufferToString());
                Console.ForegroundColor = oldColor;
            }
        }

        public void Write(string text)
        {
            int left = Console.CursorLeft;
            // move prompt to next line.
            if (Console.CursorTop > 0)
            {
                Console.Write("".PadRight(Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
            Console.ForegroundColor = GetConsoleColor(ConsoleMessageType.Normal);
            Console.WriteLine(text.PadRight(Console.WindowWidth - text.Length));
            if (Console.CursorTop > 0 && _isReadlineMode)
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = PromptColor;
                if (GetPromptTarget != null)
                    Console.Write(GetPromptTarget() + KeyBufferToString());
                Console.ForegroundColor = oldColor;
            }
        }

        #region IConsoleSystem Members

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return Console.Title; }
            set { Console.Title = value; }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            Console.Clear();
        }

        #endregion
    }
}