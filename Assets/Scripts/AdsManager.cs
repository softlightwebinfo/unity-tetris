using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
#if UNITY_IOS
    private string gameId = "3545681";
#endif

#if UNITY_ANDROID
    private string gameId = "3545680";
#endif

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId, true);
        StartCoroutine("ShowAdsWhenReady");
    }

    IEnumerator ShowAdsWhenReady()
    {
        while (!Advertisement.isInitialized || !Advertisement.IsReady())
        {
            Debug.Log("Aun no estoy preparado...");
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Show();
    }

    public static void ShowAds()
    {
        if (Advertisement.IsReady())
        {
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;
            Advertisement.Show(options);
        }
    }

    private static void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Completed - offer a reward to the player");
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.Log("Video skipped, Do not Reward the player");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.Log("Video failed to show");
        }
    }
}
