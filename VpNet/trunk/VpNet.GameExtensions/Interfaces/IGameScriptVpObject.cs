namespace VpNet.GameExtensions.Interfaces
{
    public interface IGameScriptVpObject
    {
        GameInstance GameInstance { get; set; }
        GameVpObject GameVpObject { get; set; }
        void Initialize();
        void Unload();
    }
}
