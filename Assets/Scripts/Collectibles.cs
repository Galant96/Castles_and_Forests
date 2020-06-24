using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
	[SerializeField]
	private int value = 1;

	[SerializeField]
	private string tagType = "Player";

	[SerializeField]
	private string soundName = "change";

	[SerializeField, Header("Register to know when a collectible item is hit.")]
	protected OnCollectibleHitEvent onCollectibleHit;

	private void OnTriggerEnter2D(Collider2D playerCollider)
	{
		if (playerCollider.tag == tagType)
		{
			if (soundName != "change")
			{
				SoundManager.Instance.PlaySound(soundName);
			}

			onCollectibleHit.Invoke(value);
			Destroy(gameObject);
		}
	}
}
