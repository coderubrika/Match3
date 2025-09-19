namespace Test3
{
    public interface IPlayer
    {
        public bool IsPlaying { get; }
        public void Play();
        public void Stop();
        public void Pause();
    }
}