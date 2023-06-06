using UnityEngine;

namespace TylerCode.SoundSystem
{
    [System.Serializable]
    public class S4SoundPlayerSettings
    {
        public string soundName;
        public string speaker;
        public string captionText; //For subs or captions
        public bool persistSceneChange; //Calls DontDestroyOnLoad on the object and persists between scenes
        public AudioClip audioClip;
        public Vector3 positionToPlay;
        public Transform parentObject;
        public bool looping;
        public float volume;
        public bool randomPitch = false;
        [Tooltip("If random pitch is false, the min pitch will be used")]
        public float minPitch = 1;
        public float maxPitch = 1;
        public bool isMusic;
        public bool globalSound; //Used when the sound is played at the same position as the listener

        public S4SoundPlayerSettings()
        {
        }

        public S4SoundPlayerSettings(string soundName, string speaker, string captionText, bool persistSceneChange, AudioClip audioClip, Vector3 positionToPlay, Transform parentObject, bool looping, S4SoundPlayerSettings nextSound, float volume, bool randomPitch, float minPitch, float maxPitch, bool isMusic, bool globalSound)
        {
            this.soundName = soundName;
            this.speaker = speaker;
            this.captionText = captionText;
            this.persistSceneChange = persistSceneChange;
            this.audioClip = audioClip;
            this.positionToPlay = positionToPlay;
            this.parentObject = parentObject;
            this.looping = looping;
            this.volume = volume;
            this.randomPitch = randomPitch;
            this.minPitch = minPitch;
            this.maxPitch = maxPitch;
            this.isMusic = isMusic;
            this.globalSound = globalSound;
        }
    }
}
