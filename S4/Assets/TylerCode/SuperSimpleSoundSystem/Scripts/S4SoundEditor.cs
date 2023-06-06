using UnityEditor;
using UnityEngine;
using System.Linq;
using System.IO;
using TylerCode.SoundSystem;

namespace TylerCode.SoundSystem
{
    public class SoundDataEditor : EditorWindow
    {
        private string _newSoundDataName = "New Sound Name";
        private Vector2 _scrollPosition;
        private AudioClip _newSoundDataAudioClip;

        [MenuItem("Window/Sound Data Editor")]
        public static void ShowWindow()
        {
            GetWindow<SoundDataEditor>("Sound Data Editor");
        }

        private void OnGUI()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Sounds"))
            {
                AssetDatabase.CreateFolder("Assets", "Sounds");
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, false, false);

            string[] guids = AssetDatabase.FindAssets("t:S4SoundData");

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                S4SoundData soundData = AssetDatabase.LoadAssetAtPath<S4SoundData>(path);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(soundData, typeof(S4SoundData), false);
                if (GUILayout.Button("Edit", GUILayout.Width(50)))
                {
                    SoundDataEditorWindow editorWindow = (SoundDataEditorWindow)EditorWindow.GetWindow(typeof(SoundDataEditorWindow));
                    editorWindow.soundData = soundData;
                    editorWindow.Show();
                }
                if (GUILayout.Button("Delete", GUILayout.Width(50)))
                {
                    AssetDatabase.DeleteAsset(path);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Create a new SoundData", EditorStyles.boldLabel);
            _newSoundDataName = EditorGUILayout.TextField("Name", _newSoundDataName);
            _newSoundDataAudioClip = (AudioClip)EditorGUILayout.ObjectField("Audio Clip", _newSoundDataAudioClip, typeof(AudioClip), false);


            if (GUILayout.Button("Create New"))
            {
                string[] guidCheck = AssetDatabase.FindAssets("t:S4SoundData");
                bool nameExists = guidCheck.Any(guid => AssetDatabase.LoadAssetAtPath<S4SoundData>(AssetDatabase.GUIDToAssetPath(guid)).soundName == _newSoundDataName);

                if (nameExists)
                {
                    EditorUtility.DisplayDialog("Error", "A SoundData object with this name already exists. Please choose a unique name.", "OK");
                }
                else
                {
                    S4SoundData newData = CreateInstance<S4SoundData>();
                    newData.soundName = _newSoundDataName;
                    newData.audioClip = _newSoundDataAudioClip;
                    newData.volume = 1;
                    AssetDatabase.CreateAsset(newData, Path.Combine("Assets/Sounds", _newSoundDataName + ".asset"));
                    AssetDatabase.SaveAssets();
                    _newSoundDataName = "New Sound Name";
                    _newSoundDataAudioClip = null;  // reset the AudioClip field
                }
            }

            EditorGUILayout.EndScrollView();
        }
    }

    public class SoundDataEditorWindow : EditorWindow
    {
        public S4SoundData soundData;

        private void OnGUI()
        {
            if (soundData != null)
            {
                soundData.soundName = EditorGUILayout.TextField("Sound Name", soundData.soundName);
                soundData.speaker = EditorGUILayout.TextField("Speaker", soundData.speaker);
                soundData.captionText = EditorGUILayout.TextField("Caption Text", soundData.captionText);
                soundData.persistSceneChange = EditorGUILayout.Toggle("Persist Scene Change", soundData.persistSceneChange);
                soundData.audioClip = (AudioClip)EditorGUILayout.ObjectField("Audio Clip", soundData.audioClip, typeof(AudioClip), false);
                soundData.positionToPlay = EditorGUILayout.Vector3Field("Position To Play", soundData.positionToPlay);
                soundData.parentObject = (Transform)EditorGUILayout.ObjectField("Parent Object", soundData.parentObject, typeof(Transform), true);
                soundData.looping = EditorGUILayout.Toggle("Looping", soundData.looping);
                soundData.volume = EditorGUILayout.Slider("Volume", soundData.volume, 0, 1);
                soundData.randomPitch = EditorGUILayout.Toggle("Random Pitch", soundData.randomPitch);
            if (soundData.randomPitch)
            {
                soundData.minPitch = EditorGUILayout.Slider("Min Pitch", soundData.minPitch, 0, 2);
                soundData.maxPitch = EditorGUILayout.Slider("Max Pitch", soundData.maxPitch, 0, 2);
            }
            else
                {
                    soundData.minPitch = EditorGUILayout.Slider("Pitch", soundData.minPitch, 0, 2);
                }
                soundData.isMusic = EditorGUILayout.Toggle("Is Music", soundData.isMusic);
                soundData.globalSound = EditorGUILayout.Toggle("Global Sound", soundData.globalSound);

                if (GUILayout.Button("Save"))
                {
                    EditorUtility.SetDirty(soundData);
                    AssetDatabase.SaveAssets();
                }
            }
        }
    }
}
