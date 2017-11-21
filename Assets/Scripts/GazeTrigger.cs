using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeTrigger : MonoBehaviour
{
    public float tickDownTime = 5.0f;


    private Camera myCam;
    private Transform hmdHead = null;

    void findHead()
    {
        if (hmdHead == null)
        {
            SteamVR_TrackedObject[] trackedObjects = FindObjectsOfType<SteamVR_TrackedObject>();
            foreach (SteamVR_TrackedObject tracked in trackedObjects)
            {
                if (tracked.index == SteamVR_TrackedObject.EIndex.Hmd)
                {
                    hmdHead = tracked.transform;
                    break;
                }
            }
        }
    }

    // Use this for initialization
	void Start ()
    {
        myCam = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update ()
    {


        //Ray ray = myCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //RaycastHit hit;
        //Physics.Raycast(ray,)
        //Vector3 start = hmdHead.position;
        //Vector3 end = (hmdHead.position + hmdHead.forward * 100);
        //Ray ray = new Ray(start, end);
        //Physics.Raycast(ray);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            GazeReceiver gr = hit.transform.gameObject.GetComponent<GazeReceiver>();
            if(gr)
            {
                gr.ReceiveGaze();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, (transform.position + transform.forward * 100));
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Gizmos.DrawCube(hit.point, new Vector3(0.1f, 0.1f, 0.1f));
        }
    }
}
