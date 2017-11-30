using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGazeReciever : GazeReceiver {

    Material myMaterial;
    
    private bool isGazedOn = false;
    private float currentFactor = 0;
    public float factorDelta = 0.05f;
    public float maxFaxtor = 0.8f;

    private float curHtMapTm = 0;
    public float heightMapSpeed = 1.0f;

    
    void Start ()
    {
        myMaterial = GetComponent<MeshRenderer>().materials[1];
        
        if(myMaterial == null)
        {
            Debug.LogError("We got a problem; Material is null");
        }
    }

    private void Update()
    {
        base.Update();

        //float modulator = 0.8f * Mathf.Sin(Time.time * 0.25f);
        if (isGazedOn)
        { currentFactor = Mathf.Min(currentFactor + factorDelta * Time.deltaTime, maxFaxtor); }
        else
        { currentFactor = Mathf.Max(currentFactor - factorDelta * Time.deltaTime, 0); }

        setFactorProp(currentFactor);

        Vector2 height = new Vector2(0.1f * curHtMapTm, 0.1f * curHtMapTm);
        setHeightProp(height);
    }

    void setHeightProp(Vector2 Offset)
    {
        string heightPropName = "_HeightMap";
        if (myMaterial.HasProperty(heightPropName))
        {
            myMaterial.SetTextureOffset("_HeightMap",Offset);
        }
    }

    void setFactorProp(float factor)
    {
        string factorPropName = "_Factor";
        if (myMaterial.HasProperty(factorPropName))
        {
           
            myMaterial.SetFloat(factorPropName, factor);
        }
    }

    protected override void GazeEntryTriggerOnce(RaycastHit hit)
    {
        isGazedOn = true;
    }

    protected override void GazeExit()
    {
        isGazedOn = false;
    }

    protected override void GazeUpdate(RaycastHit hit)
    {
        curHtMapTm += heightMapSpeed * Time.deltaTime;
    }
}
