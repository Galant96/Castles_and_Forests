using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Mechanism
{
	[SerializeField]
	private bool isPressed = false;
	
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Weapon") || collision.CompareTag("Object"))
		{
			isPressed = !isPressed;
			animator.SetBool("Pressing", isPressed);
			onMechanismWork.Invoke(isPressed);
		}
	}

	protected override void OnTriggerExit2D(Collider2D collision)
	{
		return;
	}
}
