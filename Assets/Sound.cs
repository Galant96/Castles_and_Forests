using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
	[SerializeField]
	private string soundName;

	public string SoundName
	{
		get { return soundName; }
		set { soundName = value; }
	}

	[SerializeField]
	private AudioClip clip;

	public AudioClip Clip
	{
		get { return clip; }
		set { clip = value; }
	}

	[SerializeField, Range(0f, 1f)]
	private float volume = 0.7f;
	public float Volume
	{
		get { return volume; }
		set { volume = value; }
	}

	[SerializeField, Range(0f, 1.5f)]
	private float pitch = 0.7f;
	public float Pitch
	{
		get { return pitch; }
		set { pitch = value; }
	}

	[HideInInspector]
	private AudioSource audioSource;

	public AudioSource AudioSource
	{
		get { return audioSource; }
		set { audioSource = value; }
	}
}
