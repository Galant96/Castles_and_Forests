using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }

	[SerializeField]
	public List<Sound> sounds = null;

	[SerializeField]
	public Sound musicSound = null;

	private void Awake()
	{
		SetUpSingelton();

		// Set up sounds
		foreach (Sound sound in sounds)
		{
			if (sound.SoundName == "Music")
			{
				musicSound = sound;
				sound.AudioSource = gameObject.AddComponent<AudioSource>();
				sound.AudioSource.clip = sound.Clip;

				sound.AudioSource.volume = PlayerPrefsController.GetMasterMusicVolume();
				sound.AudioSource.pitch = sound.Pitch;
				sound.AudioSource.loop = sound.Loop;
			}
			else
			{
				sound.AudioSource = gameObject.AddComponent<AudioSource>();
				sound.AudioSource.clip = sound.Clip;

				sound.AudioSource.volume = PlayerPrefsController.GetMasterSoundVolume();
				sound.AudioSource.pitch = sound.Pitch;
				sound.AudioSource.loop = sound.Loop;
			}
			
		}
	}

	public Sound GetSound(string name)
	{
		return sounds.Find(sounds => sounds.SoundName == name);
	}

	public Sound GetMusic()
	{
		return sounds.Find(sounds => sounds.SoundName == "Music");
	}

	public void PlaySound(string name)
	{
		if (sounds != null)
		{
			// Get the sound to play
			Sound sound = sounds.Find(sounds => sounds.SoundName == name);

			if (sound == null)
			{
				Debug.Log("The sound " + name + (" has not been assigned!"));
				return;
			}

			sound.AudioSource.Play();
		}
	}

	public void PauseSound(string name)
	{
		if (sounds != null)
		{
			// Get the sound to play
			Sound sound = sounds.Find(sounds => sounds.SoundName == name);

			if (sound == null)
			{
				Debug.Log("The sound " + name + (" has not been assigned!"));
				return;
			}

			sound.AudioSource.Pause();
		}
	}

	public void PauseAllSounds()
	{
		foreach (Sound sound in sounds)
		{
			sound.AudioSource.Pause();
		}
	}


	private void SetUpSingelton()
	{

		// If instance of the object is null then keep the previous object
		// else destroy the new object and keep "the old" one
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

}
