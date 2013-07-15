using VpNet.PluginFramework.Interfaces;

namespace VpNet.ManagedApi.System.PluginFramework
{
    public class NullConsole : IConsole
    {
        public NullConsole()
        {
            GetPromptTarget = NullPrompt;
            ParseCommandLine = NullParser;
        }

        private string NullPrompt()
        {
            return string.Empty;
        }

        private void NullParser(string commandline)
        {
            
        }

        public void RevertPrompt(){}

        public VpNet.PluginFramework.Interfaces.IConsoleDelegate.GetPrompt GetPromptTarget { get; set; }

        public VpNet.PluginFramework.Interfaces.IConsoleDelegate.ParseCommandLineDelegate ParseCommandLine { get; set; }

        public global::System.ConsoleColor BackgroundColor { get; set; }

        public bool IsPromptEnabled
        {
            get { return false; }
        }

        public string Title
        {
            get { return string.Empty; }
            set{ }
        }

        public void ReadLine()
        {
            
        }

        public void WriteLine(ConsoleMessageType type, string text)
        {
            
        }

        public void WriteLine(string text)
        {
           
        }

        public void Write(ConsoleMessageType type, string text)
        {
           
        }

        public void Write(string text)
        {
           
        }

        public void Clear()
        {
            
        }
    }
}
