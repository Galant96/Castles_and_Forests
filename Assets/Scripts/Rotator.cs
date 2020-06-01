using UnityEngine;

public class Rotator : MonoBehaviour
{
	// Create finite set of rotatio axes the game object can rotate

	public enum RotationAxes
	{
		x, // Rotate around x-axix
		y, // Rotate around x-axix
		z, // Rotate around x-axix
	}

	public enum Direction
	{
		anticlockwise, // Rotate around x-axix
		clockwise, // Rotate around x-axix
	}

	[SerializeField]
	RotationAxes rotationAxes = new RotationAxes();

	[SerializeField]
	Direction direction = new Direction();

	[SerializeField]
	private float rotationSpeed = 10f;

	Vector3 rotationVector;

	// Start is called before the first frame update
	void Start()
	{
		// Set the rotation vector
		switch (rotationAxes)
		{
			case RotationAxes.x:
				rotationVector = Vector3.right;
				break;
			case RotationAxes.y:
				rotationVector = Vector3.up;
				break;
			case RotationAxes.z:
				rotationVector = Vector3.forward;
				break;
			default:
				rotationVector = Vector3.zero;
				break;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (direction == Direction.anticlockwise)
		{
			transform.RotateAround(transform.position, rotationVector, rotationSpeed);
		}
		else
		{
			transform.RotateAround(transform.position, -rotationVector, rotationSpeed);
		}
	}

}
