using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour, IBaseCharacter
{
	// Config
	[SerializeField]
	private float speed = 10f;
	public float Speed { get => speed; set => speed = value; }

	// State
	// TO DO

	// Cached component references
	private Rigidbody2D myRigidbody2D;
	public Rigidbody2D MyRigidbody2D { get => myRigidbody2D;}

	private Animator animator;
	public Animator Animator { get => animator;}

	private Collider2D myCollider2D;
	public Collider2D MyCollider2D { get => myCollider2D; }

	// Methods
	protected virtual void Start()
	{
		// Caching a variable
		myRigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		myCollider2D = GetComponent<Collider2D>();
	}

	protected virtual void FlipSprite()
	{
		// Set true if character is moving
		bool isHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;

		if (isHorizontalSpeed)
		{
			// Scale x -1 or 1 to flip the character
			// Mathf.Sign(f) returns 1 if f is pos or 0, and -1 when f is neg
			transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1f);
		}
	}

	public abstract void Attack();
	public abstract void Die();
	public abstract void Move();
}
