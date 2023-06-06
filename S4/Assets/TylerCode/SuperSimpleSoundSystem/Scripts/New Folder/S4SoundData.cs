using UnityEngine;

namespace TylerCode.SoundSystem
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "SoundData", order = 1)]
    public class S4SoundData : ScriptableObject
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
    }
}
