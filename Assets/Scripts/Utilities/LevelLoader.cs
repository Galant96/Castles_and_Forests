using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	// Keeps an instance of the object
	public static LevelLoader Instance { get; private set; }

	[SerializeField]
	private float animationTime = 0.5f;

	private int currentSceneIndex = 0;

	public int CurrentSceneIndex
	{
		get { return currentSceneIndex; }
		private set { currentSceneIndex = value; }
	}

	private void Awake()
	{
		Instance = this;
		currentSceneIndex = GetSceneIndex();
	}

	private void Update()
	{
		if (currentSceneIndex != GetSceneIndex())
		{
			currentSceneIndex = GetSceneIndex();
		}

	}

	public void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
		GameManager.Instance.EnableMainMenuCanvas(true);
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



	public void LoadTheSameSceneAgain()
	{
		SceneManager.LoadScene(CurrentSceneIndex);
	}

	public void LoadFirstLevel()
	{
		GameManager.Instance.EnableMainMenuCanvas(false);
		StartCoroutine(WaitForAnimation(2));
	}

	public void LoadSceneAtIndex(int index)
	{
		WaitForAnimation(index);
	}

	// Untility methods to wait for an animation end
	// After, load a scene
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
