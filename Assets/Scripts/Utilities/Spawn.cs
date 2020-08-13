using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
	private GameObject character = null;

	public void SpawnCharacter(GameObject _character)
	{
		character = _character;
		character.transform.position = transform.position;
		character.SetActive(false);
		GameManager.Instance.EnableGameCanvas(true);

	}

	public void ActiveSpawnedCharacter()
	{
		if (character != null)
		{
			character.SetActive(true);
		}
	}
}
