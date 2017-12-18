using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTableCloth : MonoBehaviour
{
    public GetHandColliders handcolliders;
    List<CapsuleCollider> currentColliders = new List<CapsuleCollider>();

    Cloth myCloth;

    // Use this for initialization
    void Start ()
    {
        myCloth = GetComponent<Cloth>();
        addAllTableColliders();
        handcolliders.newCollidersHandler += Handcolliders_newCollidersHandler;
    }


    void addAllTableColliders()
    {
        List<CapsuleCollider> tableColliders = new List<CapsuleCollider>();
        foreach (CapsuleCollider cc in gameObject.GetComponentsInChildren<CapsuleCollider>())
        {
            currentColliders.Add(cc);
        }
        myCloth.capsuleColliders = currentColliders.ToArray();
    }

    private void Handcolliders_newCollidersHandler(GetHandColliders sender, List<CapsuleCollider> newColliders)
    {
        Debug.Log("got collides!");
        currentColliders.InsertRange(0, newColliders);
        //currentColliders.AddRange(newColliders);
        myCloth.capsuleColliders = currentColliders.ToArray();

    }

    // Update is called once per frame
    void Update ()
    {
       
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
