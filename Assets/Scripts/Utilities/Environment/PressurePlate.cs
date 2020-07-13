using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Mechanism
{
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		base.OnTriggerEnter2D(collision);
	}

	protected override void OnTriggerExit2D(Collider2D collision)
	{
		base.OnTriggerExit2D(collision);
	}
}
