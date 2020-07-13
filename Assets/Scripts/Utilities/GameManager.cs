using System.Collections;
using UnityEngine;
using UnityEngine.UI;
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
	private TextMeshProUGUI timeText = null;

	[SerializeField]
	private Image[] bombImages = null;
	[SerializeField]
	private Image[] lifeImages = null;
	[SerializeField]
	private Image[] keyImages = null;

	// Music
	[SerializeField]
	private string mainMusicTitle = "change";
	private Sound music = null;

	// Configs
	[SerializeField]
	private int playerLives = 3;
	public int PlayerLives { get { return playerLives; } set { playerLives = value; } }
	private int maxNumberOfLives = 0;
	public int MaxNumberOfLives { get { return maxNumberOfLives; } private set { maxNumberOfLives = value; } }

	[SerializeField]
	private int numberOfBombs = 3;
	public int NumberOfBombs { get { return numberOfBombs; } set { numberOfBombs = value; } }
	private int maxNumberOfBombs = 0;
	public int MaxNumberOfBombs { get { return maxNumberOfBombs; } private set { maxNumberOfBombs = value; } }

	[SerializeField]
	private int numberOfKeys = 3;
	public int NumberOfKeys { get { return numberOfKeys; } set { numberOfKeys = value; } }
	private int maxNumberOfKeys = 0;
	public int MaxNumberOfKeys { get { return maxNumberOfKeys; } private set { maxNumberOfKeys = value; } }

	public float Time { get; private set; } = 0f;
	public int Score { get; set; } = 0;

	private void Awake()
	{
		SetUpSingelton();

		maxNumberOfBombs = bombImages.Length;
		maxNumberOfLives = lifeImages.Length;
		maxNumberOfKeys = keyImages.Length;

		music = SoundManager.Instance.GetSound(mainMusicTitle);
	}

	private void Start()
	{
		music.AudioSource.Play();
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
			levelLoader = GetComponentInChildren<LevelLoader>();
		}

		// Manage UI
		DisplayScore();
		DisplayTime();
		SetUI(ref bombImages, numberOfBombs, maxNumberOfBombs);
		SetUI(ref lifeImages, playerLives, maxNumberOfLives);
		SetUI(ref keyImages, numberOfKeys, maxNumberOfKeys);
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
		// Pause all sounds
		SoundManager.Instance.PauseAllSounds();
		music.AudioSource.Stop();

		// Restart the level 
		levelLoader.LoadTheSameSceneAgain();
		music.AudioSource.Play();
	}

	private void TakeLife()
	{
		StartCoroutine(PrepareToLoadingSceneAgain());	
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

	public void OnAttackButtonPress()
	{
		PlayerCharacter.Instance.Attack();
	}

	public void SetUI(ref Image [] images, int numberImages, int maxNumberImages)
	{
		for (int i = 0; i < maxNumberImages; i++)
		{
			if (i < numberImages)
			{
				images[i].enabled = true;
			}
			else
			{
				images[i].enabled = false;
			}
		}
	}

	public void GetTreasure(bool chestWasOpened)
	{
		if (chestWasOpened != true)
		{
			return;
		}

		Debug.Log("Display an add!");
	}
}
