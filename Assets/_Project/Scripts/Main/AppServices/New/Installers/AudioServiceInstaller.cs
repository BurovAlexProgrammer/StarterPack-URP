using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Scripts.Main.AppServices
{
    public class AudioServiceInstaller : BaseServiceInstaller
    {
        public AudioListener AudioListener;
        public AudioSource MusicAudioSource;
        public AudioMixerGroup SoundEffectMixerGroup;
        public AudioMixerGroup MusicMixerGroup;
        public AudioClip[] BattlePlaylist;
        public AudioClip[] MenuPlaylist;
    }
}