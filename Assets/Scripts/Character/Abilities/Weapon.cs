using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	private CircleCollider2D weaponCollider;

	private void Start()
	{
		weaponCollider = GetComponent<CircleCollider2D>();
	}

	private void OnTriggerEnter2D(Collider2D enemyCollider)
	{
		bool isEnemy = weaponCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"));
		Enemy enemy = enemyCollider.gameObject.GetComponent<Enemy>();

		if (isEnemy && weaponCollider.enabled == true && enemy != null)
		{
			if (enemy.IsAlive != false)
			{
				enemy.IsAlive = false;
				enemy.EnemyTakesDamage(isEnemy);
				Debug.Log(enemyCollider);
			}
		}
	}
}
