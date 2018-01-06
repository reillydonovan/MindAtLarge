using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityEffect : MonoBehaviour
{
    public Transform target;
    private Material saturationMaterial;

    public float maxDist = 5;
    public float minDist = 0;
    public float normDist = 1;
    public float currentDistance = 0;

    void Start()
    {
        saturationMaterial = GetComponent<Renderer>().material;
    }//24x357/8

    void Update()
    { 
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        currentDistance = distanceToTarget;

        distanceToTarget = Mathf.Min(distanceToTarget, maxDist);
        distanceToTarget = Mathf.Max(distanceToTarget, minDist);
        //Debug.Log(distanceToTarget);
        normDist =( distanceToTarget-minDist) / (maxDist-minDist);
        saturationMaterial.SetVector("_HSLAAdjust", new Vector4(0, 1 / distanceToTarget, 0, 0));
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
        //Gizmos.color = Color.green;
       // Gizmos.DrawWireSphere(transform.position, minDist);
       // Gizmos.color = Color.red;
       // Gizmos.DrawWireSphere(transform.position, maxDist);     
    }
}