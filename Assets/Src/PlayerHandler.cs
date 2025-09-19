using System;

namespace Test3
{
    public class PlayerHandler<TPlayer>: IDisposable
        where TPlayer : IPlayer
    {
        private readonly UpdateSource updateSource;
        private readonly Action<TPlayer> onStop;
        private readonly TPlayer player;
        
        private bool isDisposed;
        
        public PlayerHandler(UpdateSource updateSource, TPlayer player, Action<TPlayer> onStop)
        {
            this.player = player;
            this.onStop = onStop;
            this.updateSource = updateSource;
            updateSource.OnUpdate += Update;
            
            this.player.Play();
        }

        private void Update()
        {
            if (!player.IsPlaying)
                Dispose();
        }
        
        public void Dispose()
        {
            if (isDisposed)
                return;
            
            updateSource.OnUpdate -= Update;
            isDisposed = true;
            player.Stop();
            onStop?.Invoke(player);
        }
    }
}