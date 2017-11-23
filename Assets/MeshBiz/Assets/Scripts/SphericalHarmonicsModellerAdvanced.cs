using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalHarmonicsModellerAdvanced : MonoBehaviour 
{
	// number of verts along the 'longitude'
	public int phiDivs = 10;
	// number of verts along the 'latitude'
	public int thetaDivs = 10;

    //vars to keep track if things change so we don't have to update the mesh all the time
    private int lastPhiDivs = -1;
    private int lastThetaDivs = -1;


	// Use this for initialization
	void Start () 
	{
		//we need a mesh filter
		GetComponent<MeshFilter>().mesh = new Mesh();


	}
	
	// Update is called once per frame
	void Update () 
	{
		this.UpdateMesh( GetComponent<MeshFilter>().mesh );
	}

	// create or update a mesh object to have a sphere with hamonic waves all over
	// it
	Mesh UpdateMesh(Mesh m)
	{
		if(m == null)
		{
            Debug.Log("New mesh!");
			m = new Mesh();
		}


        if (lastPhiDivs != phiDivs ||
           lastThetaDivs != thetaDivs)
        {
            //clear out the old mesh - helps resolve out of bounds errors resulting from a
            m.Clear();
            buildAndAssignVertsAndUVs( ref m);
            triangulateMesh(ref m);

            lastPhiDivs = phiDivs;
            lastThetaDivs = thetaDivs;
        }

        return m;
    }

    void buildAndAssignVertsAndUVs(ref Mesh m)
    {

        
        Debug.Log("buildAndAssignVertsAndUVs!");
        // build arrays of vectors holding the vertex data - uvs, phi/theta, xyz
        Vector3[] vectors = new Vector3[phiDivs * thetaDivs];
        Vector2[] uvs = new Vector2[phiDivs * thetaDivs];
        Vector2[] phiTheta = new Vector2[phiDivs * thetaDivs]; //phi theta will be packed inot
        float radsPerPhiDiv = Mathf.PI / (phiDivs - 1);
        float radsPerThetaDiv = 2.0f * Mathf.PI / thetaDivs;

//        float seconds = Time.timeSinceLevelLoad;


        int vIndex = 0;
        for (int i = 0; i < phiDivs; i++)
        {
            float phi = radsPerPhiDiv * i;

            for (int j = 0; j < thetaDivs; j++)
            {
                float theta = radsPerThetaDiv * j;
                //the get radius function is where 'hamonics' are added

                //add normalized uvs so that we can texture the mesh if we want
                uvs[vIndex] = new Vector2(j * 1.0f / thetaDivs, i * 1.0f / phiDivs);
                //create a vertex - we also store the phi and theta here for use in the shader
                phiTheta[vIndex] = new Vector2(phi, theta);
                vectors[vIndex++] = Vector3.zero;
//                float radius = GetRadius(phi, theta, seconds);
//                vectors[vIndex++] = new Vector3(radius * Mathf.Sin(phi) * Mathf.Cos(theta),
//                                                radius * Mathf.Sin(phi) * Mathf.Sin(theta),
//                                                radius * Mathf.Cos(phi));
//                
            }
        }
        
        m.vertices = vectors;
        m.uv = uvs;
        m.uv2 = phiTheta;
    }

    void triangulateMesh(ref Mesh m)
    {
        Debug.Log("Triangulating the mesh!");
        //assign triangles - these take the form of 'triangle strips' wrapping the
        // circumference of the sphere
        //there is room to optimise this by not recalculating/reassigning if the 
        //count of the vertecies hasn't changed because the topology will still
        // be the same.
        int triCount = 2 * (phiDivs - 1) * (thetaDivs);
        int[] triIndecies = new int[triCount * 3];
        int curTriIndex = 0;
        for (int i = 0; i < phiDivs - 1; i++)
        {
            for (int j = 0; j < thetaDivs; j++)
            {
                int ul = i * thetaDivs + j;                           //"upper left" vert
                int ur = i * thetaDivs + ((j + 1) % thetaDivs);       //"upper right" vert
                int ll = (i + 1) * thetaDivs + j;                     //"lower left" vert
                int lr = (i + 1) * thetaDivs + ((j + 1) % thetaDivs); //"lower right" vert

                //triangle one
                triIndecies[curTriIndex++] = ul;
                triIndecies[curTriIndex++] = ll;
                triIndecies[curTriIndex++] = ur;

                //triangle two
                triIndecies[curTriIndex++] = ll;
                triIndecies[curTriIndex++] = lr;
                triIndecies[curTriIndex++] = ur;
            }
        }
        m.triangles = triIndecies;
    }

	//get radius applies waves along phi and theta based on the public variables
	//optimization note:
	// this would not be impossible to code as a shader... however, getting multiple 
	// waves affecting the surface at once might take some careful thinking...
	float GetRadius(float phi, float theta, float time = 0)
	{  
        //TODO fill this in with real data so that this can be used in the scene-view to visualize while not 'running' the 'game'
        float xMod1YOffset = 0;
        float xMod1Scale = 0;
        float xMod1TimeResponse = 0;
        float xMod1Period = 0;
        float xMod1PhaseOffset = 0;
        float yMod1YOffset = 0;
        float yMod1Scale = 0;
        float yMod1TimeResponse = 0;
        float yMod1Period = 0;
        float yMod1PhaseOffset = 0;
		return xMod1YOffset + xMod1Scale*Mathf.Sin(xMod1TimeResponse*time + theta*xMod1Period + xMod1PhaseOffset) +
			yMod1YOffset + yMod1Scale*Mathf.Sin(yMod1TimeResponse*time + phi*yMod1Period + yMod1PhaseOffset);
	}



	//show a representation in the editor window
	private void OnDrawGizmos () 
    {
//		Gizmos.color = Color.cyan;
//		Gizmos.DrawWireMesh(UpdateMesh(null),transform.position,transform.rotation,transform.localScale);

	}
}
