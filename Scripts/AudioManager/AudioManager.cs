using UnityEngine;
using EazyTools.SoundManager;

public class AudioManager : MonoBehaviour
{
    public EazySoundControls[] AudioControls;

    private void Update()
    {
        for (int i = 0; i < AudioControls.Length; i++)
        {
            EazySoundControls audioControl = AudioControls[i];
        }
    }

    public void PlayMenuFull()
    {
        EazySoundControls audioControl = AudioControls[0];

        if (audioControl.audio != null && audioControl.audio.paused)
        {
            audioControl.audio.Resume();
        }
        else
        {
            int audioID = SoundManager.PlayMusic(audioControl.audioclip, 0.5f, true, false);
            AudioControls[0].audio = SoundManager.GetAudio(audioID);
        }
    }

    public void PlayMenuLoop()
    {
        EazySoundControls audioControl = AudioControls[1];

        if (audioControl.audio != null && audioControl.audio.paused)
        {
            audioControl.audio.Resume();
        }
        else
        {
            int audioID = SoundManager.PlayMusic(audioControl.audioclip, 0.5f, true, false);
            AudioControls[1].audio = SoundManager.GetAudio(audioID);
        }
    }

    public void PlayGamePlay()
    {
        EazySoundControls audioControl = AudioControls[2];

        if (audioControl.audio != null && audioControl.audio.paused)
        {
            audioControl.audio.Resume();
        }
        else
        {
            int audioID = SoundManager.PlayMusic(audioControl.audioclip, 0.5f, true, false);
            AudioControls[2].audio = SoundManager.GetAudio(audioID);
        }
    }

    public void PlayDestruction()
    {
        EazySoundControls audioControl = AudioControls[3];
        int audioID = SoundManager.PlaySound(audioControl.audioclip, 1f);

        AudioControls[3].audio = SoundManager.GetAudio(audioID);
    }

    public void PlayWheelImpact()
    {
        EazySoundControls audioControl = AudioControls[4];
        int audioID = SoundManager.PlaySound(audioControl.audioclip, 1f);

        AudioControls[4].audio = SoundManager.GetAudio(audioID);
    }

    public void PlayLanding()
    {
        EazySoundControls audioControl = AudioControls[5];
        int audioID = SoundManager.PlaySound(audioControl.audioclip, 1f);

        AudioControls[5].audio = SoundManager.GetAudio(audioID);
    }

    public void PlayBackflip()
    {
        EazySoundControls audioControl = AudioControls[6];
        int audioID = SoundManager.PlaySound(audioControl.audioclip, 1f);

        AudioControls[6].audio = SoundManager.GetAudio(audioID);
    }

    public void PlayCoin()
    {
        EazySoundControls audioControl = AudioControls[7];
        int audioID = SoundManager.PlaySound(audioControl.audioclip, 1f);

        AudioControls[7].audio = SoundManager.GetAudio(audioID);
    }

    public void PlayClick()
    {
        EazySoundControls audioControl = AudioControls[8];
        int audioID = SoundManager.PlaySound(audioControl.audioclip, 1f);

        AudioControls[8].audio = SoundManager.GetAudio(audioID);
    }

    public void PlayClickChange()
    {
        EazySoundControls audioControl = AudioControls[9];
        int audioID = SoundManager.PlaySound(audioControl.audioclip, 1f);

        AudioControls[9].audio = SoundManager.GetAudio(audioID);
    }

    public void PlayClickPicupcoin()
    {
        EazySoundControls audioControl = AudioControls[10];
        int audioID = SoundManager.PlaySound(audioControl.audioclip, 1f);

        AudioControls[10].audio = SoundManager.GetAudio(audioID);
    }

    public void Pause(string audioControlIDStr)
    {
        int audioControlID = int.Parse(audioControlIDStr);
        EazySoundControls audioControl = AudioControls[audioControlID];

        audioControl.audio.Pause();
    }

    public void Stop(string audioControlIDStr)
    {
        int audioControlID = int.Parse(audioControlIDStr);
        EazySoundControls audioControl = AudioControls[audioControlID];

        audioControl.audio.Stop();
    }

    public bool Playing(string audioControlIDStr)
    {
        int audioControlID = int.Parse(audioControlIDStr);
        EazySoundControls audioControl = AudioControls[audioControlID];

        if (audioControl.audio != null)
            return audioControl.audio.playing;
        else return false;
    }

    public void AudioVolumeChanged(string audioControlIDStr)
    {
        int audioControlID = int.Parse(audioControlIDStr);
        EazySoundControls audioControl = AudioControls[audioControlID];

        if (audioControl.audio != null)
        {
            audioControl.audio.SetVolume(0.5f, 0);
        }
    }

    public void GlobalMusicVolumeChanged(bool _active)
    {
        if (_active) SoundManager.globalMusicVolume = 1f;
        else SoundManager.globalMusicVolume = 0f;
    }

    public void GlobalSoundVolumeChanged(bool _active)
    {
        if (_active) SoundManager.globalSoundsVolume = 1f;
        else SoundManager.globalSoundsVolume = 0f;
    }
}

[System.Serializable]
public struct EazySoundControls
{
    public AudioClip audioclip;
    public Audio audio;
}
