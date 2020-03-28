using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIFlightController : MonoBehaviour
{

    public Transform targetTransform;
    Vector3 distanceToTarget;

    public float solidsSearchRadius;
    Collider[] nearestSolids;

    Vector3 cohesion;
    Vector3 cohesionVector;
    Vector3 collisionAvoidanceVector;

    public Vector3 folowingVector;
    public Vector3 oldfolowingVector;

    Vector3 deltaVector;

    int nearestSolidsCount;

    Vector3 neededMovingVector;

    Vector3 currentAngularVelo;
    Vector3 currentRelativeAngularVelo;
    Vector3 currentVelo;
    Vector3 currentRelativeVelo;

    Vector3 currentRelativeRot;


    public LayerMask collisionAvoidanceMask;

     Rigidbody rb;

     public float deltaMovementWeight, colAvoidanceWeight, collAvoidOverDistPow, deltaVectorOverDistancePow;
     public bool doForce;

     RaycastHit[] hits;

    public LinesDrawer linesDrawer;
    SpacecraftController spacecraft;

    public float doForceTreshold;

      Vector3 resultVector;

      public bool debug;

      





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentAngularVelo = targetTransform.InverseTransformDirection(rb.angularVelocity);
        currentAngularVelo = targetTransform.InverseTransformDirection(rb.angularVelocity);
        currentVelo = transform.InverseTransformDirection(rb.velocity);
        neededMovingVector = calculateVelocity();
        spacecraft = transform.GetComponent<SpacecraftController>();
    }
    // Update is called once per frame


    void OnCollisionEnter(Collision collision)
    {
        float strenght = collision.impulse.magnitude;
       
       if(debug) Debug.Log("collision: " + strenght.ToString());
    }

    public Vector3 calculateVelocity()
    {
        resultVector = Vector3.zero;
        oldfolowingVector = folowingVector;
        folowingVector = targetTransform.position - transform.position;

        deltaVector = folowingVector - oldfolowingVector;
        deltaVector /= Mathf.Pow((targetTransform.position - transform.position).magnitude, deltaVectorOverDistancePow);
        deltaVector *= deltaMovementWeight;

        

        collisionAvoidanceVector = Vector3.zero;
        nearestSolidsCount = 0;
        nearestSolids = Physics.OverlapSphere(transform.position, solidsSearchRadius, collisionAvoidanceMask);
        foreach(var solid in nearestSolids)
        {
            if(solid != GetComponent<Collider>() )
            {
                if(debug)
                {    
                    linesDrawer.pushLine(transform.position, solid.transform.position, Color.cyan);
                    Debug.DrawLine(transform.position, solid.transform.position, Color.cyan);
                }


                RaycastHit[] rHits;
                rHits = Physics.RaycastAll(transform.position, solid.transform.position - transform.position, Vector3.Distance(solid.transform.position,transform.position));

                for(int i = 0; i < rHits.Length; i++)
                {
                    if(rHits[i].transform.parent != transform && rHits[i].transform != transform){
                        Vector3 sepColAvoidanceV = (transform.position - rHits[i].point) / Mathf.Pow((transform.position - rHits[i].point).magnitude, collAvoidOverDistPow);
                        sepColAvoidanceV *= colAvoidanceWeight;
                        collisionAvoidanceVector += sepColAvoidanceV ;
                        nearestSolidsCount++;
                    }
                }
            }
        }

        

        
        // hits = Physics.RaycastAll(transform.position, folowingVector ,Vector3.Distance(targetTransform.position,transform.position));

        // for(int i = 0; i < hits.Length; i++)
        // {
        //     if(hits[i].transform.parent != transform)
        //     {
        //         if(hits[i].transform != transform)
        //         {
        //             if(hits[i].transform.parent != targetTransform)
        //             {
        //                 if(hits[i].transform != targetTransform) 
        //                 {   
        //                     //folowingVector = Vector3.zero;//if hits something, we will not fly in this direction
        //                     resultVector = folowingVector;
        //                     break;
        //                 }
        //             }
        //         }
                
        //     }
        //     if(  hits[i].transform == targetTransform)
        //     {
        //         resultVector = folowingVector;
        //         break;//leave following vector as is
        //     }

        // }


            



        resultVector = collisionAvoidanceVector + folowingVector + deltaVector - rb.velocity;

       if(debug)
       { 
            linesDrawer.pushLine(transform.position, transform.position - rb.velocity, Color.yellow);
            Debug.DrawLine(transform.position, transform.position - rb.velocity, Color.yellow);

            linesDrawer.pushLine(transform.position, transform.position + deltaVector, Color.red);
            Debug.DrawLine(transform.position, transform.position + deltaVector, Color.red);

            linesDrawer.pushLine(transform.position, transform.position + collisionAvoidanceVector, Color.green);
            Debug.DrawLine(transform.position, transform.position + collisionAvoidanceVector, Color.green);

            linesDrawer.pushLine(transform.position, transform.position + resultVector, Color.white);
            Debug.DrawLine(transform.position, transform.position + resultVector, Color.white);

            linesDrawer.pushLine(transform.position, transform.position + folowingVector, Color.blue);
            Debug.DrawLine(transform.position, transform.position + folowingVector, Color.blue);
       }

        if(resultVector.magnitude < doForceTreshold)
        {
            return collisionAvoidanceVector + deltaVector - rb.velocity;
        }else{
            // if(debug)
            // {
            //     linesDrawer.pushLine(transform.position, transform.position + folowingVector, Color.blue);
            //     Debug.DrawLine(transform.position, transform.position + folowingVector, Color.blue);
            // }
            
            return resultVector; 
        }
    }




    void Update()
    {
        currentAngularVelo = targetTransform.InverseTransformDirection(rb.angularVelocity);
        currentAngularVelo = targetTransform.InverseTransformDirection(rb.angularVelocity);
        currentVelo = transform.InverseTransformDirection(rb.velocity);

        neededMovingVector = calculateVelocity();
        // if(neededMovingVector.magnitude > 1)
        // {
        //     neededMovingVector = neededMovingVector.normalized;
        // }

        Vector3 vm = -transform.InverseTransformDirection(neededMovingVector);

        //if(vm.magnitude > 1.0f) vm = vm / vm.magnitude;
            
        if(doForce)
        {
            if (vm.z > 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("Brake", Mathf.Clamp01( vm.z * Time.fixedDeltaTime)));
            if (vm.z < 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("Throttle", Mathf.Clamp01( -vm.z * Time.fixedDeltaTime)));
        
            if (vm.y > 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("StrafeDown", Mathf.Clamp01( vm.y * Time.fixedDeltaTime)));
            if (vm.y < 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("StrafeUp", Mathf.Clamp01( -vm.y * Time.fixedDeltaTime)));
        
            if (vm.x > 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("StrafeLeft", Mathf.Clamp01( vm.x * Time.fixedDeltaTime)));
            if (vm.x < 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("StrafeRight", Mathf.Clamp01(-vm.x * Time.fixedDeltaTime)));
        }


        Vector3 dir = transform.forward;
        Vector3 neededDir = (neededMovingVector ).normalized;
        if(neededMovingVector.magnitude < doForceTreshold*2)
        {
            neededDir = targetTransform.forward;
        }
        float angle  = Vector3.Angle(dir, neededDir);
        Vector3 vr = Vector3.Cross(neededDir, dir)*angle;
        
       
        vr = transform.InverseTransformDirection(vr) + transform.InverseTransformDirection(rb.angularVelocity)*10;
        //vr.z += (-transform.eulerAngles.z+180);


        if(doForce)
        {
            if (vr.x > 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("PitchUp", Mathf.Clamp01(  vr.x * Time.fixedDeltaTime)));
            if (vr.x < 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("PitchDown", Mathf.Clamp01( -vr.x * Time.fixedDeltaTime)));
            
            if (vr.y > 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("YawLeft", Mathf.Clamp01( vr.y * Time.fixedDeltaTime)));
            if (vr.y < 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("YawRight", Mathf.Clamp01( -vr.y * Time.fixedDeltaTime)));

            if (vr.z > 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("RollRight", Mathf.Clamp01( vr.z * Time.fixedDeltaTime)));
            if (vr.z < 0) spacecraft.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("RollLeft", Mathf.Clamp01(-vr.z *Time.fixedDeltaTime)));
        }
    }
}
