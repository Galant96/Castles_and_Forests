using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour, IBaseCharacter
{
	// Config
	[SerializeField]
	private float speed = 10f;
	public float Speed { get => speed; set => speed = value; }

	// State
	// TO DO
	protected enum FlippingSite
	{
		left = -1,
		right = 1
	}

	protected FlippingSite flippingSite = new FlippingSite();

	// Cached component references
	private Rigidbody2D myRigidbody2D;
	public Rigidbody2D MyRigidbody2D { get => myRigidbody2D; set => myRigidbody2D = value; }

	private Animator animator;
	public Animator CharacterAnimator { get => animator;}

	private CapsuleCollider2D myBodyCollider2D;
	public CapsuleCollider2D MyBodyCollider2D { get => myBodyCollider2D; }

	// Methods
	protected virtual void Start()
	{
		// Caching a variable
		myRigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		myBodyCollider2D = GetComponent<CapsuleCollider2D>();
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

			if (transform.localScale.x == 1)
			{
				flippingSite = FlippingSite.right;
			}
			else if (transform.localScale.x == -1)
			{
				flippingSite = FlippingSite.left;
			}
		}
	}

	public abstract void Attack();
	public abstract void Die();
	public abstract void Move();
}
