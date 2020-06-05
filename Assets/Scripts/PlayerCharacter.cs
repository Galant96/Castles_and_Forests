using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput; // Get cross platform input

public class PlayerCharacter : BaseCharacter
{
	// Config
	[SerializeField]
	private float jumpSpeed = 5f;
	[SerializeField]
	private float climbSpeed = 5f;
	[SerializeField]
	Vector2 deathKick = new Vector2(25f, 25f);

	[SerializeField]
	private Joystick joystick = null;

	private BoxCollider2D myFeetBoxCollider = null;
	private CircleCollider2D weapon = null;

	private bool isAlive = true;

	// Events
	//Register to know when player is died"
	private OnPlayerHit onPlayerHit = null;

	private void Awake()
	{
		GameManager gameManager = FindObjectOfType<GameManager>();
		onPlayerHit = new OnPlayerHit();
		onPlayerHit.AddListener(() => gameManager.ProcessPlayerDeath());
	}

	// Start is called before the first frame update
	protected override void Start()
    {
		base.Start();
		if (joystick == null)
		{
			joystick = FindObjectOfType<Joystick>();
		}
		myFeetBoxCollider = GetComponent<BoxCollider2D>();
		weapon = GetComponentInChildren<CircleCollider2D>();
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
		// If player is not alive return
		if (isAlive == false)
		{
			return;
		}

		Die();
		Move();
		FlipSprite();
		Jump();
		Climb();
		// Attack(); // Set to button control
		Debug.Log(MyRigidbody2D.velocity);
	}

	protected override void FlipSprite()
	{
		base.FlipSprite();
	}

	// Attack is performed by pushing the button
	public override void Attack()
	{
		bool isPlayerStatic = MyRigidbody2D.velocity == Vector2.zero;

		if (isPlayerStatic)
		{
			Animator.SetTrigger("Attacking");
		}
	}

	// Enable or disable weapon via animation events
	public void EnableWeapon()
	{
		// Enable weapon's collider while attacking
		weapon.enabled = true;
	}

	public void DisableWeapon()
	{
		// Disable weapon's collider while attacking
		weapon.enabled = false;
	}


	public override void Die()
	{
		if (MyBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
		{
			isAlive = false;

			MyRigidbody2D.velocity = deathKick;
			Animator.SetBool("Dying", true);
			MyBodyCollider2D.enabled = false;

			if (onPlayerHit != null)
			{
				// Invoke the event of the player's death from the game manager
				onPlayerHit.Invoke();
			}	
		}
	}

	public override void Move()
	{
		float controlThrow = joystick.Horizontal; // Value is between -1 to +1

		Vector2 playerVelocity = new Vector2(controlThrow * Speed, MyRigidbody2D.velocity.y);
		MyRigidbody2D.velocity = playerVelocity;

		// Set true if character is moving
		bool isHorizontalSpeed = Mathf.Abs(MyRigidbody2D.velocity.x) > Mathf.Epsilon;
		Animator.SetBool("Running", isHorizontalSpeed);
	}

	private void Climb()
	{
		// Check if the player is climbing
		if (!myFeetBoxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
		{
			return;
		}

		float controlThrow = joystick.Vertical;

		if (controlThrow > 0)
		{
			Vector2 climbVelocity = new Vector2(MyRigidbody2D.velocity.x, controlThrow * climbSpeed);
			MyRigidbody2D.velocity = climbVelocity;
		}

	}

	private void Jump()
	{
		// Check if the player is grounded to prevent a double jump
		if (!myFeetBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			bool playerHasVerticalSpeed = MyRigidbody2D.velocity.y > 0;

			Debug.Log(MyRigidbody2D.velocity.y);

			if (playerHasVerticalSpeed)
			{
				Animator.SetBool("Jumping", true);
				Animator.SetBool("Falling", false);
				Animator.SetBool("Running", false);

			}
			else
			{
				Animator.SetBool("Jumping", false);
				Animator.SetBool("Falling", true);
				Animator.SetBool("Running", false);

			}

			return;
		}
		else
		{
			Animator.enabled = true;
			Animator.SetBool("Jumping", false);
			Animator.SetBool("Falling", false);
		}

		float verticalMovement = joystick.Vertical;

		if (verticalMovement >= 0.5f)
		{
			Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
			MyRigidbody2D.velocity = jumpVelocity;
		}
	}

	public void PauseJumping()
	{
		Animator.enabled = false;
	}
}


