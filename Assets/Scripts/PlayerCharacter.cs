using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput; // Get cross platform input

public class PlayerCharacter : BaseCharacter
{
	// Config
	[SerializeField]
	private float jumpSpeed = 5f;


	// Start is called before the first frame update
	protected override void Start()
    {
		base.Start();
    }

    // Update is called once per frame
    void Update()
    {
		if (MyRigidbody2D != null)
		{
			Move();
			FlipSprite();
			Jump();

			Debug.Log(MyRigidbody2D.velocity);
		}
	}

	protected override void FlipSprite()
	{
		base.FlipSprite();
	}

	public override void Attack()
	{
		throw new System.NotImplementedException();
	}

	public override void Die()
	{
		throw new System.NotImplementedException();
	}

	public override void Move()
	{
		float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // Value is between -1 to +1
		Vector2 playerVelocity = new Vector2(controlThrow * Speed, MyRigidbody2D.velocity.y);
		MyRigidbody2D.velocity = playerVelocity;

		// Set true if character is moving
		bool isHorizontalSpeed = Mathf.Abs(MyRigidbody2D.velocity.x) > Mathf.Epsilon;
		Animator.SetBool("Running", isHorizontalSpeed);
	}

	private void Jump()
	{
		// Check if the player is grounded to prevent a double jump
		if (!MyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			Animator.SetBool("Jumping", true);
			return;
		}
		else
		{
			Animator.SetBool("Jumping", false);

		}


		if (CrossPlatformInputManager.GetButtonDown("Jump"))
		{
			Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
			MyRigidbody2D.velocity = jumpVelocity;

		}
	}

}
