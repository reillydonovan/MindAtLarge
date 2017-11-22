using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeReceiver : MonoBehaviour
{
    public float delaySeconds = 1.0f;

    private float gazeRemainingTickDown = 999.0f;
    private float gazeStartTime = -1;

    private bool gazedOn = false;
    private bool delayTriggered = false;
    private bool entryTriggered = false;
    private RaycastHit? hit = null;


    public void ReceiveGaze(RaycastHit hit)
    {
        gazedOn = true;
        this.hit = hit;
    }

    public void Update()
    {
        if (gazedOn == true && this.hit.HasValue)
        {
            if (!entryTriggered)
            {
                GazeEntryTriggerOnce(this.hit.Value);
                entryTriggered = true;
            }

            gazeRemainingTickDown -= Time.deltaTime;
            if (gazeRemainingTickDown <= 0)
            {
                if (!delayTriggered)
                {
                    delayTriggered = true;
                    GazeDelayTriggerOnce(this.hit.Value);
                }
                GazeUpdate(this.hit.Value);
            }
        }
        else
        {
            gazeRemainingTickDown = delaySeconds;
            delayTriggered = false;
            entryTriggered = false;
        }
        gazedOn = false;
        hit = null;
    }

    protected virtual void GazeEntryTriggerOnce(RaycastHit hit)
    {
        Debug.Log("GazeEntryTriggerOnce");
    }

    protected virtual void GazeDelayTriggerOnce(RaycastHit hit)
    {
        Debug.Log("GazeDelayTriggerOnce");
    }

    protected virtual void GazeUpdate(RaycastHit hit)
    {
        Debug.Log("GazeUpdate");
    }

    public void OnDrawGizmos()
    {
        //todo show gaze tick down & volume
    }
}
