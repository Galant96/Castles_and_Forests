using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Thrower : MonoBehaviour
{
	[SerializeField]
	private GameObject bombPrefab = null;

	[SerializeField]
	private GameObject objectModel = null;

	[SerializeField]
	private GameObject trajectoryPointPrefab = null;
	[SerializeField]
	private Transform throwPoint = null;

	[SerializeField]
	private int numberOfTrajectoryPoints = 30;

	[SerializeField]
	private Vector2 throwPower = new Vector2(5f, 10f);

	private List<GameObject> trajectoryPoints = null;

	private bool isPressed = false;


	private void Start()
	{
		trajectoryPoints = new List<GameObject>();

		for (int i = 0; i < numberOfTrajectoryPoints; i++)
		{
			GameObject dot = Instantiate(trajectoryPointPrefab, throwPoint) as GameObject;
			dot.SetActive(false);
			trajectoryPoints.Insert(i, dot);
		}
	}

	private void FixedUpdate()
	{

		if (Input.touchCount > 0 && PlayerCharacter.Instance.PlayerVelocity.x == 0)
		{
			Touch touch = Input.GetTouch(0);

			switch(touch.phase)
			{
				case TouchPhase.Began:
					break;
				case TouchPhase.Moved:
					isPressed = true;

					if (!objectModel)
					{
						CreateObject();
					}
					break;
				case TouchPhase.Ended:
					isPressed = false;

					if (objectModel != null)
					{
						ThrowObject();
					}
					break;

				default:
					break;
			}
		}

		if (isPressed && objectModel != null)
		{
			Vector3 force = GetForce(objectModel.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
			float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg; // TO DO IMPLEMENT LOOK UP TABLE
			throwPoint.eulerAngles = new Vector3(0, 0, angle);
			setTrajectoryPoints(throwPoint.position, force / objectModel.GetComponent<Rigidbody2D>().mass);
		}
		else
		{
			for (int i = 0; i < numberOfTrajectoryPoints; i++)
			{
				trajectoryPoints[i].SetActive(false);
			}
		}
	}
	

	private void setTrajectoryPoints(Vector3 pointStartPosition, Vector3 pointVelocity)
	{
		float velocity = Mathf.Sqrt((pointVelocity.x * pointVelocity.x) + (pointVelocity.y * pointVelocity.y));
		float angle = Mathf.Rad2Deg * (Mathf.Atan2(pointVelocity.y, pointVelocity.x));
		float time = 0;

		time += 0.1f;

		for (int i = 0; i < numberOfTrajectoryPoints; i++)
		{
			float dx = velocity * time * Mathf.Cos(angle * Mathf.Deg2Rad);
			float dy = velocity * time * Mathf.Sin(angle * Mathf.Deg2Rad) 
				- ((Physics2D.gravity.magnitude * time * time) * 0.5f);

			Vector3 pos = new Vector3(pointStartPosition.x + dx, pointStartPosition.y + dy, 2);
			trajectoryPoints[i].transform.position = pos;
			trajectoryPoints[i].SetActive(true);
			trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pointVelocity.y - (Physics.gravity.magnitude) * time, pointVelocity.x) * Mathf.Rad2Deg);
			time += 0.1f;
		}
	}

	private void ThrowObject()
	{
		objectModel.SetActive(true);
		objectModel.GetComponent<Rigidbody2D>().AddForce(GetForce(objectModel.transform.position,
		Camera.main.ScreenToWorldPoint(Input.mousePosition)), ForceMode2D.Impulse);

		// TO DO - FIX THAT
		Destroy(objectModel, 3f);

		PlayerCharacter.Instance.GetComponent<Animator>().SetBool("Throwing", false);

		// Reset the thrower
		objectModel = null;
	}

	private Vector2 GetForce(Vector3 fromPos, Vector3 toPos)
	{
		return ((new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * throwPower);
	}

	private void CreateObject()
	{

		GameManager gameManager = GameManager.Instance;

		// Check if there is any bombs in equpiment.
		bool isBombThrown = (gameManager.NumberOfBombs <= 0);

		if (isBombThrown != true)
		{
			gameManager.NumberOfBombs--;

			objectModel = Instantiate(bombPrefab, throwPoint.position, Quaternion.identity) as GameObject;

			objectModel.SetActive(false);

			PlayerCharacter.Instance.GetComponent<Animator>().SetBool("Throwing", true);
		}
		else
		{
			return;
		}
	}

	private bool IsPointerOverUIObject()
	{
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
}
