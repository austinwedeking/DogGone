using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightPulse : MonoBehaviour
{
    Light2D orbLight;
    bool lightIncreasing = true;

    // Start is called before the first frame update
    void Start()
    {
        orbLight = GameObject.Find("light").GetComponent<Light2D>();
        StartCoroutine(lightFlash());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator lightFlash()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (lightIncreasing == true && orbLight.intensity < 5)
            {
                orbLight.intensity += 0.5f; //brighten
            }
            else if (lightIncreasing == true && orbLight.intensity >= 5)
            {
                lightIncreasing = false; //start dim
            }

            if (lightIncreasing == false && orbLight.intensity > 0.5)
            {
                orbLight.intensity -= 0.5f; //dim
            }
            else if (lightIncreasing == false && orbLight.intensity <= 0.5)
            {
                lightIncreasing = true; //start brighten
            }
        }
    }
}
