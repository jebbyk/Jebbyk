using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/// <summary>
/// this class can only operate with devices atteched to the cuurent object
/// so for example it cant "fly towards" or "dock at" or even "Attack someone"
/// It only can throttle, brake, roll left or right, make weapon or rocketLouncher shoot e.t.c
/// If you wont to add more functionality u should add another component (AI for example)
/// </summary>
public class SpacecraftController : MonoBehaviour {

    public List<BoosterController> boostersList;
    public VelocityStablilizer stab;

    public bool stabSwitchState;

    public GraphDrawer gd2;
    public float rollFactor;
    /// <summary>
    /// Name of the action need to be sended and it's value
    /// </summary>
    public class SpacecraftMicrocomand {
        public string name;
        public float fValue;
        public SpacecraftMicrocomand(string _name, float _fValue)
        {
            name = _name;
            fValue = _fValue;
        }
    }

    public List<SpacecraftMicrocomand> microcomands = new List<SpacecraftMicrocomand>();



	// Use this for initialization
	void Start () {

        stab = transform.GetComponent<VelocityStablilizer>();
		
	}
	
	// Update is called once per frame
	void Update () {

        //flight controll
        foreach(BoosterController bc in boostersList)
        {
            bc.currentPower = 0;
            foreach (string t in bc.triggersList)
            {
                foreach (SpacecraftMicrocomand mc in microcomands)
                {
                    if (mc.name == t && mc.fValue > 0)
                    {
                        if (mc.name == "RollLeft" || mc.name == "RollRight") mc.fValue /= rollFactor;
                        bc.currentPower += mc.fValue;
                    }
                    //microcomands.Remove(mc);
                }
                
            }
            if (bc.currentPower > 1) bc.currentPower = 1;
        }

        foreach (SpacecraftMicrocomand mc in microcomands)
        {
            if(mc.name == "SwitchStabilization")
            {
                
                stab.momStabEnbaled = !stab.momStabEnbaled;
                stab.velStabEnabled = stab.momStabEnbaled;
            }
        }
	}


    void LateUpdate() {
        microcomands.Clear();
    }


}
