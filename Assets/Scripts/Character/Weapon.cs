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
		if (isEnemy && weaponCollider.enabled == true)
		{
			Debug.Log(enemyCollider);
			enemyCollider.gameObject.GetComponent<Enemy>().EnemyTakesDamage(isEnemy);
		}
	}
}
