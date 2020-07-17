using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Mechanism
{
	[SerializeField]
	private bool wasOpen = false;

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && GameManager.Instance.NumberOfKeys > 0 && wasOpen != true)
		{
			GameManager gameManager = GameManager.Instance;
			// Set buttons active
			gameManager.ActiveButton(gameManager.VideoGoldButton, true);
			gameManager.ActiveButton(gameManager.VideoLifeButton, true);
			animator.SetBool("Openning", true);
			GameManager.Instance.NumberOfKeys -= 1;
			Debug.Log("Treasure");
			onMechanismWork.Invoke(wasOpen);
			wasOpen = true;
		}
	}

	protected override void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			animator.SetBool("Openning", false);
		}
	}
}
