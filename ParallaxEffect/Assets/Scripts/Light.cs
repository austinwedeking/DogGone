using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light : MonoBehaviour
{
    Light2D characterLight;
    bool lightIncreasing = true;

    // Start is called before the first frame update
    void Start()
    {
        //characterLight = GameObject.Find("CharacterLight").GetComponent<Light2D>();
        //StartCoroutine(lightFlash());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator lightFlash()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);

            if (lightIncreasing == true && characterLight.intensity < 20)
            {
                characterLight.intensity += 1; //brighten
            }
            else if (lightIncreasing == true && characterLight.intensity >= 20)
            {
                lightIncreasing = false; //start dim
            }

            if (lightIncreasing == false && characterLight.intensity > 0)
            {
                characterLight.intensity -= 1; //dim
            }
            else if (lightIncreasing == false && characterLight.intensity <= 0)
            {
                lightIncreasing = true; //start brighten
            }
        }
    }
}
