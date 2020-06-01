using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	// Keeps an instance of the object
	public static LevelLoader Instance { get; private set; }

	private int currentSceneIndex = 0;

	public int CurrentSceneIndex
	{
		get { return currentSceneIndex; }
		private set { currentSceneIndex = value; }
	}

	private int previousSceneIndex = 0;

	private void Awake()
	{
		SetUpSingelton();
		CurrentSceneIndex = GetSceneIndex();
	}

	private void Update()
	{
		if (CurrentSceneIndex != GetSceneIndex())
		{
			CurrentSceneIndex = GetSceneIndex();
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

	public void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

	public void LoadOptions()
	{
		SceneManager.LoadScene("Options");
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public int GetSceneIndex()
	{
		return SceneManager.GetActiveScene().buildIndex;
	}

	public void LoadPreviousScene()
	{
		SceneManager.LoadScene(previousSceneIndex);
	}


	public void SavePreviousScene()
	{
		previousSceneIndex = GetSceneIndex();
	}

	public void LoadTheSameSceneAgain()
	{
		SceneManager.LoadScene(CurrentSceneIndex);
	}

	public void LoadFirstLevel()
	{
		SceneManager.LoadScene(1);
	}

}
