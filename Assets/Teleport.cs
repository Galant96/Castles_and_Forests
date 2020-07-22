using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	[SerializeField]
	private string soundName = "";

	private bool wasTeleported = false;

	[SerializeField, Header("Register to know, when the teleportation occures.")]
	OnTeleportationEvent onTeleportation = null;

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") || collision.CompareTag("Object"))
		{
			if (wasTeleported == false)
			{
				Debug.Log(collision.tag);

				SoundManager.Instance.PlaySound(soundName);

				onTeleportation.Invoke(collision.gameObject);
			}
		}
	}

	protected void OnTriggerExit2D(Collider2D collision)
	{
		wasTeleported = false;
	}

	public void ObjectTeleport(GameObject teleportedObject)
	{
		Debug.Log(teleportedObject);

		wasTeleported = true;
		teleportedObject.transform.position = transform.position;
		// Reset the virtual cameras.
		//GameplayCamera.Instance.SetCameras(PlayerCharacter.Instance.gameObject);
	}

}
