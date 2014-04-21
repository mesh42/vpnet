using System;

namespace VpNet.GameExtensions
{
    public class GameAvatarManager
    {
        private readonly GameInstance _instance;


        public GameAvatarManager(GameInstance instance)
        {
            _instance = instance;
            _instance.OnAvatarChange += _instance_OnAvatarChange;
            _instance.OnAvatarClick += _instance_OnAvatarClick;
            _instance.OnAvatarLeave += _instance_OnAvatarLeave;
        }

        void _instance_OnAvatarLeave(GameInstance sender, GameAvatarLeaveEventArgs args)
        {
            throw new NotImplementedException();
        }

        void _instance_OnAvatarClick(GameInstance sender, GameAvatarClickEventArgs args)
        {
            throw new NotImplementedException();
        }

        void _instance_OnAvatarChange(GameInstance sender, GameAvatarChangeEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
