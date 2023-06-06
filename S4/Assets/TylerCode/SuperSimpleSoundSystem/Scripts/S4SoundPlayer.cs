﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TylerCode.SoundSystem
{
    /// <summary>
    /// This class is responsible for actually playing the sound. You shouldn't ever really worry about this. 
    /// </summary>
    public class S4SoundPlayer : MonoBehaviour
    {
        [SerializeField]
        private S4SoundPlayerSettings _soundPlayerSettings;
        [SerializeField]
        private int _playerID = 0;

        private bool _naturalDeath = false; //Lets the player know if it's being destroyed as part of a scene cleanup

        //This function should be cleaned up instead of a ton of if statements. 
        /// <summary>
        /// Starts playing a sound by adding an audio source to this object with the proper configuration and starting the sound. 
        /// </summary>
        /// <param name="player">The sound player settings for this sound.</param>
        /// <param name="id">The ID assigned to this sound from the S4 manager.</param>
        /// <param name="manager">The S4 Sound Manager controlling the operation.</param>
        /// <param name="fadeIn">Should this sound have a 3 second fade-in? (Optional)</param>
        public void StartPlayer(S4SoundPlayerSettings player, int id, S4SoundManager manager, bool fadeIn = false)
        {
            _soundPlayerSettings = player;
            float desiredVolume = 1;

            if (_soundPlayerSettings.parentObject != null)
            {
                this.transform.position = (_soundPlayerSettings.parentObject.position + _soundPlayerSettings.positionToPlay);
                this.transform.parent = _soundPlayerSettings.parentObject;
            }
            else
            {
                this.transform.position = _soundPlayerSettings.positionToPlay;
            }

            if (player.isMusic)
            {
                desiredVolume = _soundPlayerSettings.volume * 1; //manager.musicVolume;
            }
            else
            {
                desiredVolume = _soundPlayerSettings.volume * 1;// manager.soundVolume;
            }

            AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();

            audioSource.clip = _soundPlayerSettings.audioClip;
            audioSource.loop = _soundPlayerSettings.looping;

            if (player.randomPitch)
            {
                audioSource.pitch = Random.Range(player.minPitch, player.maxPitch);
            }
            else
            {
                audioSource.pitch = _soundPlayerSettings.minPitch;
            }

            if (fadeIn)
            {
                audioSource.volume = 0;
                StartCoroutine(FadeAudioSource.StartFade(audioSource, 3, desiredVolume));
            }
            else
            {
                audioSource.volume = desiredVolume;
            }

            if (_soundPlayerSettings.globalSound)
            {
                this.transform.parent = null;
                audioSource.rolloffMode = AudioRolloffMode.Linear;
                audioSource.minDistance = 10000;
                audioSource.maxDistance = 10000;
                audioSource.dopplerLevel = 0;
            }

            if (_soundPlayerSettings.looping == false)
            {
                Invoke("EndOfSound", _soundPlayerSettings.audioClip.length);
            }

            if (_soundPlayerSettings.persistSceneChange)
            {
                DontDestroyOnLoad(this.gameObject);
            }

            audioSource.Play();
        }

        /// <summary>
        /// Exists because you can't invoke a method with optional arguments. 
        /// </summary>
        private void EndOfSound()
        {
            StopPlayer();
        }

        /// <summary>
        /// Stops the sound/song from playing
        /// </summary>
        /// <param name="fade">Should this sound fade out over 3 seconds? (Optional)</param>
        public void StopPlayer(bool fade = false)
        {
            S4SoundManager manager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<S4SoundManager>();

            AudioSource audioSource = GetComponent<AudioSource>();

            if (fade)
            {
                StartCoroutine(FadeAudioSource.StartFade(audioSource, 3, 0));
                Invoke("StopSound", 3);
            }
            else
            {
                StopSound();
            }

            manager.RemoveSound(_playerID);
            _naturalDeath = true;
        }

        public void OnDestroy()
        {
            if (_naturalDeath == false && _playerID != 0)
            {
                StopPlayer();
            }
        }

        private void StopSound()
        {
            AudioSource audioSource = GetComponent<AudioSource>();

            audioSource.Stop();
            Destroy(this.gameObject);
        }
    }
}
