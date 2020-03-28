using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour {

    public List<CameraAnchor> anchors;
    public string changeKey;
    bool flag = true;
    public int i = 0;

	// Use this for initialization
	void Start () {

        anchors[i].ConnectCam(transform);

	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetAxis(changeKey) > 0 )
        {
            if (flag)
            {
                anchors[i].DisconnectCam();
                flag = false;
                if (i < anchors.Count - 1)
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
                anchors[i].ConnectCam(transform);
            }
        }
        else
        {
            flag = true;
        }


    }
}
