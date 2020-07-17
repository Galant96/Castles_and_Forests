using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : MonoBehaviour
{
	[SerializeField]
	private float colliderHeight = 3.75f;

	[SerializeField]
	private float colliderWidth = 3.75f;

	[SerializeField]
	private float particlesLifetime = 0.7f;

	private ParticleSystem.MainModule whirlwindParticles;

	private BoxCollider2D whirlwindCollider;

	private void Awake()
	{
		whirlwindParticles = GetComponent<ParticleSystem>().main;

		whirlwindCollider = GetComponent<BoxCollider2D>();

		whirlwindParticles.startLifetime = particlesLifetime;

		Vector2 newSize = new Vector2(colliderWidth, colliderHeight);

		whirlwindCollider.size = newSize;
	}

}
