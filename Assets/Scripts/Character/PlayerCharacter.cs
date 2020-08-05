using UnityEngine;
using System.Collections;
using System;
//using UnityStandardAssets.CrossPlatformInput; // Get cross platform input

public class PlayerCharacter : BaseCharacter
{
	public static PlayerCharacter Instance { get; private set; }

	// Config
	[SerializeField]
	private float jumpSpeed = 5f;
	[SerializeField]
	private float climbSpeed = 5f;
	[SerializeField]
	Vector2 deathKick = new Vector2(25f, 25f);
	[SerializeField]
	Vector2 dashKick = new Vector2(50f, 0f);
	[SerializeField]
	float dashingTime = 1f;

	float controlThrow = 0f;
	public float ControlThrow { get => controlThrow; set => controlThrow = value; }

	private Vector2 playerVelocity;
	public Vector2 PlayerVelocity { get => playerVelocity; set => playerVelocity = value; }

	[SerializeField]
	private GameObject dashForm = null;

	[SerializeField]
	private Joystick joystick = null;

	[SerializeField]
	private BoxCollider2D myFeetBoxCollider = null;
	private CircleCollider2D weapon = null;

	private CharacterSoundKeeper soundKeeper = null;

	private bool isDashing = false;
	public bool IsDashing
	{
		get { return isDashing; }
	}

	private bool isAlive = true;
	public bool IsAlive
	{
		get { return isAlive; }
		private set { isAlive = value; }
	}



	// Events
	[SerializeField, Header("Register to know if character is spawned.")]
	OnCharacterSpawn onCharacterSpawn = null;
	// Start is called before the first frame update
	protected override void Start()
    {
		Instance = this;

		// Spawn character at the spawn point.
		onCharacterSpawn.Invoke(gameObject);

		base.Start();
		soundKeeper = GetComponent<CharacterSoundKeeper>();

		//myFeetBoxCollider = GetComponent<BoxCollider2D>();
		weapon = GetComponentInChildren<CircleCollider2D>();

		flippingSite = FlippingSite.right;

		ControlThrow = Mathf.Clamp(0f, -1f, 1f);
	}

	// Update is called once per frame
	private void FixedUpdate()
    {
		if (joystick == null)
		{
			Debug.Log("Is Found!");
			joystick = GameManager.Instance.GetComponentInChildren<Joystick>();
		}
		// If player is not alive return
		if (isAlive == false)
		{
			return;
		}

		// Do if player is dashing. 
		if (isDashing == true)
		{
			transform.parent = null;

			if (flippingSite == FlippingSite.right)
			{
				MyRigidbody2D.AddForce(dashKick * Speed, ForceMode2D.Force);
			}
			else if(flippingSite == FlippingSite.left)
			{
				MyRigidbody2D.AddForce(-dashKick * Speed, ForceMode2D.Force);
			}
		}

		Die();
		Move();
		FlipSprite();
		//Jump();
		Climb();
		// Attack(); // Set to button control

		GroundPlayer();
	}

	protected override void FlipSprite()
	{
		base.FlipSprite();
	}

	// Attack is performed by pushing the button
	public override void Attack()
	{
		if (isAlive != true)
		{
			return;
		}

		bool isPlayerStatic = MyRigidbody2D.velocity == Vector2.zero;

		if (isPlayerStatic)
		{
			if (weapon.enabled != true)
			{
				isDashing = false;
				EnableWeapon(true);
			}

			CharacterAnimator.SetTrigger("Attacking");
		}
	}

	public void EnableWeapon(bool isEnabled)
	{
		// Enable weapon's collider while attacking
		weapon.enabled = isEnabled;
	}

	public void DisableWeapon()
	{
		weapon.enabled = false;
	}

	public override void Die()
	{
		bool isPlayerTrapped = MyBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards", "Water"));
		if (isPlayerTrapped && isDashing != true)
		{
			isAlive = false;

			if (MyBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Water")))
			{
				soundKeeper.PlayCharacterSound("Splash"); // Play if the player fall into the water.
				MyRigidbody2D.velocity = Vector2.zero;
			}
			else
			{
				soundKeeper.PlayCharacterSound(soundKeeper.DeathSound[0]);
				MyRigidbody2D.velocity = deathKick;
			}

			CharacterAnimator.SetBool("Dying", true);

			// Process player death from by calling a method from the game manager instance
			GameManager.Instance.ProcessPlayerDeath();
		}
	}

	public override void Move()
	{
		
		//float controlThrow = joystick.Horizontal; // Value is between -1 to +1

		playerVelocity = new Vector2(controlThrow * Speed, MyRigidbody2D.velocity.y);
		MyRigidbody2D.velocity = playerVelocity;

		//// Set true if character is moving
		bool isHorizontalSpeed = Mathf.Abs(MyRigidbody2D.velocity.x) > Mathf.Epsilon;
		CharacterAnimator.SetBool("Running", isHorizontalSpeed);

	}

	private void Climb()
	{
		// Check if the player is climbing
		if (!MyBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
		{
			return;
		}

		float controlThrowVertical = 1f;

		if (controlThrowVertical > 0)
		{
			Vector2 climbVelocity = new Vector2(MyRigidbody2D.velocity.x, controlThrowVertical * climbSpeed);
			MyRigidbody2D.velocity = climbVelocity;
		}

	}

	public void Jump()
	{
		// Check if the player is grounded to prevent a double jump
		if (myFeetBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Objects")))
		{
			CharacterAnimator.SetBool("Jumping", true);
			CharacterAnimator.SetBool("Falling", false);

			Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
			MyRigidbody2D.velocity = jumpVelocity;
		}
	}

	/// <summary>
	///  Check if the player character is grounded or it is not and set an appropriate animation.
	/// </summary>
	private void GroundPlayer()
	{
		if (!myFeetBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Objects")))
		{
			CharacterAnimator.SetBool("Falling", true);
		}
		else
		{
			CharacterAnimator.SetBool("Falling", false);
		}

		CharacterAnimator.SetBool("Jumping", false);

	}

	// Restore player's life on the potion collision
	public void OnRestoreLife(int restoredLifes)
	{
		GameManager.Instance.PlayerLives += restoredLifes;
	}

	// Receive score
	public void OnGrandScore(int score)
	{
		GameManager.Instance.Score += score;
	}

	// Receive a key
	public void OnGetKey(int keyNumbers)
	{
		GameManager.Instance.NumberOfKeys += keyNumbers;
	}

	public void OnJumpPotion(int newJumpSpeed)
	{
		// Potion effect duration time
		float effectDurationTime = 3f;
		StartCoroutine(JumpPotionEffect(newJumpSpeed, effectDurationTime));
	}

	IEnumerator JumpPotionEffect(int newJumpSpeed, float effectDurationTime)
	{
		float originalJumpSpeed = jumpSpeed;
		jumpSpeed = newJumpSpeed; 
		yield return new WaitForSeconds(effectDurationTime);
		jumpSpeed = originalJumpSpeed;
	}

	public void OnCollectBomb(int addedBombs)
	{
		GameManager gameManager = GameManager.Instance;
		// Check if there is already max number of bombs in equpiment.
		bool isBombAdded = (gameManager.NumberOfBombs >= gameManager.MaxNumberOfBombs);

		if (isBombAdded != true)
		{
			gameManager.NumberOfBombs += addedBombs;
		}
		else
		{
			return;
		}
	}

	public void Dash()
	{
		if (isAlive != false)
		{
			StartCoroutine(StartDashing(isDashing));
		}
	}

	IEnumerator StartDashing(bool isPlayerDashing)
	{
		// Set to true.
		isDashing = !isPlayerDashing;

		if (transform.parent != null)
		{
			Instance.transform.parent = null;
		}

		if (isDashing != false)
		{
			// Disable all player's colliders
			MyBodyCollider2D.enabled = false;
			myFeetBoxCollider.enabled = false;

			// Freeze Y position to preven falling and jumping.
			MyRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

			// Disable the sprite renderer to dispaly a dash form.
			GetComponent<SpriteRenderer>().enabled = false;
			dashForm.SetActive(true);

			// Play an animation to set the cinemachine camera.
			CharacterAnimator.SetBool("Dashing", true);


			// Disactivate interface for the time of dashing.
			GameManager.Instance.InterfaceButtons.SetActive(false);

			// Wait the time of dashing.
			yield return new WaitForSeconds(dashingTime);
		}
		
		// Restore the original status.

		GameManager.Instance.InterfaceButtons.SetActive(true);
		MyBodyCollider2D.enabled = true;
		myFeetBoxCollider.enabled = true;

		MyRigidbody2D.constraints = RigidbodyConstraints2D.None;
		MyRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

		GetComponent<SpriteRenderer>().enabled = true;
		dashForm.SetActive(false);
		CharacterAnimator.SetBool("Dashing", false);

		// Set to false.
		isDashing = isPlayerDashing;

	}

}


