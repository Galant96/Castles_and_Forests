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

	[SerializeField, Range(0f, 3f)]
	private float pitch = 1f;
	public float Pitch
	{
		get { return pitch; }
		set { pitch = value; }
	}

	[SerializeField]
	private bool loop = false;
	public bool Loop
	{
		get { return loop; }
		set { loop = value; }
	}

	[HideInInspector]
	private AudioSource audioSource;

	public AudioSource AudioSource
	{
		get { return audioSource; }
		set { audioSource = value; }
	}
}
