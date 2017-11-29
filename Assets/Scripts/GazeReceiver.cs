using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GazeReceiver : MonoBehaviour
{
    public float delaySeconds = 1.0f;

    private float gazeRemainingTickDown = 999.0f;
    private float gazeStartTime = -1;

    private bool gazedOn = false;
    private bool delayTriggered = false;
    private bool entryTriggered = false;
    private bool exitTriggered = false;
    private RaycastHit? hit = null;

    private bool UpdateCalled = true;

    public void ReceiveGaze(RaycastHit hit)
    {
        if (!UpdateCalled)
        {
            Debug.LogError("GazeReciever base-class update not called! Make sure subclass overrides call the base-class implementation!");
        }
        gazedOn = true;
        this.hit = hit;
        UpdateCalled = false;
        exitTriggered = false;
    }

    public void Update()
    {
        UpdateCalled = true;
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
               
            }
            GazeUpdate(this.hit.Value);
        }
        else //GazeExit
        {
            if(!exitTriggered)
            {
                GazeExit();
                exitTriggered = true;
            }
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

    protected virtual void GazeExit()
    {
        Debug.Log("GazeExit");
    }

    public void OnDrawGizmos()
    {
        //todo show gaze tick down & volume
    }
}
