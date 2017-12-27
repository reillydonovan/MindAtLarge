using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBoneHandRigger : MonoBehaviour {
    private List<DynamicBoneColliderBase> colliders;
    public DynamicBone boneRoot;

    // Use this for initialization
    void Start () {
        colliders = new List<DynamicBoneColliderBase>();
        CreateCollisionComponents(transform);
        AssignToDynamicBoneObject();
    }
	
    void CreateCollisionComponents(Transform t)
    {
        t.gameObject.AddComponent<DynamicBoneCollider>();
        DynamicBoneCollider dbc = t.GetComponent<DynamicBoneCollider>();
        dbc.m_Radius = 0.01f;
        colliders.Add(dbc);
        foreach (Transform child in t)
        {
            CreateCollisionComponents(child);
        }
    }

    void AssignToDynamicBoneObject()
    {
        List<DynamicBoneColliderBase> allColliders = new List<DynamicBoneColliderBase>();
        allColliders.AddRange(boneRoot.m_Colliders);
        allColliders.AddRange(colliders);
        boneRoot.m_Colliders = allColliders;
    }
}
