using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSourcePlayer : MonoBehaviour
{
	[SerializeField]
	private string soundName = "change";

	private void OnTriggerEnter2D(Collider2D playerCollider)
	{
		if (playerCollider.gameObject.CompareTag("Player") && soundName != "change")
		{
			SoundManager.Instance.PlaySound(soundName);
		}
		else
		{
			Debug.Log("The sound has not been assigned to the object!");
		}
	}

	private void OnTriggerExit2D(Collider2D playerCollider)
	{
		if (playerCollider.gameObject.CompareTag("Player") && soundName != "change")
		{
			SoundManager.Instance.PauseSound(soundName);
		}
		else
		{
			Debug.Log("The sound has not been assigned to the object!");
		}
	}
}
