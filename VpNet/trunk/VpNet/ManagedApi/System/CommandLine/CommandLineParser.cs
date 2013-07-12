using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using VpNet.CommandLine;
using VpNet.CommandLine.Attributes;

namespace VpNet.ManagedApi.System.CommandLine
{
    public class CommandLineParser
    {
        private string[] _args;

        public bool Parse(string commandLine)
        {
            // convert string[] args to simulate console input type style
            _args = (from object match in Regex.Matches(commandLine, @"([^\s]*""[^""]+""[^\s]*)|\w+") select match.ToString()).ToArray();
            var types = Assembly.GetCallingAssembly().GetTypes().Where(x => x.GetInterface(typeof(IParsableCommand).ToString()) != null);
            foreach (var type in types)
            {
                var b = type.GetCustomAttributes(typeof (CommandAttribute),false);
                if (b.Length == 1)
                {
                    var a = (CommandAttribute)b[0];
                    if (a.Literal == _args[0].ToLower())
                    {
                        // process the command.
                        var cmd = Activator.CreateInstance(type);
                    }
                }
            }
            return false;
        }

        public bool Parse(string[] args)
        {
            _args = args;
            return true;
        }
    }
}
