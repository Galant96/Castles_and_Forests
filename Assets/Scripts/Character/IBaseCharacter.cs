using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define basics mechanics, which any character can perform in the game
public interface IBaseCharacter
{
	void Move();

	void Attack();

	void Die();
}
