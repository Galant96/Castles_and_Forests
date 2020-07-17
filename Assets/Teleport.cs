using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	private bool wasTeleported = false;

	[SerializeField, Header("Register to know, when the teleportation occures.")]
	OnTeleportationEvent onTeleportation = null;

	// Start is called before the first frame update
	void Start()
    {

	}

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") || collision.CompareTag("Object"))
		{
			if (wasTeleported == false)
			{
				Debug.Log(collision.tag);

				onTeleportation.Invoke(collision.gameObject);
			}
		}
	}

	protected void OnTriggerExit2D(Collider2D collision)
	{
		wasTeleported = false;
	}

	public void PlayerTeleport(GameObject teleportedObject)
	{
		Debug.Log(teleportedObject);

		wasTeleported = true;
		teleportedObject.transform.position = transform.position;
		// Reset the virtual cameras.
		GameplayCamera.Instance.SetCameras(PlayerCharacter.Instance.gameObject);
	}

	

	// Update is called once per frame
	void Update()
    {
        
    }
}
