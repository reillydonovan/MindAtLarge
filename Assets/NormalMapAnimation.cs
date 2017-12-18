using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMapAnimation : MonoBehaviour
{

    Material refractiveGlassMaterial;
    public Vector2 velocity;
    public float distAmt;
	// Use this for initialization
	void Start ()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Debug.Log("got the mesh renderer");
        refractiveGlassMaterial = mr.material;
	}
	
	// Update is called once per frame
	void Update ()
    {
        refractiveGlassMaterial.SetFloat("_BumpAmt", distAmt);
        refractiveGlassMaterial.SetTextureOffset("_BumpMap", 
             0.1f  *velocity * Time.time);
	}
}
