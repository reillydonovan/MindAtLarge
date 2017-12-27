using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTouchInteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
       // printCollider(collision);
        
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawLine(contact.point, contact.point - contact.normal * 5);
            //Ray r = new Ray(contact.point - contact.normal, contact.normal);
            //Physics.Raycast()
            // Debug.DrawRay(contact.point, contact.normal*5, Color.white);
           // Mesh m = GetComponent<Mesh>();
           // m.ge
        }


    }

    void printCollider(Collision collision)
    {
        Debug.Log("name: " + collision.gameObject.name + " " + collision.GetType() );
    }
}
