using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fogAnimator : MonoBehaviour
{

    public bool fogAnimating = false;
    private Color startTextColor;
    public Text text;
    public Text text2;
    float currenTextAlpha = 1;
    public float fogStartInitial = 0;
    public float fogStartFinal = 0;
    private float fogStartCurrent = 0;

    public float fogEndInitial = 0;
    public float fogEndFinal = 0;
    private float fogEndCurrent = 0;

    public float transitionTime = 5;
    // Use this for initialization
    void Start ()
    {
        startTextColor = text.color;
        ResetMe();
    }

    public void ResetMe()
    {
        fogEndCurrent = fogEndInitial;
        fogStartCurrent = fogStartInitial;
        fogAnimating = false;
        currenTextAlpha = 1;
    }

    // Update is called once per frame
    void Update ()
    {
        InputUpdate();
        
        if (fogAnimating)
        {
            float textAlphaDelta = 1 / transitionTime;
            currenTextAlpha -= 2*textAlphaDelta*Time.deltaTime;
            currenTextAlpha = Mathf.Max(currenTextAlpha, 0);
            text.color = new Color(startTextColor.r, startTextColor.g, startTextColor.b, currenTextAlpha);
            text2.color = new Color(startTextColor.r, startTextColor.g, startTextColor.b, currenTextAlpha);
            float fogSDif = (fogStartFinal - fogStartInitial) / transitionTime;
            float fogEDif = (fogEndFinal - fogEndInitial) / transitionTime;

            fogStartCurrent = Mathf.Min(fogStartCurrent + fogSDif*Time.deltaTime, fogStartFinal);
            fogEndCurrent = Mathf.Min(fogEndCurrent + fogEDif * Time.deltaTime, fogEndFinal); ;
            RenderSettings.fogEndDistance = fogEndCurrent;
            RenderSettings.fogStartDistance = fogStartCurrent;
            RenderSettings.fogMode = FogMode.Linear;
        }
    }

    void InputUpdate()
    {
        KeyCode keyCode = KeyCode.F;
        if (Input.GetKeyDown(keyCode))
        {
            fogAnimating = true;

        }
    }
}
