using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCamera : MonoBehaviour
{
	[SerializeField]
	private Camera mainCamera;

	private float size = 5f;
	private float startSize;

	[SerializeField]
	private GameObject background = null;

	private Vector2 scaleVector;

    // Start is called before the first frame update
    void Start()
    {
		mainCamera = Camera.main;
		size = mainCamera.orthographicSize;
		startSize = size;

		if (background != null)
		{
			scaleVector = background.transform.localScale;
		}
	}

    // Update is called once per frame
    void Update()
    {
		size = mainCamera.orthographicSize;

		if (background != null)
		{
			ResizeBackground();
		}
	}

	private void ResizeBackground()
	{
		size -= startSize;
		size += 1f;
		Debug.Log(size);

		background.transform.localScale = new Vector2(scaleVector.x / size, scaleVector.y / size);
	}
}
