using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchorOld : MonoBehaviour {

    public float forwardOffset, 
        distanceOverFwdVelo, 
        verticalOffsetOverVelo, 
        horizontalOffsetOverVelo,
        horizontalOffsetOverRot,
        verticalOffsetOverRot, 
        rotationOverAngVelo, 
        verticalOffset,
        smoothFactor;

    public bool anchorRotation;

    public Vector3 rotMults;

    Transform targetTransform;
    Rigidbody targetRB;
    [SerializeField]
    private Transform camTransform;
    Vector3 oldVelocity = new Vector3();
    Vector3 velocityDelta = new Vector3();
    Vector3 oldAngVel = new Vector3();
    public Vector3 angVelDelta = new Vector3();
    Vector3 oldVelDelta = new Vector3();
    Vector3 nineteenAngle = new Vector3(90, 90, 90);

    Vector3 BestPosCalc()
    {
        Vector3 neededPos = new Vector3();

        velocityDelta = targetRB.velocity - oldVelocity;
        velocityDelta = targetTransform.InverseTransformDirection(velocityDelta);
        oldVelocity = targetRB.velocity;

        neededPos.z = forwardOffset - velocityDelta.z * distanceOverFwdVelo;
        neededPos.y = verticalOffset + velocityDelta.y * verticalOffsetOverVelo + angVelDelta.x*verticalOffsetOverRot;
        neededPos.x = -velocityDelta.x * horizontalOffsetOverVelo - angVelDelta.y * horizontalOffsetOverRot;

        return neededPos;

    }

    Vector3 BestRotCalc()
    {
        Vector3 neededRot = new Vector3();

        angVelDelta = targetRB.angularVelocity - oldAngVel;
        angVelDelta.x = Mathf.Lerp(oldVelDelta.x, angVelDelta.x, smoothFactor / Time.deltaTime);
        angVelDelta.y = Mathf.Lerp(oldVelDelta.y, angVelDelta.y, smoothFactor / Time.deltaTime);
        angVelDelta.z = Mathf.Lerp(oldVelDelta.z, angVelDelta.z, smoothFactor / Time.deltaTime);
        oldVelDelta = angVelDelta;
        angVelDelta = targetTransform.InverseTransformDirection(angVelDelta);
        oldAngVel = targetRB.angularVelocity;

        neededRot.x = angVelDelta.x * rotMults.x;
        neededRot.y = angVelDelta.y * rotMults.y;
        neededRot.z = angVelDelta.z * rotMults.z;

        return neededRot;
    }

   

    public void ConnectCam(Transform camT)
    {
        camTransform = camT;
        camTransform.parent = transform;
        camTransform.localPosition = BestPosCalc();
    }
    public void DisconnectCam()
    {
        camTransform = null;
    }

	// Use this for initialization
	void Start () {

        if(camTransform != null)
        {
            if (camTransform.parent != transform)
            {
                camTransform.parent = transform;
            }
            if (forwardOffset == 0) forwardOffset = camTransform.localPosition.z;
            if (verticalOffset == 0) verticalOffset = camTransform.localPosition.y;
        }
        
        targetTransform = transform.parent;
        targetRB = targetTransform.GetComponent<Rigidbody>();
        
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 newPos = BestPosCalc();
        Vector3 newRot = BestRotCalc();

        if (camTransform != null)
        {
            newPos.x = Mathf.Lerp(camTransform.localPosition.x, newPos.x, smoothFactor * Time.deltaTime);
            newPos.y = Mathf.Lerp(camTransform.localPosition.y, newPos.y, smoothFactor * Time.deltaTime);
            newPos.z = Mathf.Lerp(camTransform.localPosition.z, newPos.z, smoothFactor * Time.deltaTime);

            camTransform.localPosition = newPos;


            // if(newRot.x )

            /* newRot.x = Mathf.Lerp(camTransform.localEulerAngles.x+90, newRot.x+90, smoothFactor * Time.deltaTime);
             newRot.y = Mathf.Lerp(camTransform.localEulerAngles.y+90, newRot.y+90, smoothFactor * Time.deltaTime);
             newRot.z = Mathf.Lerp(camTransform.localEulerAngles.z+90, newRot.z+90, smoothFactor * Time.deltaTime);*/
            if (anchorRotation) transform.localEulerAngles = newRot;
            else camTransform.localEulerAngles = newRot;
        }


        
        
	}
}
