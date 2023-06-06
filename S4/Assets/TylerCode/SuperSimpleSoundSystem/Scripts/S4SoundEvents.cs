using System;

namespace TylerCode.SoundSystem
{
    public class SubtitleEventArgs : EventArgs
    {
        public SubtitleData SubtitleData { get; set; }

        public SubtitleEventArgs(SubtitleData subtitleData)
        {
            SubtitleData = subtitleData;
        }
    }

    public class VolumeEventArgs : EventArgs
    {
        public VolumeData VolumeData { get; set; }

        public VolumeEventArgs(VolumeData volumeData)
        {
            VolumeData = volumeData;
        }
    }

    public class VolumeData
    {
        public float MusicVolume { get; set; }
        public float SoundVolume { get; set; }
    }

    public class SubtitleData
    {
        public string Speaker;
        public string Subtitle;
        public bool IsDialog;
        public bool SubsEnabled;
        public bool CaptionsEnabled;

        public SubtitleData(string speaker, string subtitle, bool isDialog)
        {
            Speaker = speaker;
            Subtitle = subtitle;
            IsDialog = isDialog;
        }
    }
}