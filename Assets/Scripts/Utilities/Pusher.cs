﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Pusher Class is responsible for pushing game objects in various directions

public class Pusher : MonoBehaviour
{
	// Create finite set of pushing directions

	public enum PushingDirections
	{
		up,		// Push up
		down,	// Push down
		left,	// Push left
		right,	// Push right
	}

	[SerializeField]
	private PushingDirections pushingDirections = new PushingDirections();

	[SerializeField]
	private PushingDirections pullingDirection = new PushingDirections();

	[SerializeField]
	private float pushingSpeed = 2f;

	private Vector3 pushingVector;
	private Vector3 pullingVector;

	bool changeDirection = false;

	private Rigidbody2D pusherRigidbody = null;
	[SerializeField]
	private BoxCollider2D pusherCollider = null;

    // Start is called before the first frame update
    void Start()
    {
		pusherRigidbody = GetComponent<Rigidbody2D>();

		// Set the pushing Vector
		switch (pushingDirections)
		{
			case PushingDirections.up:
				pushingVector = Vector3.up;
				break;
			case PushingDirections.down:
				pushingVector = Vector3.down;
				break;
			case PushingDirections.left:
				pushingVector = Vector3.left;
				break;
			case PushingDirections.right:
				pushingVector = Vector3.right;
				break;
			default:
				break;
		}

		switch (pullingDirection)
		{
			case PushingDirections.up:
				pullingVector = Vector3.up;
				break;
			case PushingDirections.down:
				pullingVector = Vector3.down;
				break;
			case PushingDirections.left:
				pullingVector = Vector3.left;
				break;
			case PushingDirections.right:
				pullingVector = Vector3.right;
				break;
			default:
				break;
		}
	}

	private void FixedUpdate()
	{
		if (pusherCollider != null)
		{
			if (pusherCollider.IsTouchingLayers(LayerMask.GetMask("Player")) != false)
			{
				PlayerCharacter.Instance.transform.parent = transform;
			}

			Push();
		}
		
	}

	private void Push()
	{
		if (changeDirection != true)
		{
			transform.Translate(pullingVector * pushingSpeed * Time.deltaTime);
		}
		else
		{
			transform.Translate(pushingVector * pushingSpeed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (pusherCollider != null)
		{
			if (pusherCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) != false)
			{
				changeDirection = !changeDirection;
			}
		}
		
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") == true)
		{
			PlayerCharacter.Instance.transform.parent = null;
		}
	}

}
