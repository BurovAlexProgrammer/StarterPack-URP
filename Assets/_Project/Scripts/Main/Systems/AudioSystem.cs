using _Project.Scripts.Main.Events;
using _Project.Scripts.Main.Events.Audio;
using _Project.Scripts.Main.Services;

namespace _Project.Scripts.Main.Systems
{
    public class AudioSystem : BaseSystem
    {
        private AudioService _audioService;
        public override void Init()
        {
            base.Init();
            _audioService = Services.Services.Get<AudioService>();
            new AudioSystemInitializedEvent().Fire();
        }

        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<PlayMenuMusicEvent>(PlayMenuMusic);
            AddListener<PlayGameEvent>(OnPlayGame);
            AddListener<ShowMainMenuEvent>(OnShowMainMenu);
        }

        private void OnShowMainMenu(BaseEvent obj)
        { 
            _audioService.PlayMusic(AudioService.MusicPlayerState.MainMenu);
        }

        private void OnPlayGame(BaseEvent obj)
        {
            _audioService.PlayMusic(AudioService.MusicPlayerState.Battle);
        }

        private void PlayMenuMusic(BaseEvent obj)
        {
            _audioService.PlayMusic(AudioService.MusicPlayerState.MainMenu);
        }
    }
}