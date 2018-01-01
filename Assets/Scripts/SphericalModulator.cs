using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalModulator : MonoBehaviour
{
    public Light connectedLight;

    private Material sphericalHarmonicMaterial;
    public Vector3 RotationSpeed = new Vector3(1,1,1);

    // Use this for initialization
    void Start()
    {
        sphericalHarmonicMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        modulateRotation();
        modulateLight();
        modulateSphereMat();
    }

    void modulateRotation()
    {
        transform.Rotate( RotationSpeed.x,
                          RotationSpeed.y,
                          RotationSpeed.z);
    }

    void modulateSphereMat()
    {
        sphericalHarmonicMaterial.SetFloat("xMod1YOffset", 
            0.35f * Mathf.Sin(Time.time/8.0f));
        sphericalHarmonicMaterial.SetFloat("xMod1Scale",
            .41f * Mathf.Sin(Time.time/7.3f));
        //sphericalHarmonicMaterial.SetFloat("xMod1TimeResponse", 
        //    3 * Mathf.Sin(Time.time));
        sphericalHarmonicMaterial.SetFloat("xMod1Period", 
            12 * Mathf.Sin(Time.time/20.0f));
        //sphericalHarmonicMaterial.SetFloat("xMod1PhaseOffset", 
        //    3 * Mathf.Sin(Time.time));

        sphericalHarmonicMaterial.SetFloat("yMod1YOffset",
            0.25f * Mathf.Sin(Time.time /13.0f));
        sphericalHarmonicMaterial.SetFloat("yMod1Scale", 
            0.2f * Mathf.Sin(Time.time / 11.0f));
        //sphericalHarmonicMaterial.SetFloat("yMod1TimeResponse", 
        //    3 * Mathf.Sin(Time.time));
        sphericalHarmonicMaterial.SetFloat("yMod1Period",
            18 * Mathf.Sin(Time.time / 3.0f));
        //sphericalHarmonicMaterial.SetFloat("yMod1PhaseOffset",
        //    3 * Mathf.Sin(Time.time));
    }

    void modulateLight()
    {
        float hue = (Time.time / 10) % 1;///(Mathf.Sin(Time.time/2) + 1) / 2;
        connectedLight.color = Color.HSVToRGB( hue, 1, 1);
    }
}
