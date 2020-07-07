using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	[SerializeField]
	private float detonationTime = 3f;

	[SerializeField]
	private BoxCollider2D explosionLeftCollider = null;
	[SerializeField]
	private BoxCollider2D explosionRightCollider = null;

	[SerializeField]
	private Animator bombAnimator = null;

	[SerializeField]
	private string explosionSoundName = "change";

	[SerializeField]
	private string fuseSoundName = "change";

	[SerializeField]
	private GameObject sparkles = null;

	[SerializeField]
	private float explosionPowerX = 1000f;
	[SerializeField]
	private float explosionPowerY = 1000f;

	private void Start()
	{
		explosionLeftCollider.enabled = false;
		explosionRightCollider.enabled = false;

		bombAnimator = GetComponent<Animator>();

		sparkles.SetActive(false);

		if (explosionLeftCollider != null && explosionRightCollider != null && bombAnimator != null)
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
		explosionLeftCollider.enabled = true;
		explosionRightCollider.enabled = true;

		sparkles.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D otherCollider)
	{
		bool isObstacleOnLeft = explosionLeftCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Objects"));
		bool isObstacleOnRight = explosionRightCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Objects"));

		if (isObstacleOnLeft || isObstacleOnRight)
		{
			if (otherCollider.CompareTag("Enemy"))
			{
				otherCollider.GetComponent<Enemy>().Die();
			}

			if (otherCollider.CompareTag("Object"))
			{
				if (otherCollider.GetComponent<Rigidbody2D>() != null)
				{
					Debug.Log(otherCollider.gameObject);
					if (isObstacleOnRight)
					{
						otherCollider.GetComponent<Rigidbody2D>().AddForce(new Vector2(explosionPowerX, explosionPowerY));
					}
					else if(isObstacleOnLeft)
					{
						otherCollider.GetComponent<Rigidbody2D>().AddForce(new Vector2(-explosionPowerX, explosionPowerY));
					}
				}
			}
		}
	}

	public void DestroyBomb()
	{
		Destroy(gameObject);
	}

}
