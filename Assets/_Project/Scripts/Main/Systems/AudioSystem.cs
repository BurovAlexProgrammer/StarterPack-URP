using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Events;
using _Project.Scripts.Main.Events.Audio;

namespace _Project.Scripts.Main.Systems
{
    public class AudioSystem : BaseSystem
    {
        private AudioService _audioService;
        public override void Init()
        {
            base.Init();
            _audioService = Services.Get<AudioService>();
        }

        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<PlayMenuMusicEvent>(PlayMenuMusic);
        }

        private void PlayMenuMusic(BaseEvent obj)
        {
            _audioService.PlayMusic(AudioService.MusicPlayerState.MainMenu);
        }
    }
}