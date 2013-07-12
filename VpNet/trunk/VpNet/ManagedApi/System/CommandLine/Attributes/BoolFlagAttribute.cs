namespace VpNet.CommandLine.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class BoolFlagAttribute : CommandLineAttribute
    {
        public string False { get; set; }
        public string True { get; set; }
    }
}
