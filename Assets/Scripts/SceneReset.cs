using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
//Valve.VR.IVRSystem.
public class SceneReset : MonoBehaviour
{
    private fogAnimator fog;
    private Scene curScene;
    private bool didDeviceInactivityReset = false;

    EDeviceActivityLevel oldActLevel;

    // Use this for initialization
    void Start ()
    {
        oldActLevel = getHeadActivityLevel();
        Debug.Log("oldActLevel: " + oldActLevel);
        curScene = SceneManager.GetActiveScene();
        fog = GetComponent<fogAnimator>();
        //EDeviceActivityLevel d = EDeviceActivityLevel;
    }
	
	// Update is called once per frame
	void Update ()
    {
        EDeviceActivityLevel currentActivityLevel = getHeadActivityLevel();

        //Debug.Log("cur actlev: " + currentActivityLevel);
        bool matchingActivityLevels = (oldActLevel == currentActivityLevel);
        if (!matchingActivityLevels && !didDeviceInactivityReset && currentActivityLevel == EDeviceActivityLevel.k_EDeviceActivityLevel_Standby)
        {
            reload();
            didDeviceInactivityReset = true;
            Debug.Log("actlev: " + currentActivityLevel);
            Debug.Log("INACTIVITY RESET");
        }
        else
        {
            didDeviceInactivityReset = false;
        }

        if(!matchingActivityLevels)
        {
            Debug.Log("(!matchingActivityLevels)s");
            oldActLevel = currentActivityLevel;
        }

        KeyCode keyCode = KeyCode.R;
		if(Input.GetKeyDown(keyCode))
        {
            reload();
        }
	}

    EDeviceActivityLevel getHeadActivityLevel()
    {
        CVRSystem cvrSystem = SteamVR.instance.hmd;
        EDeviceActivityLevel actlev = cvrSystem.GetTrackedDeviceActivityLevel(OpenVR.k_unTrackedDeviceIndex_Hmd);
        return actlev;
    }

    void reload()
    {
        SceneManager.UnloadSceneAsync(curScene);
        SceneManager.LoadScene(curScene.name, LoadSceneMode.Single);
        fog.ResetMe();
    }
}
