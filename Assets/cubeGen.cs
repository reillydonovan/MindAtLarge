using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeGen : MonoBehaviour {
    public GameObject template;
    public float updateT = 2;
    float curT = 0;
	// Use this for initialization
	void Start () {
        curT = updateT;
	}
	
	// Update is called once per frame
	void Update () {
        curT -= Time.deltaTime;
        if(curT < 0)
        {
            curT = updateT;
            //GameObject.Instantiate(template,transform);
            GameObject.Instantiate(template, transform.position, transform.rotation, transform);                
        }
    }
}
