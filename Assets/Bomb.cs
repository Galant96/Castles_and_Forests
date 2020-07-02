using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	[SerializeField]
	private float detonationTime = 3f;

	[SerializeField]
	private BoxCollider2D explosionCollider = null;

	[SerializeField]
	private Animator bombAnimator = null;

	[SerializeField]
	private string explosionSoundName = "change";

	[SerializeField]
	private string fuseSoundName = "change";

	[SerializeField]
	private GameObject sparkles = null;

	private void Start()
	{
		explosionCollider = GetComponent<BoxCollider2D>();
		explosionCollider.enabled = false;
		bombAnimator = GetComponent<Animator>();

		sparkles.SetActive(false);

		if (explosionCollider != null && bombAnimator != null)
		{
			StartCoroutine(StartCountDown());
		}
		else
		{
			Debug.Log("Explosion collider and/or bomb animator are not assigned to the object.");
		}
	}

	// Start to countdown and the destroy a bomb
	private IEnumerator StartCountDown()
	{
		if (fuseSoundName != null && fuseSoundName != "change")
		{
			SoundManager.Instance.PlaySound(fuseSoundName);
		}
		yield return new WaitForSeconds(detonationTime);
		bombAnimator.SetTrigger("Detonating");
		if (explosionSoundName != null && explosionSoundName != "change")
		{
			SoundManager.Instance.PlaySound(explosionSoundName);
		}
	}

	public void StartSparkles()
	{
		sparkles.SetActive(true);
	}

	public void TakeDamage()
	{
		sparkles.SetActive(false);
		explosionCollider.enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D enemyCollider)
	{
		if (explosionCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
		{
			if (enemyCollider.CompareTag("Enemy"))
			{
				enemyCollider.GetComponent<Enemy>().Die();
			}
		}
	}

	public void DestroyBomb()
	{
		Destroy(gameObject);
	}

}
