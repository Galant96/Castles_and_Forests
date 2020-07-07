using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for running various mechanism such as doors, press plates or levers in the game.
/// </summary>
public class Mechanism : MonoBehaviour
{
	[SerializeField]
	private Animator animator = null;

	[SerializeField, Header("Register to know, when the mechanism is on.")]
	private OnMechanismEvent onMechanismWork = null;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") || collision.CompareTag("Enemy") || collision.CompareTag("Object"))
		{
			animator.SetBool("Pressing", true);
			onMechanismWork.Invoke(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") || collision.CompareTag("Enemy") || collision.CompareTag("Object"))
		{
			animator.SetBool("Pressing", false);

			onMechanismWork.Invoke(false);
		}
	}

	public void PlayMechanismSound(string soundName)
	{
		SoundManager.Instance.PlaySound(soundName);
	}
}
