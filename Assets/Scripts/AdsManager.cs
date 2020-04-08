using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    string appleID = "3545681", googleID = "3545680";

    // Start is called before the first frame update
    void Start()
    {
#if PLATFORM_ANDROID
            Advertisement.Initialize(googleID);
#endif
#if PLATFORM_IOS
            Advertisement.Initialize(appleID);
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
