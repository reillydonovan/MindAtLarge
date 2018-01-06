using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityEffect : MonoBehaviour
{
    public Transform target;
    private Material saturationMaterial;

    public float maxDist = 5;
    public float minDist = 0;
    private float normDist = 1;
    private float currentDistance = 0;

    void Start()
    {
        saturationMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    { 
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        currentDistance = distanceToTarget;

        distanceToTarget = Mathf.Min(distanceToTarget, maxDist);
        distanceToTarget = Mathf.Max(distanceToTarget, minDist);
        //Debug.Log(distanceToTarget);
        normDist =( distanceToTarget-minDist) / (maxDist-minDist);
        float saturation =  - normDist;
        //Debug.Log("saturation: " + saturation + " normDist: " + normDist + " current: " + currentDistance);
        saturationMaterial.SetVector("_HSLAAdjust", new Vector4(0,saturation, 0, 0));
    }

    private void OnDrawGizmos()
    {
        Vector3 fromMeToTarg = target.position - transform.position;
        Vector3 fromMeToTargNorm = fromMeToTarg.normalized;

        if(currentDistance < minDist)
        {
            Gizmos.color = Color.red;
        }
        else if(currentDistance >= minDist)
        {
            Gizmos.color = Color.green;
        }
        else if(currentDistance >= maxDist)
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine(transform.position, target.position);

        //Debug.DrawLine(transform.position, target.position);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDist);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDist);     
    }
}