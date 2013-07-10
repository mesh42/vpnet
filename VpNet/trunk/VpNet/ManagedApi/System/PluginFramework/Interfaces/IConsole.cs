using System;

namespace VpNet.PluginFramework.Interfaces
{
    public interface IConsole
    {
        ConsoleColor BackgroundColor { get; set; }
        bool IsPromptEnabled { get; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        string Title { get; set; }

        void ReadLine();
        void WriteLine(ConsoleMessageType type, string text);
        void WriteLine(string text);
        void Write(ConsoleMessageType type, string text);
        void Write(string text);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
    }
}