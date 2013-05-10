using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class GrendelAudioOptions
{
    [HideInInspector]public List<GrendelAudioChannel> AudioChannels = new List<GrendelAudioChannel>();
    [HideInInspector]public List<GrendelAudioBank> AudioBanks = new List<GrendelAudioBank>();

    public const string AcceptedAudioFileTypes = "*.wav;*.mp3;*.ogg";

    private static AudioSource mPreviewAudioSource;

    public static AudioSource PreviewAudioSource
    {
        get
        {
            return mPreviewAudioSource;
        }
    }

    public static void PlayAudioClipPreview(AdjustableAudioClip clip)
    {
        if (mPreviewAudioSource == null)
        {
            mPreviewAudioSource = new GameObject("_previewAudioClip", typeof(AudioSource)).GetComponent<AudioSource>();
            mPreviewAudioSource.gameObject.hideFlags = HideFlags.HideAndDontSave;
        }
        else if (mPreviewAudioSource.isPlaying && mPreviewAudioSource.clip == clip.Clip)
        {
            mPreviewAudioSource.Stop();
            return;
        }

        mPreviewAudioSource.gameObject.transform.position = Vector3.zero;
        mPreviewAudioSource.clip = clip.Clip;
        mPreviewAudioSource.pitch = clip.Pitch;
        mPreviewAudioSource.Play();;
    }
}
