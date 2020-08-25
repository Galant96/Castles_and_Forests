using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Mechanism
{
	[SerializeField]
	private bool wasOpen = false;

	[SerializeField, Header("Register to know if, player destroy or get a container with collectables.")]
	OnGetReward OnGetReward = null;

	[SerializeField]
	private int minRewardNumber = 1;
	[SerializeField]
	private int maxRewardNumber = 10;

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && GameManager.Instance.NumberOfKeys > 0 && wasOpen != true)
		{
			if (collision == null)
			{
				return;
			}

			if (collision.gameObject.GetComponent<PlayerCharacter>().IsDashing != false)
			{
				return;
			}

			OpenTreasure();
		}
	}
	
	public void OpenTreasure()
	{
		GameManager gameManager = GameManager.Instance;
		// Set buttons active
		gameManager.ActiveButton(gameManager.VideoGoldButton, true);
		gameManager.ActiveButton(gameManager.VideoLifeButton, true);
		animator.SetBool("Openning", true);
		GameManager.Instance.NumberOfKeys -= 1;
		wasOpen = true;

		OnGetReward.Invoke(wasOpen, transform.position, minRewardNumber, maxRewardNumber);
	}

	protected override void OnTriggerExit2D(Collider2D collision)
	{
		return;
	}
}
