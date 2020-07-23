using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactHandler : Mechanism
{
	[SerializeField]
	private bool isImpact = false;

	[SerializeField, Header("Register to know if, player destroy or get a container with collectables.")]
	OnGetReward OnGetReward = null;

	[SerializeField]
	private int minRewardNumber = 1;
	[SerializeField]
	private int maxRewardNumber = 3;

	[SerializeField]
	private ParticleSystem particle = null;

	[SerializeField]
	private string impactSoundName = "";

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Weapon") || collision.CompareTag("Object"))
		{
			isImpact = !isImpact;
			if (animator != null)
			{
				animator.SetBool("Pressing", isImpact);
			}

			onMechanismWork.Invoke(isImpact);
			OnGetReward.Invoke(isImpact, transform.position, minRewardNumber, maxRewardNumber);

			GetComponent<Collider2D>().enabled = false;
			PlayMechanismSound(impactSoundName);
			particle.Play();
			GetComponent<SpriteRenderer>().enabled = false;
			Destroy(gameObject, 2f);
		}
	}

	protected override void OnTriggerExit2D(Collider2D collision)
	{
		return;
	}
}
