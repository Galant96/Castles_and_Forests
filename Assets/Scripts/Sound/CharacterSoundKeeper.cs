using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundKeeper : MonoBehaviour
{
	[SerializeField]
	private List <string> stepSounds = new List<string>() { "change" };
	public List<string> StepSounds { get => stepSounds; set => stepSounds = value; }

	[SerializeField]
	private List<string> attackSounds = new List<string>() { "change" };
	public List<string> AttackSounds { get => attackSounds; set => attackSounds = value; }

	[SerializeField]
	private string jumpSound = "change";
	public string JumpSound { get => jumpSound; set => jumpSound = value; }

	[SerializeField]
	private string landSound = "change";
	public string LandSound { get => landSound; set => landSound = value; }

	[SerializeField]
	private string climbSound = "change";
	public string ClimbSound { get => climbSound; set => climbSound = value; }

	[SerializeField]
	private List<string> deathSound = new List<string>() { "change" };
	public List<string> DeathSound { get => deathSound; set => deathSound = value; }

	public void PlayCharacterSound(string soundName)
	{
		if (soundName != "change")
		{
			SoundManager.Instance.PlaySound(soundName);
		}
		else
		{
			Debug.Log("The soundis not set to the object.");
		}
	}

	public void PlaySoundByIndex(List<string> soundsList, int index)
	{
		PlayCharacterSound(soundsList[index]);
	}

	public void PlayRandomAttackSound()
	{
		int randomIndex = Random.Range(0, attackSounds.Count);
		string randomSound = attackSounds[randomIndex];
		Debug.Log(randomSound);

		PlayCharacterSound(randomSound);
	}

	public void PlayRandomWeaponImpactSound()
	{
		int randomIndex = Random.Range(0, deathSound.Count);
		string randomSound = deathSound[randomIndex];
		Debug.Log(randomSound);

		PlayCharacterSound(randomSound);
	}
}
