using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTableCloth : MonoBehaviour {

    public GetHandColliders handcolliders;
    private List<CapsuleCollider> tableColliders;
    Cloth myCloth;

    // Use this for initialization
    void Start ()
    {
        myCloth = GetComponent<Cloth>();
        tableColliders = new List<CapsuleCollider>();
        foreach(CapsuleCollider cc in gameObject.GetComponentsInChildren<CapsuleCollider>())
        {
            tableColliders.Add(cc);
        }
        myCloth.capsuleColliders = tableColliders.ToArray();
    }
	
	// Update is called once per frame
	void Update ()
    {
        List<CapsuleCollider> allColliders = new List<CapsuleCollider>();
        if(tableColliders.Count>0)
            allColliders.InsertRange(0, tableColliders);
        if(handcolliders.Capsules != null && handcolliders.Capsules.Count>0)
            allColliders.InsertRange(0, handcolliders.Capsules);

        if(allColliders.Count > 0)
            myCloth.capsuleColliders = allColliders.ToArray();
    }

    public void HandMethod(bool param)
    {

    }

    public void HandNoParamMethod()
    {

    }



    public bool boolMethod()
    {
        return false;
    }
}
