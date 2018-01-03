using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityEffect : MonoBehaviour
{
    public Transform target;

    void Start()
    {

        StartCoroutine(AdjustSaturation());

    }

    IEnumerator AdjustSaturation()
    {

        while (true)
        {

           
                float distanceToTarget = Vector3.Distance(transform.position, target.position); 

                if (distanceToTarget < 1) distanceToTarget = 1;

                Debug.Log(distanceToTarget);

                yield return new WaitForSeconds(0.01f);

            
        }

    }
}