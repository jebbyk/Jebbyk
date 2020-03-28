using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMonitor : MonoBehaviour
{
  

    float passedTime;

    public GraphDrawer gd;
    
    public TextMesh fpsText;
    public float updateInterval;





    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update()
    {

        
         gd.currentValue = (1/Time.deltaTime);

         fpsText.text = "FPS: " + (1/Time.deltaTime).ToString("#") + "\nFrame Time: " + Time.deltaTime.ToString("#.000") + "ms";

    }
}
