using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBoneHandRigger : MonoBehaviour {
    private List<DynamicBoneColliderBase> colliders;
    public DynamicBone[] boneRoots;

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
        foreach (DynamicBone boneRoot in boneRoots)
        {
            List<DynamicBoneColliderBase> allColliders = new List<DynamicBoneColliderBase>();
            allColliders.AddRange(boneRoot.m_Colliders);
            allColliders.AddRange(colliders);
            boneRoot.m_Colliders = allColliders;
        }
    }
}
