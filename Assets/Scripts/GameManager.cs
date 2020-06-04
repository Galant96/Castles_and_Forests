using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
	public static GameManager Instance { get; private set; }

	[SerializeField]
	private int playerLives = 3;

	[SerializeField]
	private LevelLoader levelLoader;

	private void Awake()
	{
		SetUpSingelton();
		if (levelLoader == null)
		{
			levelLoader = FindObjectOfType<LevelLoader>();
		}
	}

	private void SetUpSingelton()
	{

		// If instance of the object is null then keep the previous object
		// else destroy the new object and keep "the old" one
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


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	public void ProcessPlayerDeath()
	{
		
		if (playerLives > 1)
		{
			
			TakeLife();
		}
		else
		{
			ResetGameSession();
		}

	}

	IEnumerator PrepareToLoadingSceneAgain()
	{
		playerLives -= 1;
		yield return new WaitForSeconds(2);
		// Restart the level 
		levelLoader.LoadTheSameSceneAgain();
	}

	private void TakeLife()
	{
		StartCoroutine(PrepareToLoadingSceneAgain());	
	}

	private void ResetGameSession()
	{
		// Back to the main menu
		levelLoader.LoadMainMenu();
		// Destroy the current Game Manager
		Destroy(gameObject);
	}
}
