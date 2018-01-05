using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimator : MonoBehaviour {

    public Material animatedMaterial;
    public Vector2 AnimSpeed = new Vector2(0.2f,0.2f);

    void Start ()
    {
		
	}
	
	void Update ()
    {
        //Vector2 oldOffset = animatedMaterial.GetTextureOffset("_MainTe")
        animatedMaterial.SetTextureOffset("_MainTex", AnimSpeed * Time.time);	
	}
}
