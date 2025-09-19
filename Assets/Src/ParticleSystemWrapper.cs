using UnityEngine;

namespace Test3
{
    public class ParticleSystemWrapper : MonoBehaviour, IPlayer
    {
        [SerializeField] private ParticleSystem particleSystem;

        public void SetColor(Color color)
        {
            ParticleSystem.MainModule mainModule = particleSystem.main;
            mainModule.startColor = color;
        }

        public bool IsPlaying => particleSystem.isPlaying;
        
        public void Play()
        {
            particleSystem.Play();
        }

        public void Stop()
        {
            particleSystem.Stop();
        }

        public void Pause()
        {
            particleSystem.Pause();
        }
    }
}