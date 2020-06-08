using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
	[SerializeField]
	protected int value = 1;

	[SerializeField]
	protected string tagType = "Player";

	[SerializeField, Header("Register to know when a collectible item is hit.")]
	protected OnCollectibleHitEvent onCollectibleHit;

	private void OnTriggerEnter2D(Collider2D playerCollider)
	{
		if (playerCollider.tag == tagType)
		{
			onCollectibleHit.Invoke(value);
			Destroy(gameObject);
		}
	}

}
