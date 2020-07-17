using System.Collections;
using System.Collections.Generic;
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
	private GameObject treasureCanvas = null;

	[SerializeField]
	private GameObject interfaceButtons = null;
	public GameObject InterfaceButtons { get => interfaceButtons; set => interfaceButtons = value; }

	[SerializeField]
	private GameObject [] treasureSlots = new GameObject[3];

	[SerializeField]
	private List<GameObject> collectablesPrefab = null;

	[SerializeField]
	private Button videoGoldButton = null;
	public Button VideoGoldButton { get => videoGoldButton; set => videoGoldButton = value; }

	[SerializeField]
	private Button videoLifeButton = null;
	public Button VideoLifeButton { get => videoLifeButton; set => videoLifeButton = value; }

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

	// Number of items from the treasure
	private int numberOfItems = 3;

	public enum Reward
	{
		treasure,
		health
	}

	[SerializeField]
	private Reward rewardType = new Reward();
	public Reward RewardType { get => rewardType; set => rewardType = value; }

	public void SetRewardToTreasure()
	{
		rewardType = Reward.treasure;
	}

	public void SetRewardToHealth()
	{
		rewardType = Reward.health;
	}

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
		// Set the selector panle disactive.
		treasureCanvas.SetActive(false);
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

		checkMaxNumberOfPlayersStats();

		// Manage UI
		DisplayScore();
		DisplayTime();
		SetUI(ref bombImages, numberOfBombs, maxNumberOfBombs);
		SetUI(ref lifeImages, playerLives, maxNumberOfLives);
		SetUI(ref keyImages, numberOfKeys, maxNumberOfKeys);
	}

	private void checkMaxNumberOfPlayersStats()
	{
		if (playerLives > maxNumberOfLives)
		{
			playerLives = maxNumberOfLives;
		}

		if (numberOfBombs > maxNumberOfBombs)
		{
			numberOfBombs = maxNumberOfBombs;
		}

		if (numberOfKeys > maxNumberOfKeys)
		{
			numberOfKeys = maxNumberOfKeys;
		}
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

		treasureCanvas.SetActive(true);

		GameObject[] randomItems = new GameObject[numberOfItems];
		int[] multiplayers = new int[numberOfItems];


		// Get random items from the treasure.
		for (int i = 0; i < randomItems.Length; i++)
		{
			randomItems[i] = collectablesPrefab[Random.Range(0, collectablesPrefab.Count-1)];
			multiplayers[i] = Random.Range(1, 10);

			Debug.Log(multiplayers[i]);
			treasureSlots[i].GetComponent<Image>().sprite = randomItems[i].GetComponent<SpriteRenderer>().sprite;
			treasureSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = " X " + multiplayers[i];

			randomItems[i].GetComponent<Collectibles>().InvokeOnCollectibleHit(multiplayers[i]);
		}

		Debug.Log("Display an add!");
	}

	public void ActiveButton(Button button)
	{
		button.gameObject.SetActive(false);
	}

	public void ActiveButton(Button button, bool isActive)
	{
		button.gameObject.SetActive(isActive);
	}

	public void ExitCanvas(GameObject canvas)
	{
		canvas.SetActive(false);
	}
}
