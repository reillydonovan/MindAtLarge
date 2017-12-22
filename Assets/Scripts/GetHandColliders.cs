using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Leap.Unity.Interaction;

public delegate void GotNewCollidersEventHandler(GetHandColliders sender, List<CapsuleCollider> newColliders);


public class GetHandColliders : MonoBehaviour
{
    //what is the point of gettors/settors/accessors in c sharp???
    private List<CapsuleCollider> capsules;
    public List<CapsuleCollider> Capsules { private set { capsules = value; } get { return capsules; } }
    public event GotNewCollidersEventHandler newCollidersHandler;

    void Start()
    {
        capsules = new List<CapsuleCollider>();
    }

	void Update()
    {
        List<CapsuleCollider> newCapsules = new List<CapsuleCollider>();
		//search scene for 'hands'
        foreach (GameObject go in gameObject.scene.GetRootGameObjects() )
        {
            foreach (ContactBone cb in go.GetComponentsInChildren<ContactBone>())
            {
                if (cb.metacarpalJoint == null)
                { 
                    CapsuleCollider cc = cb.gameObject.GetComponent<CapsuleCollider>();
                    if (cc != null)
                        if (!capsules.Contains(cc))
                            newCapsules.Add(cc);
                }
            }
        }
        if(newCapsules.Count > 0)
        {
            capsules.AddRange(newCapsules);
            OnNewCollidersReceived(newCapsules);            
        }
	}

    protected virtual void OnNewCollidersReceived(List<CapsuleCollider> newCapsules)
    {
        if (newCapsules.Count > 0)
        {
            Debug.Log("new cap count = " + newCapsules.Count);
        }
        GotNewCollidersEventHandler handler = newCollidersHandler;
        if (handler != null)
        {
            handler(this, newCapsules);
        }
    }
}
