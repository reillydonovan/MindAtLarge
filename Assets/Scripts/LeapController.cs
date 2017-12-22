using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractionEngineUtility;
using Leap.Unity.Attributes;
using Leap.Unity;
    using Leap.Unity.RuntimeGizmos;
using Leap.Unity.Query;
using Leap.Unity.Interaction.Internal;
using System;
using System.Collections.Generic;
using UnityEngine;


public class LeapController : MonoBehaviour {

    RenderTexture rt;
    MeshRenderer mr;
    LeapImageRetriever lr;
    // Use this for initialization
    void Start () {

        mr = GetComponent<MeshRenderer>();
        lr = GetComponent<LeapImageRetriever>();
    }
	
	// Update is called once per frame
	void Update () {
        
        Material mat = mr.material;
       
        if (lr)
        {
            mat.mainTexture = lr.TextureData.RawTexture.CombinedTexture;
        }
    }
}
