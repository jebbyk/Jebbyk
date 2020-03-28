using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchorController : MonoBehaviour
{

   /* public float baseForwardOffset;
    public float baseVerticalOffset;
    public float offsetByFwdSpeed;
    public float offsetByHorSpeed;
    public float offsetByVertSpeed;
    public float rotOffsetByAngVel;*/

    public Vector3 baseOffset;
    public Vector3 offsetOverVelo;

    public Vector3 offsetOverVeloDelta;

    public Vector3 rotOffsetOverVelo;

    Transform camTransform;
    Rigidbody targetRB;
    Transform targetTransform;
    public bool AllowRotation;

    Vector3 velocity;
    Vector3 oldVelocity;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
