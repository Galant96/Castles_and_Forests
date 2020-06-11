using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{ 
	public static GameManager Instance { get; private set; }

	[SerializeField]
	private LevelLoader levelLoader;

	// UI
	[SerializeField]
	private TextMeshProUGUI scoreText = null;

	[SerializeField]
	private TextMeshProUGUI playerLivesText = null;

	[SerializeField]
	private TextMeshProUGUI timeText = null;

	[SerializeField]
	private int playerLives = 3;

	public int PlayerLives { get { return playerLives; } set { playerLives = value; } }

	public float Time { get; private set; } = 0f;

	public int Score { get; set; } = 0;

	private void Awake()
	{
		SetUpSingelton();
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

    // Update is called once per frame
    void Update()
    {
		if (levelLoader == null)
		{
			levelLoader = FindObjectOfType<LevelLoader>();
		}

		// Manage UI
		DisplayLife();
		DisplayScore();
		DisplayTime();

	}

	public void ProcessPlayerDeath()
	{
		if (PlayerLives > 1)
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
		PlayerLives -= 1;
		yield return new WaitForSeconds(2);
		// Restart the level 
		levelLoader.LoadTheSameSceneAgain();
	}

	private void TakeLife()
	{
		StartCoroutine(PrepareToLoadingSceneAgain());	
	}

	private void DisplayLife()
	{
		if (playerLivesText.text != null)
		{
			playerLivesText.text = PlayerLives.ToString();
		}
	}

	private void DisplayScore()
	{
		if (scoreText != null)
		{
			scoreText.text = Score.ToString();
		}
	}

	private void DisplayTime()
	{
		if (timeText != null)
		{
			// Get time from the system
			Time += UnityEngine.Time.deltaTime;

			int minutesInt = (int)(Time / 60);
			string minutes = (minutesInt.ToString());

			if (minutesInt < 10)
			{
				minutes = "0" + minutes;
			}

			int secondsInt = (int)(Mathf.Round(Time % 60));
			string seconds = secondsInt.ToString();

			if (secondsInt < 10)
			{
				seconds = "0" + seconds;
			}

			timeText.text = minutes + ":" + seconds; 
		}
	}

	private void ResetGameSession()
	{
		// Back to the main menu
		levelLoader.LoadMainMenu();
		// Destroy the current Game Manager
		Destroy(gameObject);
	}

}
