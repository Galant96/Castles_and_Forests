using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Mechanism
{
	[SerializeField]
	private bool isOpen = false;

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && GameManager.Instance.NumberOfKeys > 0)
		{
			animator.SetBool("Openning", true);
			GameManager.Instance.NumberOfKeys -= 1;
			Debug.Log("Treasure");
			onMechanismWork.Invoke(isOpen);
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
