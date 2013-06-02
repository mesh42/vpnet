namespace VpNet.Interfaces
{
    public interface IUniverseFunctions<out TResult>
        where TResult : class, IRc, new()
    {
        TResult Connect(string host = "universe.virtualparadise.org", ushort port = 57000);
        TResult Login(string username, string password, string botname);
    }
}
