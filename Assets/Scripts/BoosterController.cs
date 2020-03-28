using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoosterController : MonoBehaviour {

    public float maxPower, currentPower;
    //Rigidbody rb;
    public SpacecraftController shipController;
    public Rigidbody rb;

    public List<string> triggersList;
    
	// Use this for initialization
	void Start () {
        //rb = GetComponent<Rigidbody>();
        shipController = transform.parent.GetComponent<SpacecraftController>();
        shipController.boostersList.Add(this);

        rb = transform.parent.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //rb.AddRelativeForce(new Vector3(0, 0, maxPower * currentPower * Time.deltaTime*5));

        rb.AddForceAtPosition(transform.forward * maxPower * currentPower * Time.deltaTime * 5, transform.position);
    }
}
