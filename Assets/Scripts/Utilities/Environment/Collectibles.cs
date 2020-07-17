using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
	[SerializeField]
	private int value = 1;
	public int Value { get => value; private set => this.value = value; }

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

			InvokeOnCollectibleHit(1);

			Destroy(gameObject);
		}
	}

	public void InvokeOnCollectibleHit(int multiplayer)
	{
		onCollectibleHit.Invoke(value * multiplayer);
	}
}
