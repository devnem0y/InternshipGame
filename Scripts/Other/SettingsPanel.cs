using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{

    private AudioManager audioManager;
    public Button music, sound;
	public Sprite musicOn, musicOff, soundOn, soundOff;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
		if (Data.music == "off") music.GetComponent<Image> ().sprite = musicOff;
		else music.GetComponent<Image> ().sprite = musicOn;

		if (Data.sound == "off") sound.GetComponent<Image> ().sprite = soundOff;
		else sound.GetComponent<Image> ().sprite = soundOn;
	}

	public void Clicked(string name)
    {
		switch (name)
        {
		    case "Music":
			    if (Data.music == "off")
                {
				    music.GetComponent<Image> ().sprite = musicOn;
                    Data.music = "on";
                    audioManager.GlobalMusicVolumeChanged(true);
			    }
                else
                {
				    music.GetComponent<Image> ().sprite = musicOff;
                    Data.music = "off";
                    audioManager.GlobalMusicVolumeChanged(false);
                }
			    break;
		    case "Sound":
			    if (Data.sound == "off")
                {
				    sound.GetComponent<Image> ().sprite = soundOn;
                    Data.sound = "on";
                    audioManager.GlobalSoundVolumeChanged(true);
                }
                else
                {
				    sound.GetComponent<Image> ().sprite = soundOff;
                    Data.sound = "off";
                    audioManager.GlobalSoundVolumeChanged(false);
                }
			    break;
		}
	}
}
