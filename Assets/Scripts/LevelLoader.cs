using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	// Keeps an instance of the object
	public static LevelLoader Instance { get; private set; }

	[SerializeField]
	private float animationTime = 1f;

	private int currentSceneIndex = 0;

	public int CurrentSceneIndex
	{
		get { return currentSceneIndex; }
		private set { currentSceneIndex = value; }
	}

	private int previousSceneIndex = 0;

	private void Awake()
	{
		CurrentSceneIndex = GetSceneIndex();
	}

	private void Update()
	{
		if (CurrentSceneIndex != GetSceneIndex())
		{
			CurrentSceneIndex = GetSceneIndex();
		}
	}

	public void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadMainMenu()
	{
		StartCoroutine(WaitForAnimation("Main Menu"));
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
		StartCoroutine(WaitForAnimation(1));
	}

	// Untility methods to wait for an animation end
	// After, load a needed scene
	IEnumerator WaitForAnimation(string animationName)
	{
		Debug.Log("Loading a scene...");
		yield return new WaitForSeconds(animationTime);
		SceneManager.LoadScene(animationName);
	}

	IEnumerator WaitForAnimation(int animationIndex)
	{
		Debug.Log("Loading a scene...");
		yield return new WaitForSeconds(animationTime);
		SceneManager.LoadScene(animationIndex);
	}

}
