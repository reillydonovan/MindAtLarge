using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityEffect : MonoBehaviour
{
    public Transform target;

    void Update() { 


            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget < 1) distanceToTarget = 1;

           // Remap(distanceToTarget, 0, 1, -1, 1);
            Renderer rend = GetComponent<Renderer>();
            rend.material.SetVector("_HSLAAdjust", new Vector4(0, 1 / distanceToTarget, 0, 0));

            Debug.Log(distanceToTarget);

    }
    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


}