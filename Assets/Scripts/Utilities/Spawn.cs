using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
	private GameObject character = null;
	[SerializeField]
	private float canvasReadyCountdown = 3f;

	public void SpawnCharacter(GameObject _character)
	{
		character = _character;
		character.transform.position = transform.position;
		character.SetActive(false);
		StartCoroutine(WaitForCanvas(canvasReadyCountdown));

	}

	public void ActiveSpawnedCharacter()
	{
		if (character != null)
		{
			character.SetActive(true);
		}
	}

	IEnumerator WaitForCanvas(float time)
	{
		GameManager.Instance.EnableGameCanvas(false);

		yield return new WaitForSeconds(time);

		GameManager.Instance.EnableGameCanvas(true);

	}
}
