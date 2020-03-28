using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityStablilizer : MonoBehaviour {


    SpacecraftController spacecraft;
    Rigidbody rb;
    public float factor;
    public bool momStabEnbaled, velStabEnabled;


    Vector3 currentAngularVelo, currentVelo;

    // Use this for initialization
    void Start () {

        rb = transform.GetComponent<Rigidbody>();
        spacecraft = transform.GetComponent<SpacecraftController>();

	}
	
	// Update is called once per frame
	void Update () {

        //Vector3 neededAngularForce = new Vector3();
        if (momStabEnbaled)
        {
            currentAngularVelo = transform.InverseTransformDirection(rb.angularVelocity);
            //Debug.DrawLine(transform.position, transform.position + rb.angularVelocity, Color.red);

            
            if(spacecraft.microcomands.Find(m => m.name == "PitchDown") == null)
            {
                if (currentAngularVelo.x > 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("PitchUp", Mathf.Clamp01( currentAngularVelo.x * factor * Time.fixedDeltaTime)));
            }
            if(spacecraft.microcomands.Find(m => m.name == "PitchUp") == null)
            {
                if (currentAngularVelo.x < 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("PitchDown", Mathf.Clamp01( -currentAngularVelo.x * factor * Time.fixedDeltaTime)));
            }
            if(spacecraft.microcomands.Find(m => m.name == "YawRight") == null)
            {
                if (currentAngularVelo.y > 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("YawLeft", Mathf.Clamp01( currentAngularVelo.y * factor*Time.fixedDeltaTime)));
            }
            if(spacecraft.microcomands.Find(m => m.name == "YawLeft") == null)
            {
                if (currentAngularVelo.y < 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("YawRight", Mathf.Clamp01( -currentAngularVelo.y * factor*Time.fixedDeltaTime)));
            }
            if(spacecraft.microcomands.Find(m => m.name == "RollLeft") == null)
            {
                if (currentAngularVelo.z > 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("RollRight", Mathf.Clamp01( currentAngularVelo.z * factor*Time.fixedDeltaTime)));
            }
            if(spacecraft.microcomands.Find(m => m.name == "RollRight") == null)
            {
                if (currentAngularVelo.z < 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("RollLeft", Mathf.Clamp01(-currentAngularVelo.z * factor*Time.fixedDeltaTime)));
            }
        }



        if(velStabEnabled)
        {
            currentVelo = transform.InverseTransformDirection(rb.velocity);

            if(spacecraft.microcomands.Find(m => m.name == "Throttle") == null)
            {
                if (currentVelo.z > 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("Brake", Mathf.Clamp01( currentVelo.z * factor/2.0f * Time.fixedDeltaTime)));
            }
            if(spacecraft.microcomands.Find(m => m.name == "Brake") == null)
            {
                if (currentVelo.z < 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("Throttle", Mathf.Clamp01( -currentVelo.z * factor / 2.0f * Time.fixedDeltaTime)));
            }

            if(spacecraft.microcomands.Find(m => m.name == "StrafeUp") == null)
            {
                if (currentVelo.y > 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("StrafeDown", Mathf.Clamp01( currentVelo.y * factor / 2.0f * Time.fixedDeltaTime)));
            }
            if(spacecraft.microcomands.Find(m => m.name == "StrafeDown") == null)
            {
                if (currentVelo.y < 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("StrafeUp", Mathf.Clamp01( -currentVelo.y * factor / 2.0f*Time.fixedDeltaTime)));
            }

            if(spacecraft.microcomands.Find(m => m.name == "StrafeRight") == null)
            {
                if (currentVelo.x > 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("StrafeLeft", Mathf.Clamp01( currentVelo.x * factor/2.0f*Time.fixedDeltaTime)));
            }
            if(spacecraft.microcomands.Find(m => m.name == "StrafeLeft") == null)
            {
                if (currentVelo.x < 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("StrafeRight", Mathf.Clamp01(-currentVelo.x * factor/2.0f*Time.fixedDeltaTime)));
            }
        }
    }
}
