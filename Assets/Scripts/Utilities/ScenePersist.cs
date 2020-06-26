using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
	public static ScenePersist Instance { get; private set; }

	private LevelLoader levelLoader;

	private int startingSceneIndex = -1;

	private void Awake()
	{
		SetUpSingelton();
	}

	// Start is called before the first frame update
	void Start()
    {

		if (levelLoader == null)
		{
			levelLoader = GameManager.Instance.GetComponentInChildren<LevelLoader>();
		}

		// Get the starting scene index
		if (levelLoader != null)
		{
			startingSceneIndex = levelLoader.CurrentSceneIndex;
		}

	}

	// Update is called once per frame
	void Update()
    {
		if (levelLoader.CurrentSceneIndex != startingSceneIndex)
		{
			Destroy(gameObject);
		}
	}

	private void SetUpSingelton()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
