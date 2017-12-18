using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Leap.Unity.Interaction;
public class GetHandColliders : MonoBehaviour
{
    //what is the point of gettors/settors/accessors in c sharp???
    private List<CapsuleCollider> capsules;
    public List<CapsuleCollider> Capsules { private set { capsules = value; } get { return capsules; } }

	void Start()
    {
        capsules = new List<CapsuleCollider>();
    }

	void Update()
    {
		//search scene for 'hands'
        foreach (GameObject go in gameObject.scene.GetRootGameObjects() )
        {
            foreach (ContactBone cb in go.GetComponentsInChildren<ContactBone>())
            {
                CapsuleCollider cc = cb.gameObject.GetComponent<CapsuleCollider>();
                if(cc!=null)
                    if(!capsules.Contains(cc))
                        capsules.Add(cc);
            }
        }
        if(capsules.Count > 0)
        {
            Debug.Log("cap count = " + capsules.Count);
        }
	}
}
