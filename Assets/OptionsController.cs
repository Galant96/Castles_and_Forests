using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
	[SerializeField]
	private Slider soundSlider = null;

	[SerializeField]
	private float defaultSoundVolume = 0.8f;

	private float currentSoundVolume = 0f;
	private float previousSoundVolume = 0f;

	[SerializeField]
	private Slider musicSlider = null;

	[SerializeField]
	private float defaultMusicVolume = 0.8f;

	private float currentMusicVolume = 0f;
	private float previousMusicVolume = 0f;

	// Start is called before the first frame update
	void Start()
    {

		// This is for menu options issue with synchronising sliders' values
		soundSlider.value = GameManager.Instance.VolumeSoundSliderMainController.value;

		musicSlider.value = GameManager.Instance.VolumeMusicSliderMainController.value;
		//Debug.Log(musicSlider.value);

		// Setting sound volume
		soundSlider.value = PlayerPrefsController.GetMasterSoundVolume();
		previousSoundVolume = soundSlider.value;
		currentSoundVolume = soundSlider.value;

		foreach (Sound sound in SoundManager.Instance.sounds)
		{
			if (sound.SoundName == "Music")
			{
				return;
			}

			sound.Volume = soundSlider.value;

			sound.AudioSource.volume = sound.Volume;
		}

		// Setting music volume
		musicSlider.value = PlayerPrefsController.GetMasterMusicVolume();
		previousMusicVolume = musicSlider.value;
		currentMusicVolume = musicSlider.value;

		SoundManager.Instance.GetMusic().Volume = musicSlider.value;
		SoundManager.Instance.GetMusic().AudioSource.volume = SoundManager.Instance.GetMusic().Volume;
	}

	// Update is called once per frame
	void Update()
    {
		currentSoundVolume = soundSlider.value;

		if (currentSoundVolume != previousSoundVolume)
		{
			previousSoundVolume = currentSoundVolume;

			foreach (Sound sound in SoundManager.Instance.sounds)
			{
				if (sound.SoundName != "Music")
				{
					sound.Volume = soundSlider.value;

					sound.AudioSource.volume = sound.Volume;
				}

			}
		}

		currentMusicVolume = musicSlider.value;

		if (currentMusicVolume != previousMusicVolume)
		{
			previousMusicVolume = currentMusicVolume;

			SoundManager.Instance.GetMusic().Volume = musicSlider.value;
			SoundManager.Instance.GetMusic().AudioSource.volume = SoundManager.Instance.GetMusic().Volume;
		}
	}

	public void Save()
	{
		GameManager.Instance.SyncOptionsSoundSliders(soundSlider.value);
		GameManager.Instance.SyncOptionsMusicSliders(musicSlider.value);


		SoundManager.Instance.PlaySound("Button");
		PlayerPrefsController.SetMasterSoundVolume(soundSlider.value);
		PlayerPrefsController.SetMasterMusicVolume(musicSlider.value);
	}

	public void Exit()
	{
		LevelLoader.Instance.LoadMainMenu();
	}

	public void SetDefault()
	{
		soundSlider.value = defaultSoundVolume;
		musicSlider.value = defaultMusicVolume;
	}
}
