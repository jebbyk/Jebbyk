using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpeedMonitor : MonoBehaviour
{
    public Transform tt;
    public GraphDrawer gd;
    public TextMesh speedText, accelText;
    Vector3 oldPosition;
    Vector3 newPosition;
    float passedTime;
    public float updateInterval;
    float speed;
    float oldSpeed;
    float acceleration;


    // Start is called before the first frame update
    void Start()
    {
        oldPosition = tt.position;
        newPosition = oldPosition;
    }

    void Update()
    {
        passedTime += Time.deltaTime;

        if(passedTime > updateInterval)
        {
            oldPosition = newPosition;
            newPosition = tt.position;
            oldSpeed = speed;
            speed = Vector3.Distance(oldPosition, newPosition)/passedTime;

            acceleration = (speed - oldSpeed)/passedTime;

            gd.currentValue = speed;
            speedText.text = speed.ToString("#") + " m/s";
            accelText.text = "(acc: " +  acceleration.ToString("#") + "m/s)";
            passedTime = 0; 

        }
    }
}
