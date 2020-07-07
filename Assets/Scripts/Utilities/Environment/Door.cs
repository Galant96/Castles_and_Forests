using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField]
	private string openDoorSoundName = "change";

	[SerializeField]
	private string closeDoorSoundName = "change";

	[SerializeField]
	private Animator animator = null;

	[SerializeField]
	private BoxCollider2D doorCollider = null;

	private void Start()
	{
		if (animator == null || doorCollider == null)
		{
			animator = GetComponent<Animator>();
			doorCollider = GetComponent<BoxCollider2D>();
		}

		doorCollider.isTrigger = false;
	}

	public void OpenDoor(bool wasOpen)
	{
		// Allow to pass through the door
		doorCollider.isTrigger = wasOpen;
		animator.SetBool("Opening", wasOpen);

		if (wasOpen)
		{
			SoundManager.Instance.PlaySound(openDoorSoundName);
		}
		else
		{
			SoundManager.Instance.PlaySound(closeDoorSoundName);
		}
	}
}
