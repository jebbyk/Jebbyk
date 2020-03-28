using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterFireController : MonoBehaviour {

    BoosterController booster;
    ParticleSystem psComponent;
    public float startEmission;
    public bool allowToEmit = true;
 

	// Use this for initialization
	void Start () {

        booster = transform.parent.GetComponent<BoosterController>();
        psComponent = GetComponent<ParticleSystem>();
        var em = psComponent.emission;
        startEmission = em.rateOverTime.constant;
		
	}
	
	// Update is called once per frame
	void Update () {
        var em = psComponent.emission;

        if (booster.currentPower > 0)
        {
            em.rateOverTime = startEmission * booster.currentPower;
        }
        else
        {
            em.rateOverTime = 0;
        }
		
	}
}
