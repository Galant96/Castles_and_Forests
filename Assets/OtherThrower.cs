using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherThrower : MonoBehaviour
{
	[SerializeField]
	private GameObject bombPrefab = null;

	private GameObject bombModel = null;


	[SerializeField]
	private Transform throwerPoint = null;

	Vector2 swipeDirecion;


	private void Update()
	{
		swipeDirecion = SwipeDetector.Instance.SwipeData.EndPosition;
	}

	public void Throw(Vector2 direction)
	{
		Debug.Log(direction);
		Debug.Log(Camera.main.ScreenToWorldPoint(direction));

		Vector2 worldPos = Camera.main.ScreenToWorldPoint(direction);
		Debug.Log(worldPos);
		bombModel = Instantiate(bombPrefab, throwerPoint.position, Quaternion.identity) as GameObject;
		bombModel.GetComponent<Rigidbody2D>().velocity = worldPos;
	}
}
