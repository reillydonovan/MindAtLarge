using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperShapeModeller : MonoBehaviour 
{
	// number of verts along the 'longitude'
	public int phiDivs = 10;
	// number of verts along the 'latitude'
	public int thetaDivs = 10;

    //definition of m, n1, n2, n3 for supershape Eval #1
    public Vector4 mn3VecPhi = new Vector4(2.0f,0.7f,0.3f,0.2f);
    //definition of m, n1, n2, n3 for supershape Eval #2
    public Vector4 mn3VecTheta = new Vector4(3.0f,100.0f,100.0f,100.0f);

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
			m = new Mesh();
		}

		//clear out the old mesh
		m.Clear();

		Vector3[] vectors = new Vector3[phiDivs * thetaDivs];
		Vector2[] uvs = new Vector2[phiDivs * thetaDivs];
		float radsPerPhiDiv = Mathf.PI/(phiDivs-1);
		float radsPerThetaDiv = 2.0f*Mathf.PI/thetaDivs;

		float seconds = Time.timeSinceLevelLoad;

		// build an array of vectors holding the vertex data
		int vIndex = 0;
		for(int i=0; i < phiDivs; i++)
		{
			float phi = radsPerPhiDiv*i;

			for(int j=0; j < thetaDivs; j++)
			{
				float theta = radsPerThetaDiv*j;	
				//the get radius function is where 'hamonics' are added
                float r1 = Eval(mn3VecPhi[0],mn3VecPhi[1],mn3VecPhi[2],mn3VecPhi[3],phi);//GetRadius(phi,theta,seconds);
                float r2 = Eval(mn3VecTheta[0],mn3VecTheta[1],mn3VecTheta[2],mn3VecTheta[3],theta);//GetRadius(phi,theta,seconds);

                //add uvs so that we can texture the mesh if we want
				uvs[vIndex] = new Vector2(j*1.0f/thetaDivs,i*1.0f/phiDivs);
				//create a vertex 
				//
				//optimization alert: since the only thing that changes here is the radius
				//(when the number of divisions stays the same) we could cache these numbers
				// and use a shader to create and apply the variations in radius and compute 
				// the normals.
                vectors[vIndex++] = new Vector3(r1*r2*Mathf.Sin(phi)*Mathf.Cos(theta),
                                                r1*r2*Mathf.Sin(phi)*Mathf.Sin(theta),
												r1*r2*Mathf.Cos(phi));				
			}
		}
		m.vertices = vectors;
		m.uv = uvs;

		//assign triangles - these take the form of 'triangle strips' wrapping the
		// circumference of the sphere
		//there is room to optimise this by not recalculating/reassigning if the 
		//count of the vertecies hasn't changed because the topology will still
		// be the same.
		int triCount = 2 * (phiDivs-1) * (thetaDivs);
		int[] triIndecies = new int[triCount*3];
		int curTriIndex = 0;
		for(int i=0; i < phiDivs-1; i++)
		{
			for(int j=0; j < thetaDivs; j++)
			{
				int ul = i*thetaDivs+j;//"upper left" vert
				int ur = i*thetaDivs+ ((j+1) % thetaDivs);//"upper right" vert
				int ll = (i+1)*thetaDivs+j;//"lower left" vert
				int lr = (i+1)*thetaDivs+ ((j+1) % thetaDivs); //"lower right" vert
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
		//use the triangle info to calculate vertex normals so we dont have to B)
		m.RecalculateNormals();
		return m;
	}

    float Eval(float m,float n1,float n2,float n3,float phi )
    {
        float r;
        float t1,t2;
        float a=1,b=1;

        t1 = Mathf.Cos(m * phi / 4) / a;
        t1 = Mathf.Abs(t1);
        t1 = Mathf.Pow( t1 ,n2);

        //float tx1 = Mathf.Pow( Mathf.Cos(m * phi / 4) / a ,n2);

        t2 = Mathf.Sin(m * phi / 4) / b;
        t2 = Mathf.Abs(t2);
        t2 = Mathf.Pow(t2,n3);

        //float tx2 = Mathf.Pow(Mathf.Abs(Mathf.Sin(m * phi / 4) / b),n3);
        // r1*r2 = Mathf.Pow( Mathf.Cos(m * phi / 4) / a ,n2) *Mathf.Pow(Mathf.Abs(Mathf.Sin(m * phi / 4) / b),n3)
        //         (cos(m * phi / 4) / a) ^n2 *  ( abs( sin( m * phi / 4 ) / b) )^n3
        r = Mathf.Pow(t1+t2,1/n1);
        return r;
    }

	//show a representation in the editor window
	private void OnDrawGizmos () {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireMesh(UpdateMesh(null),transform.position,transform.rotation,transform.localScale);

	}
}
