﻿using System;
using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace _Project.Scripts.Main.AppServices
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioSource))]
    public class AudioService : Old_BaseService
    {
        [SerializeField] private AudioListener _audioListener;
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioMixerGroup _soundEffectMixerGroup;
        [SerializeField] private AudioMixerGroup _musicMixerGroup;
        [SerializeField] private AudioClip[] _battlePlaylist;
        [SerializeField] private AudioClip[] _menuPlaylist;

        [Inject] private Old_ScreenService _oldScreenService;

        private MusicPlayerState _currentState;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _audioListener = _oldScreenService.MainCamera.GetComponent<AudioListener>();
            _musicAudioSource = GetComponent<AudioSource>();
        }

        public void Setup(SettingsService settingsService)
        {
            _musicAudioSource.enabled = settingsService.Audio.MusicEnabled;
            SwitchSoundEffects(settingsService.Audio.SoundEnabled);
        }

        public async UniTaskVoid PlayMusic(MusicPlayerState playerState)
        {
            if (_currentState == playerState) return;
            
            var lastState = playerState;
            _currentState = playerState;
            PlayRandomTrack();

            while (_currentState == lastState)
            {
                await UniTask.WaitForFixedUpdate();
                if (_musicAudioSource != null && _musicAudioSource.isPlaying == false)
                {
                    PlayRandomTrack();
                } 
            }
        }

        private async void SwitchSoundEffects(bool newState)
        {
            await UniTask.NextFrame();
            _soundEffectMixerGroup.audioMixer.SetFloat(_soundEffectMixerGroup.name, newState ? 0f : -80f);
        }

        private void PlayRandomTrack()
        {
            if (_musicAudioSource.isActiveAndEnabled == false) return;
            
            switch (_currentState)
            {
                case MusicPlayerState.None:
                    StopMusic();
                    break;
                case MusicPlayerState.MainMenu:
                    if (_menuPlaylist.Length == 0)
                    {
                        Debug.LogWarning("No audio clips on menuPlayList", this);
                    }
                    else
                    {
                        _musicAudioSource.clip = _menuPlaylist.GetRandomItem();
                        _musicAudioSource.Play();
                    }
                    break;
                case MusicPlayerState.Battle:
                    if (_battlePlaylist.Length == 0)
                    {
                        Debug.LogWarning("No audio clips on battlePlayList", this);
                    }
                    else
                    {
                        _musicAudioSource.clip = _battlePlaylist.GetRandomItem();
                        _musicAudioSource.Play();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void StopMusic()
        {
            _currentState = MusicPlayerState.None;
        }

        public enum MusicPlayerState {None, MainMenu, Battle}
    }
}