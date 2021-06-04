using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond;

    private bool isNight = false;

    [SerializeField] private float fogDensityCalc; // increase

    [SerializeField] private float nightFogDensity; // Fog density on night
    private float dayFogDensity; // Fog density on day 
    private float currentFogDensity; // cal

    // Start is called before the first frame update
    void Start()
    {
        dayFogDensity = RenderSettings.fogDensity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);

        if (transform.eulerAngles.x >= 170)
            isNight = true;
        else if (transform.eulerAngles.x <= 10)
            isNight = false;

        if (isNight)
        {
            if (currentFogDensity <= nightFogDensity)
            {
                currentFogDensity += 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
                RenderSettings.fogColor = Color.black;
            }

        }
        else
        {
            if (currentFogDensity >= dayFogDensity)
            {
                currentFogDensity -= 0.5f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
                RenderSettings.fogDensity = 0;
            }
        }
    }
}
