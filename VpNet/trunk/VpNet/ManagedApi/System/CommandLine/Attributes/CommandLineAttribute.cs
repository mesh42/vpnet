namespace VpNet.CommandLine.Attributes
{
    public class CommandLineAttribute : System.Attribute
    {
        public bool Required { get; set; }
        public string HelpDescription { get; set; }
    }
}