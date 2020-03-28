using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GraphDrawer : MonoBehaviour
{

    
    public float graphThickness;

    public int dotsCount;
    public LineRenderer lr;
    public List<float> values;

    public float currentValue;

    public float horScale;
    public float verScale;

    bool flag = true;

    public float minValue;

    float passedTime;

    public float updateInterval;

    public bool smoothGraph;
    public float dotAccelFactor;
    public float smoothFactor;

    

    public LinesDrawer linesDrawer;

    public bool glDrawer;

    private float speed;
    private float neededSpeed;
    private float oldValue;

    // Start is called before the first frame update
    void Start()
    {
        lr = transform.GetComponent<LineRenderer>();

        lr.positionCount = dotsCount;
    }

    // Update is called once per frame
    void Update()
    {
         passedTime += Time.deltaTime;

        if(passedTime > updateInterval)
        {
            for(int i = 1; i < dotsCount; i++)
            {
               values[i-1] = values[i];
            }

            if(smoothGraph)
            {
                neededSpeed = currentValue - oldValue;
                if (speed > neededSpeed) speed -= dotAccelFactor * passedTime;
                if (speed < neededSpeed) speed += dotAccelFactor * passedTime;
                
                values[dotsCount-1] = oldValue + speed*smoothFactor*passedTime;

                oldValue = values[dotsCount-1];
            }else{
                values[dotsCount-1] = currentValue;
            }


            float topValue = values.Max();

        
            if(topValue < minValue) topValue = minValue;

            if(!glDrawer)
            {
                for(int i = 1; i < dotsCount; i++)
                {
                    lr.SetPosition(i, new Vector3(((float)i/dotsCount)*horScale, (values[i]/topValue)*verScale, 0));
                    
                }
            }else
            {
                for(int i = 1; i < dotsCount; i++)
                {
                    linesDrawer.pushLine(
                    transform.position + (transform.right*(i-1)*horScale) + (transform.up*(values[i-1]/topValue)*verScale),
                    transform.position + (transform.right*i*horScale) + (transform.up*(values[i]/topValue)*verScale), 
                    Color.white);
                }
            }
            

            passedTime = 0;
        }
    }
}
