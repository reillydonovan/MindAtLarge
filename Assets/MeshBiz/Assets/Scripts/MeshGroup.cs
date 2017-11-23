using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//class to allow really really large numbers of verts to adressed 
public class MeshGroup : MonoBehaviour 
{
    // number of verts along the 'longitude'
    public int phiDivs = 10;

    // number of verts along the 'latitude'
    public int thetaDivs = 10;

    //vars to keep track if things change so we don't have to update the mesh all the time
    private int lastPhiDivs = -1;
    private int lastThetaDivs = -1;

    private ArrayList meshes;
    private ArrayList transformChildren;

    void Start () 
    {
        meshes = new ArrayList();
        transformChildren = new ArrayList();
    }

    // Update is called once per frame
    void Update () 
    {
        createMeshes();
    }

    private void createMeshes()
    {
        //remove all children-meshes
        meshes.Clear();
        foreach (Transform child in transformChildren) 
        {
            GameObject.Destroy(child.gameObject);
        }
        transformChildren.Clear();

        //create new meshes
        int maxVertCount = 65536-1; //(2^16-1)%3 == 0
        int meshcount = (phiDivs * thetaDivs)/maxVertCount;
         
        for(int i = 0; i <= meshcount; i++)
        {
            GameObject go = new GameObject();
            transformChildren.Add(go.transform);
            go.transform.parent = transform;
            go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshFilter>();
            Mesh m = new Mesh();
            meshes.Add(m);
            go.GetComponent<MeshFilter>().mesh = m;
        }
    }

//    maybe we eventually have a technique to set all of the child materials...
//    public void setMaterials()
}


