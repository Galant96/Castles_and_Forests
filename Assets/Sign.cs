using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
	[SerializeField]
	private TextInfo textInfo = null;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			GameManager.Instance.DisplaySignInfo(textInfo);
		}
	}
}
