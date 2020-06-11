using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }

	[SerializeField]
	public List<Sound> sounds;

	private void Awake()
	{
		SetUpSingelton();

		// Set up sounds
		foreach (Sound sound in sounds)
		{
			sound.AudioSource = gameObject.AddComponent<AudioSource>();
			sound.AudioSource.clip = sound.Clip;

			sound.AudioSource.volume = sound.Volume;
			sound.AudioSource.pitch = sound.Pitch;
		}
	}
	/// <summary>
	/// Use this function to play a random sound
	/// </summary>
	public void PlayRandomSound()
	{
		if (sounds.Count > 0)
		{
			// Play sound effect for the given collectable object
			AudioSource.PlayClipAtPoint(sounds[GetRandomSoundIndex()].AudioSource.clip, Camera.main.transform.position);
		}
	}

	public void PlaySoundAtIndex(int index)
	{
		if (sounds.Count > 0)
		{
			// Play sound effect for the given collectable object
			AudioSource.PlayClipAtPoint(sounds[index].AudioSource.clip, Camera.main.transform.position);
		}
	}

	public void PlaySoundByName(string name)
	{
		if (sounds.Count > 0)
		{
			// Get the sound to play
			Sound sound = sounds.Find(sounds => sounds.SoundName == name);
			AudioSource.PlayClipAtPoint(sound.Clip, Camera.main.transform.position);
		}
	}

	/// <summary>
	/// Pick a random index from the list
	/// </summary>
	/// <returns> Random index </returns>
	private int GetRandomSoundIndex()
	{
		// Get random index
		int randomIndex = Random.Range(0, sounds.Count - 1);
		return randomIndex;
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
