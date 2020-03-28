using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {

    SpacecraftController shipController;

    public bool enableShipControls = false;


    public List<string> controlsList = new List<string>();

    public GraphDrawer gd1;
    public GraphDrawer gd2;
    public GraphDrawer gd3;
    public GraphDrawer gd4;


	// Use this for initialization
	void Start () {

        shipController = GetComponent<SpacecraftController>();
      

		
	}
	
	// Update is called once per frame
	void Update () {


        foreach(string s in controlsList)
        {

            if(s == "SwitchControl" && Input.GetButtonDown(s)) 
            {
                enableShipControls = Cursor.visible = !enableShipControls;
                if(enableShipControls)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }else{
                    Cursor.lockState = CursorLockMode.None;    
                }
            }

            if(s == "SwitchStabilization" && Input.GetButtonDown(s)) 
            {
                shipController.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand(s, Input.GetAxis(s)));
            }
            
            if(enableShipControls)
            {
                //This shit is only for mouse. I HATE THIS FUCKING MOUSE INPUT IN UNITY.... HUYUNITY!!!!!!!!!!!!!!!!!
                if(s == "MouseVertical" || s == "MouseHorizontal")
                {
                    if(s=="MouseVertical")//HAAAAAAATEEE
                    {
                        if(Input.GetAxis(s) > 0) shipController.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("PitchUp", Mathf.Clamp01(Input.GetAxis("MouseVertical")/Time.deltaTime)));
                        if(Input.GetAxis(s) < 0) shipController.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("PitchDown", Mathf.Clamp01(-Input.GetAxis("MouseVertical")/Time.deltaTime)));
                    }
                    if(s=="MouseHorizontal")//FUUUUCKKKKK
                    {
                        if(Input.GetAxis(s) > 0) shipController.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("YawRight", Mathf.Clamp01(Input.GetAxis("MouseHorizontal")/Time.deltaTime)));
                        if(Input.GetAxis(s) < 0) shipController.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand("YawLeft", Mathf.Clamp01(-Input.GetAxis("MouseHorizontal")/Time.deltaTime)));
                    }
                }else{
                    if(Input.GetAxis(s) > 0)
                    {
                        if(s !="SwitchStabilization")
                        {
                            shipController.microcomands.Add(new SpacecraftController.SpacecraftMicrocomand(s, Input.GetAxis(s)));
                        }
                    }
                }
            }   
        }
        
	}
}
