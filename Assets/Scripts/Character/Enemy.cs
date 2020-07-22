using UnityEngine;

public class Enemy : BaseCharacter
{
	// Score value for an enemy
	[SerializeField]
	private int value = 100;

	private BoxCollider2D wallCollider = null;
	private CharacterSoundKeeper soundKeeper = null;
	private bool isAlive = true;
	public bool IsAlive { get => isAlive; set => isAlive = value; }

	[SerializeField, Header("Register to know if enemy is died, and grant score to the player.")]
	OnScoreEvent onScore = null;

	// Start is called before the first frame update
	protected override void Start()
    {
		base.Start();
		wallCollider = GetComponent<BoxCollider2D>();
		soundKeeper = GetComponent<CharacterSoundKeeper>();
    }

    // Update is called once per frame
    void Update()
    {

		if (isAlive != true)
		{
			return;
		}

		Move();
    }

	public override void Attack()
	{
		throw new System.NotImplementedException();
	}

	public override void Die()
	{
		// Make sure that enemy is hit only once
		MyBodyCollider2D.enabled = false;

		MyRigidbody2D.velocity = Vector2.zero;

		// Play death animation and sound
		soundKeeper.PlayRandomWeaponImpactSound();
		CharacterAnimator.SetTrigger("Dying");

		if (onScore != null)
		{
			// Grant score for the player
			onScore.Invoke(value);
		}
		
		Destroy(gameObject, 0.5f);
	}
		
	public void EnemyTakesDamage(bool isHit)
	{
		if (isHit != false)
		{
			Die();
		}
	}

	public override void Move()
	{
		if (IsFacingRight())
		{
			MyRigidbody2D.velocity = new Vector2(Speed, MyRigidbody2D.velocity.y);
		}
		else
		{
			MyRigidbody2D.velocity = new Vector2(-Speed, MyRigidbody2D.velocity.y);
		}
	}

	// Check if enemy is facing right
	private bool IsFacingRight()
	{
		// The enemy is facing right if local scale x is greater than 0
		return transform.localScale.x > 0;
	}

	private void OnTriggerExit2D(Collider2D wall)
	{
		if (wall.CompareTag("Ground"))
		{
			FlipSprite();
		}
	}

	protected override void FlipSprite()
	{
		// Calculate and flip the enemy sprite
		transform.localScale = new Vector2(-(Mathf.Sign(MyRigidbody2D.velocity.x)), 1f);
	}

}
