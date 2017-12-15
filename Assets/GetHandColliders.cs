using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Leap.Unity.Interaction;
public class GetHandColliders : MonoBehaviour {
    private List<CapsuleCollider> capsules;
    public List<CapsuleCollider> Capsules { private set { capsules = value; } get { return capsules; } }
	void Start()
    {
		
	}
	


	void Update()
    {
        capsules = new List<CapsuleCollider>();
		//search scene for 'hands'
        foreach (GameObject go in gameObject.scene.GetRootGameObjects() )
        {
            foreach (ContactBone cb in go.GetComponentsInChildren<ContactBone>())
            {
                //ContactBone cb = go.GetComponentInChildren<ContactBone>();
                CapsuleCollider cc = cb.gameObject.GetComponent<CapsuleCollider>();
                if(cc!=null)
                    capsules.Add(cc);
            }
        }
        if(capsules.Count > 0)
        {
            Debug.Log("cap count = " + capsules.Count);
        }
	}
}
