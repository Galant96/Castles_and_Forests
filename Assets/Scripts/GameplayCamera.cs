using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameplayCamera : MonoBehaviour
{
	public static GameplayCamera Instance { get; private set; }

	[SerializeField]
	private Camera mainCamera;

	[SerializeField]
	private List<CinemachineVirtualCamera> cinemachineVirtualCameras = new List<CinemachineVirtualCamera>();
	public List<CinemachineVirtualCamera> CinemachineVirtualCameras { get => cinemachineVirtualCameras; set => cinemachineVirtualCameras = value; }

	private float size = 5f;
	private float startSize;

	[SerializeField]
	private GameObject background = null;

	private Vector2 scaleVector;

    // Start is called before the first frame update
    void Start()
    {
		Instance = this;

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
		background.transform.localScale = new Vector2(scaleVector.x / size, scaleVector.y / size);
	}

	/// <summary>
	/// Set a new virtual cameras' target and reset the mechanism.
	/// </summary>
	/// <param name="gameObject"></param>
	public void SetCameras(GameObject gameObject)
	{
		foreach (CinemachineVirtualCamera cinemachineVirtualCamera in cinemachineVirtualCameras)
		{
			cinemachineVirtualCamera.enabled = false;
			cinemachineVirtualCamera.Follow = gameObject.transform;
			cinemachineVirtualCamera.enabled = true;
		}
	}

}
