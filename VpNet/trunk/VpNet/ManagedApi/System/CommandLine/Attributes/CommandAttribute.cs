namespace VpNet.CommandLine.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class CommandAttribute : System.Attribute
    {
        public string Literal { get; set; }
    }
}
