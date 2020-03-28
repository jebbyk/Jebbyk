using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class changeFrameRate : MonoBehaviour
{
    // Start is called before the first frame update


    public List<int> frameRates;
    public int currenFrameRate = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.F)){
            if(currenFrameRate < frameRates.Count-1)
            {
                currenFrameRate++;
            }else{
                currenFrameRate = 0;
            }
            Application.targetFrameRate = frameRates[currenFrameRate];
        }
        
    }
}
