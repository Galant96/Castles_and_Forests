using UnityEngine;
using UnityEngine.Advertisements;

public class UnityMonetizationHandler : MonoBehaviour, IUnityAdsListener
{
	private string googlePlayID = "3712389";
	string myPlacementId = "rewardedVideo";
	private bool TestMode = true;
	private GameManager gameManager = null;

	[SerializeField]
	private int minRewardNumber = 1;
	[SerializeField]
	private int maxRewardNumber = 10;

	// Start is called before the first frame update
	void Start()
    {
		gameManager = GameManager.Instance;
		// Initialise ads
		Advertisement.AddListener(this);
		Advertisement.Initialize(googlePlayID, TestMode);
    }

	// Implement IUnityAdsListener interface methods:
	public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
	{
		// Define conditional logic for each ad completion status:
		if (showResult == ShowResult.Finished)
		{
			Debug.LogWarning("You Get A Reward!");

			switch (gameManager.RewardType)
			{
				case GameManager.Reward.treasure:
					gameManager.GetTreasure(true, gameManager.ObjectInstantiationPos, minRewardNumber, maxRewardNumber);
					break;
				case GameManager.Reward.health:
					gameManager.PlayerLives = gameManager.MaxNumberOfLives;
					break;
				default:
					break;
			}
			// Reward the user for watching the ad to completion.
		}
		else if (showResult == ShowResult.Skipped)
		{
			// Do not reward the user for skipping the ad.
			Debug.LogWarning("You Do Not Get A Reward!");

		}
		else if (showResult == ShowResult.Failed)
		{
			Debug.LogWarning("The ad did not finish due to an error.");
		}
	}

	public void OnUnityAdsReady(string placementId)
	{
		// If the ready Placement is rewarded, show the ad:
		if (placementId == myPlacementId)
		{
			// Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
		}
	}

	public void OnUnityAdsDidError(string message)
	{
		// Log the error.
	}

	public void OnUnityAdsDidStart(string placementId)
	{
		// Optional actions to take when the end-users triggers an ad.
	}

	// When the object that subscribes to ad events is destroyed, remove the listener:
	public void OnDestroy()
	{
		Advertisement.RemoveListener(this);
	}

	public void DisplayVideoAds()
	{
		Advertisement.Show(myPlacementId);
	}
}
