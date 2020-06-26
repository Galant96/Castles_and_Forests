﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	private float throwPower = 25f;

	private List<GameObject> trajectoryPoints = null;

	private bool isObjectThrown = false;
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
		if (isObjectThrown)
		{
			return;
		}

		if(Input.GetMouseButtonDown(0))
		{
			isPressed = true;

			if (!objectModel)
			{
				CreateObject();
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			isPressed = false;

			if (!isObjectThrown)
			{
				ThrowObject();
			}
		}

		if (isPressed)
		{
			Vector3 force = GetForce(objectModel.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
			float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
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

		isObjectThrown = true;

		// TO DO - FIX THAT
		Destroy(objectModel, 3f);
	}

	private Vector2 GetForce(Vector3 fromPos, Vector3 toPos)
	{
		return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y) * throwPower);
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
		}
		else
		{
			return;
		}
	}

}
