using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour
{
    public float verticalOffset;
    public float forwardOffset;
    public Vector3 offsetOverVelocity;
    public Vector3 offsetOverVelocityHardness;
    public Vector3 rotationOverAngularVelocity;
    public float anchorRotationMultiplier;


    public float smoothFactor;
    public bool rotateCam;
    public bool rotateAnchor;
    public Transform targetTransform;
    public Rigidbody targetRB;
    public Transform camTransform;

    Vector3 curentVelocity;
    Vector3 oldVelocity;
    Vector3 velocityDelta;
    Vector3 oldVelocityDelta;
    Vector3 positionToBeSetted;


    Vector3 angularVelocity;
    Vector3 oldAngularVelocity;
    Vector3 angularVelocityDelta;
    Vector3 oldAngularVelocityDelta;
    Vector3 rotationToBeSetted;

    public GraphDrawer gd1;
    public GraphDrawer gd2;
    public GraphDrawer gd3;
    public GraphDrawer gd4;
    // Start is called before the first frame update
    

    Vector3 CalcRotation(){
        
        oldAngularVelocity = angularVelocity;
        angularVelocity = targetTransform.InverseTransformDirection( targetRB.angularVelocity);

        oldAngularVelocityDelta = angularVelocityDelta;//delta from privious frame
        angularVelocityDelta = (angularVelocity - oldAngularVelocity)/Time.deltaTime;//current delta

        angularVelocityDelta.x  = Mathf.Lerp(oldAngularVelocityDelta.x, angularVelocityDelta.x, smoothFactor*Time.deltaTime*10);
        angularVelocityDelta.y  = Mathf.Lerp(oldAngularVelocityDelta.y, angularVelocityDelta.y, smoothFactor*Time.deltaTime*10);
        angularVelocityDelta.z  = Mathf.Lerp(oldAngularVelocityDelta.z, angularVelocityDelta.z, smoothFactor*Time.deltaTime*10);

        rotationToBeSetted.x =  angularVelocityDelta.x * rotationOverAngularVelocity.x;
        rotationToBeSetted.y =  angularVelocityDelta.y * rotationOverAngularVelocity.y;
        rotationToBeSetted.z =  angularVelocityDelta.z * rotationOverAngularVelocity.z;

        return rotationToBeSetted;
    }

    Vector3 CalcPosition(){
        
        oldVelocity = curentVelocity;
        curentVelocity = targetTransform.InverseTransformDirection( targetRB.velocity);

        oldVelocityDelta = velocityDelta;//delta from privious frame
        velocityDelta = (curentVelocity - oldVelocity)/Time.deltaTime;//current delta

        velocityDelta.x  = Mathf.Lerp(oldVelocityDelta.x, velocityDelta.x, smoothFactor*Time.deltaTime*10);
        velocityDelta.y  = Mathf.Lerp(oldVelocityDelta.y, velocityDelta.y, smoothFactor*Time.deltaTime*10);
        velocityDelta.z  = Mathf.Lerp(oldVelocityDelta.z, velocityDelta.z, smoothFactor*Time.deltaTime*10);

        positionToBeSetted.x =  Mathf.Atan(velocityDelta.x*offsetOverVelocityHardness.x)* offsetOverVelocity.x;
        positionToBeSetted.y =  verticalOffset + Mathf.Atan(velocityDelta.y*offsetOverVelocityHardness.y)* offsetOverVelocity.y;
        positionToBeSetted.z = forwardOffset + Mathf.Atan(velocityDelta.z*offsetOverVelocityHardness.z)*offsetOverVelocity.z;

        return positionToBeSetted;
    }

    public void ConnectCam(Transform camT)
    {
        camTransform = camT;
        camTransform.parent = transform;
        camTransform.localPosition = new Vector3(0,0,0);
    }
    public void DisconnectCam()
    {
        camTransform = null;
    }

    void Start()
    {

        if(camTransform != null)
        {
            if(camTransform.parent != transform)
            {
                camTransform.parent = transform;
            }
            if (forwardOffset == 0) forwardOffset = camTransform.localPosition.z;
            if (verticalOffset == 0) verticalOffset = camTransform.localPosition.y;

        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 newRotation = CalcRotation();
        Vector3 newPosition = CalcPosition();

        if(camTransform != null)
        {
            if (rotateCam) camTransform.localEulerAngles = newRotation;
            if(rotateAnchor) {
                    transform.localEulerAngles = newRotation*anchorRotationMultiplier;
            }

            camTransform.localPosition = newPosition;
        }
    }
}
