using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubDivAdjuster : MonoBehaviour {
    Material subdivMat;
	// Use this for initialization
	void Start ()
    {
        subdivMat = GetComponent<Renderer>().materials[0];
        Debug.Log("something");
	}
	
	// Update is called once per frame
	void Update ()
    {
        float amt = Mathf.Sin(Time.fixedTime) * 2.0f;
        subdivMat.SetFloat("Factor", amt);
        Debug.Log(amt);
	}
}
