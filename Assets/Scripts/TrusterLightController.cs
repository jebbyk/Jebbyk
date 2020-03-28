using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrusterLightController : MonoBehaviour {

    BoosterController booster;
    Light light;
    float startEmission;
    bool allowToEmit = true;
    public float speed;

	// Use this for initialization
	void Start () {
        booster = transform.parent.GetComponent<BoosterController>();
        light = GetComponent<Light>();
        startEmission = light.intensity;
		
	}
	
	// Update is called once per frame
	void Update () {
        float neededEmission;
        if (booster.currentPower > 0)
        {
            neededEmission = startEmission * booster.currentPower;
        }
        else
        {
            //light.enabled = false;
            neededEmission = 0;
        }

        light.intensity = Mathf.Lerp(light.intensity, neededEmission, speed*Time.deltaTime);
        if (light.intensity > 0) light.enabled = true;
        else light.enabled = false;

    }
}
