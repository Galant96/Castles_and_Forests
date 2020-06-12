using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundKeeper : MonoBehaviour
{
	[SerializeField]
	private string runSound = "change";
	public string RunSound { get => runSound; set => runSound = value; }

	[SerializeField]
	private List<string> attackSounds = new List<string>() { "change" };

	[SerializeField]
	private string jumpSound = "change";
	public string JumpSound { get => jumpSound; set => jumpSound = value; }

	[SerializeField]
	private string climbSound = "change";
	public string ClimbSound { get => climbSound; set => climbSound = value; }

	[SerializeField]
	private string dieSound = "change";
	public string DieSound { get => dieSound; set => dieSound = value; }

	public void PlayCharacterSound(string soundName)
	{
		if (soundName != "change")
		{
			SoundManager.Instance.PlaySoundByName(soundName);
		}
		else
		{
			Debug.Log("The soundis not set to the object.");
		}
	}

	public void PlayRandomAttackSound()
	{
		int randomIndex = Random.Range(0, attackSounds.Count);
		string randomSound = attackSounds[randomIndex];
		Debug.Log(randomSound);

		PlayCharacterSound(randomSound);
	}
}
